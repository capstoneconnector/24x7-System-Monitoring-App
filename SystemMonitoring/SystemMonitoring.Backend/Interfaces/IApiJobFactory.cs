using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Enumeration;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IApiJobFactory
    {
        IApiJob CreateJob(String endpoint, JobType jobType);
    }
}
