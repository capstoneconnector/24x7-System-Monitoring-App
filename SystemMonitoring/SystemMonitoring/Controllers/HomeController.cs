using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Models;
using SystemMonitoring.Backend.Handlers;
using SystemMonitoring.Backend.Interfaces;
using Hangfire;
using Npgsql;
using SystemMonitoring.Backend.Models;
using SystemMonitoring.ViewModels;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Enumeration;

namespace SystemMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IApiJobFactory _apiJobFactory;
        private DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, IApiJobFactory apiJobFactory, DataContext dataContext)
        {
            _logger = logger;
            _apiJobFactory = apiJobFactory;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate(() => TotalJobsCall(JobType.TotalJobs), "0/30 * * ? * *");

            //var jobResults = _dataContext.JobResults.Where(x => x.TotalFailedJobs>0);

            var totalJobResults = _dataContext.TotalJobResults.Skip(Math.Max(0, _dataContext.TotalJobResults.Count() - 5));
            var currentJobResults = _dataContext.CurrentJobResults.Skip(Math.Max(0, _dataContext.CurrentJobResults.Count() - 5));

            var viewModel = new HomeIndexViewModel
            {
                TotalJobResults = totalJobResults,
                CurrentJobResult = currentJobResults,

            };

            return View(viewModel);
        }


        //IDEA
        //Each method is a seperate job. The hangfire method below is purely made to get the api call for accutech data
        //If we make several methods, we just have a flag that runs in the index to see what job should be add. Index is ran on open so it'll always add that one job
        //There are only a few api calls they will want, we just have to make a method for each one

        //WORRY
        //Do we need to reload every job the program is opened?
        //  -   Does this matter since this is a website?

        //var job = _apiJobFactory.CreateJob();//string endpoint goes in here)

        //Job ID - What kind of job is being done
        //TJ - Total Jobs - //api.accutechdataexchange.com/api/Hangfire/TotalJobs?date=
        //CJ - Current Jobs - //api.accutechdataexchange.com/api/Hangfire/CurrentJobStates
        //if (JobID == "TJ")
        //{
        //RecurringJob.AddOrUpdate(() => TotalJobsCall(), "0/30 * * ? * *");
        //}

        [HttpPost]
        public async Task <IActionResult> Submit(AddJobViewModel model)
        {
            if (model.JobId == JobType.TotalJobs)
            {
                await TotalJobsCall(model.JobId);
            }
            else
            {
                await CurrentJobStatesCall(model.JobId);
            }
            return RedirectToActionPermanent("Index");
        }
        public async Task TotalJobsCall(JobType jobType)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Year.ToString();

            string date = mn + "/" + dy + "/" + yy; 
            var apiEndpoint = "https://api.accutechdataexchange.com/api/Hangfire/TotalJobs?date=" + date;
            var job = _apiJobFactory.CreateJob(apiEndpoint, jobType);
            await job.Run();
        }

        public async Task CurrentJobStatesCall(JobType jobType)
        {
           
            //string date = mn + "/" + dy + "/" + yy;
            var apiEndpoint = "https://api.accutechdataexchange.com/api/Hangfire/CurrentJobStates";
            var job = _apiJobFactory.CreateJob(apiEndpoint, jobType);
            await job.Run();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
