using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Data;
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

        public IApiJob CreateJob(string endpoint)
        {
            return new ApiJob(_dataContext, endpoint);
        }
    }
}
