using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IApiJobFactory
    {
        IApiJob CurrentJob(ReoccurringJob task);
    }
}
