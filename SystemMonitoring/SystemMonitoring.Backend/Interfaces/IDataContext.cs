using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IDataContext
    {
        public DbSet<JobResult> JobResults { get; set; }
    }
}
