using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IApiJob
    {
        Task Run<T>() where T : class;
    }
}
