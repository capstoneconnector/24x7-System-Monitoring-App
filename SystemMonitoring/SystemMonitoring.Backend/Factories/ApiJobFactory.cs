using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Enumeration;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Jobs;

namespace SystemMonitoring.Backend.Factories
{
    public class ApiJobFactory : IApiJobFactory
    {
        private DataContext _dataContext;

        public ApiJobFactory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IApiJob CreateJob(string endpoint, JobType jobType)
        {
            switch (jobType)
            {
                case JobType.CurrentJobs:
                    return new CurrentApiJob(_dataContext, endpoint);
                case JobType.TotalJobs:
                    return new TotalApiJob(_dataContext, endpoint);
                default:
                    return null;
            }
            
        }
    }
}
