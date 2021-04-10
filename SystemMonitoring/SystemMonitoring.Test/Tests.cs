using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Models;
using SystemMonitoring.Controllers;
using SystemMonitoring.ViewModels;

namespace SystemMonitoring.Test
{
    [TestClass]
    public class Tests
    {
        private DataContext _dataContext;
        private readonly HomeController _homeController;
        private readonly AddJobViewModel _mock;
        private readonly ILogger<HomeController> _logger;
        private readonly IApiJobFactory _apiJobFactory;
        private readonly ReoccurringJob _mockReoccurringJob;

        public Tests()
        {
            _homeController = new HomeController(_logger, _apiJobFactory, _dataContext);
        }

        [TestMethod]
        public void Test_IndexViewReturn()
        {
            var expected = typeof(Task<IActionResult>);
            var result = _homeController.Index();
            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_AddTaskReturn()
        {
            var expected = typeof(IActionResult);
            var result = _homeController.AddTask();
            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_SubmitReturn()
        {
            var expected = typeof(Task<IActionResult>);
            var result = _homeController.Submit(_mock);
            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_AddNewTaskReturn()
        {
            var expected = typeof(Task);
            var result = _homeController.AddNewTask(_mock);
            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_AddTaskPartialReturn()
        {
            var expected = typeof(Task<IActionResult>);
            var result = _homeController.AddTaskPartial("http://api.accutechdataexchange.com/api/Hanfire/TotalJobs%22");

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_GetFieldsReturn()
        {
            var expected = typeof(Task<Dictionary<string, object>>);
            var result = _homeController.GetFields("http://api.accutechdataexchange.com/api/Hanfire/TotalJobs%22");

            Assert.IsInstanceOfType(result, expected);
        }

        [TestMethod]
        public void Test_RunTaskReturn()
        {
            var expected = typeof(Task);
            var result = _homeController.RunTask(_mockReoccurringJob);
            Assert.IsInstanceOfType(result, expected);
        }
    }
}
