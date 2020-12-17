using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IApiJobFactory
    {
        IApiJob CreateJob(String endpoint);
    }
}
