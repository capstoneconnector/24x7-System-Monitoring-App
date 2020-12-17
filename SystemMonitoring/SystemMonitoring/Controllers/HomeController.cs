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
            RecurringJob.AddOrUpdate(() => HangfireMethod(), "0/30 * * ? * *");

            //var jobResults = _dataContext.JobResults.Where(x => x.TotalFailedJobs>0);

            var jobResults = _dataContext.JobResults.Skip(Math.Max(0, _dataContext.JobResults.Count() - 5));

            var viewModel = new HomeIndexViewModel
            {
                JobResults = jobResults,
            };

            return View(viewModel);
        }

        public async Task HangfireMethod()
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Year.ToString();

            string date = mn + "/" + dy + "/" + yy; 
            var apiEndpoint = "https://api.accutechdataexchange.com/api/Hangfire/TotalJobs?date=" + date;
            var job = _apiJobFactory.CreateJob(apiEndpoint);
            await job.Run();
        }

        public async Task<YahooApiCallData> YahooData()
        {
            var output = new YahooApiCallData
            {
                Data = await ApiCallHandler.ApiCallAsync()
            };
            return output;
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
