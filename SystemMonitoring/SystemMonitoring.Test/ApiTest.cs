using Autofac.Extras.Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Factories;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Jobs;
using SystemMonitoring.Backend.Models;
using SystemMonitoring.ViewModels;

namespace SystemMonitoring.Test
{
    [TestClass]
    public class ApiTest
    {
        private DataContext _dataContext;

        [TestMethod]
        public void TestApiJobFactory()
        {
            var dbOption = new DbContextOptionsBuilder<DataContext>().UseSqlServer("DefaultConnection").Options;
            DataContext dcon = new DataContext(dbOption);
            var endpoint = "https://api.accutechdataexchange.com/api/Hangfire/TotalJobs?date=12/15/2020";
            ApiJobFactory jobFact = new ApiJobFactory(dcon);
            var api_job = jobFact.CreateJob(endpoint);
            Assert.IsTrue(api_job is ApiJob);
            Assert.IsNotNull(api_job);
        }




    }
}
