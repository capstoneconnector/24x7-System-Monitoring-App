using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Models;
using SystemMonitoring.Backend.Interfaces;
using Hangfire;
using SystemMonitoring.Backend.Models;
using SystemMonitoring.ViewModels;
using SystemMonitoring.Backend.Data;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SystemMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiJobFactory _apiJobFactory;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, IApiJobFactory apiJobFactory, DataContext dataContext)
        {
            _logger = logger;
            _apiJobFactory = apiJobFactory;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ReoccurringJob> reoccuring = _dataContext.ReoccurringJob.AsEnumerable<ReoccurringJob>();
            List<ReoccurringJob> reoccuringlist = new List<ReoccurringJob>();

            foreach (var task in reoccuring)
            {
                reoccuringlist.Add(task);
            }

            foreach (var task in reoccuringlist)
            {
                RecurringJob.AddOrUpdate(task.Id.ToString(), () => RunTask(task), task.CronString);
            }

            IEnumerable<CurrentJobResult> currentJobResults = _dataContext.CurrentJobResults.AsEnumerable<CurrentJobResult>();

            var viewModel = new HomeIndexViewModel
            {
                CurrentJobs = currentJobResults.OrderBy(job => job.Name),
                ReoccurringJobs = reoccuring,
            };
            return View(viewModel);
        }

        public async Task RunTask(ReoccurringJob task)
        {
            var job = _apiJobFactory.CurrentJob(task);
            await job.Run<TotalJobsApiResponse>();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(AddJobViewModel model)
        {
            await AddNewTask(model);

            return RedirectToAction("Index");
        }

        public async Task AddNewTask(AddJobViewModel model)
        {
            if (model.Value == null)
            {
                model.Value = "";
            }

            var newTask = new ReoccurringJob
            {
                Name = model.Name,
                Url = model.Url,
                CronString = model.CronString,
                PriorityField = model.PriorityField,
                ConditionalExpression = new string[] { model.Conditional.ToString(), model.Value.ToString() },
                ContactGroup_Id = model.ContactGroupId,
            };

            try
            {
                _dataContext.ReoccurringJob.Add(newTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _dataContext.SaveChangesAsync();

            _dataContext.CurrentJobResults.Add(new CurrentJobResult {
                Name = newTask.Name,
                ReoccurringJobId = newTask.Id,
                Status = "Unchecked",
                Date = DateTime.MinValue,
            });

            await _dataContext.SaveChangesAsync();
        }

        public IActionResult AddTask()
        {
            return View();
        }

        public IActionResult Info(int taskId)
        {
            HistoryViewModel history = new HistoryViewModel
            {
                JobHistories = _dataContext.JobHistories.Where(job => job.ReoccurringJobId == taskId)
            };
            return View(history);
        }

        public async Task<IActionResult> AddTaskPartial(string urlInput)
        {
            FieldsViewModel fields = new FieldsViewModel
            {
                Fields = await GetFields(urlInput)
            };
            return PartialView(fields);
        }

        public IActionResult TaskInfo(int taskId)
        {
            var asset = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == taskId);

            var viewModel = new EditIndexViewModel

            {
                Id = asset.Id,
                Name = asset.Name,
                Url = asset.Url,
                CronString = asset.CronString,
                PriorityField = asset.PriorityField,
                Conditional = asset.ConditionalExpression[0],
                Value = asset.ConditionalExpression[1],

            };

            return PartialView(viewModel);
        }

        public IActionResult Edit(int taskId)
        {
            var asset = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == taskId);

            var viewModel = new EditIndexViewModel

            {
                Id = asset.Id,
                Name = asset.Name,
                Url = asset.Url,
                CronString = asset.CronString,
                PriorityField = asset.PriorityField,
                Conditional = asset.ConditionalExpression[0],
                Value = asset.ConditionalExpression[1],
                ContactGroupId = asset.ContactGroup_Id,

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirm(EditIndexViewModel model)
        {
            if (model.Value == null)
            {
                model.Value = "";
            }
            _dataContext.ReoccurringJob.Find(model.Id).Name = model.Name;
            _dataContext.ReoccurringJob.Find(model.Id).Url = model.Url;
            _dataContext.ReoccurringJob.Find(model.Id).CronString = model.CronString;
            _dataContext.ReoccurringJob.Find(model.Id).PriorityField = model.PriorityField;
            _dataContext.ReoccurringJob.Find(model.Id).ConditionalExpression = new string[] { model.Conditional.ToString(), model.Value.ToString() };
            _dataContext.ReoccurringJob.Find(model.Id).ContactGroup_Id = model.ContactGroupId;
            _dataContext.CurrentJobResults.FirstOrDefault(e => e.ReoccurringJobId == model.Id).Name = model.Name;

            await _dataContext.SaveChangesAsync();

            await RunOnCommand(model.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int taskId)
        {
            var asset = _dataContext.CurrentJobResults.FirstOrDefault(e => e.Id == taskId);

            var viewModel = new DeleteIndexViewModel

            {
                Id = asset.Id,
                Name = asset.Name,
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int taskId)
        {
            var current = _dataContext.CurrentJobResults.FirstOrDefault(e => e.Id == taskId);
            var reoccurring = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == current.ReoccurringJobId);

            _dataContext.Remove(current);
            _dataContext.Remove(reoccurring);

            _dataContext.SaveChanges();

            RecurringJob.RemoveIfExists(reoccurring.Id.ToString());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RunOnCommand(int taskId)
        {
            var task = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == taskId);
            var job = _apiJobFactory.CurrentJob(task);
            await job.Run<TotalJobsApiResponse>();

            return RedirectToAction("Index");
        }

        public async Task<Dictionary<string, object>> GetFields(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Dictionary<string, object>>(message);
            }
            else
                return null;
        }

        public IActionResult AddUser()
        {
            return View();
        }

        public void AddNewGroup(string groupName)
        {

            try
            {
                _dataContext.ContactGroups.Add(new ContactGroup
                {
                    Name = groupName,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            _dataContext.SaveChanges();
        }

        public IActionResult AddUserPartial(string urlInput)
        {
            ContactGroupsViewModel cGroups = new ContactGroupsViewModel
            {
                Groups = _dataContext.ContactGroups.AsEnumerable<ContactGroup>(),
            };
            return PartialView(cGroups);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUser(NewUserViewModel model)
        {
            await AddNewUser(model);

            return RedirectToAction("Index");
        }

        public async Task AddNewUser(NewUserViewModel model)
        {

            var info = new ContactInfo
            {
                PhoneNumber = model.Phone,
                Email = model.Email,
            };

            _dataContext.ContactInfos.Add(info);

            await _dataContext.SaveChangesAsync();

            var contactInfoId = info.Id;

            var user = new User { Name = model.Name, ContactInfo_Id = contactInfoId, };

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();

            _dataContext.Contacts.Add(new Contact
            {
                User_Id = user.Id,
                ContactGroup_Id = model.ContactGroupId,
                ContactMethod = model.ContactMethod,
            });

            await _dataContext.SaveChangesAsync();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
