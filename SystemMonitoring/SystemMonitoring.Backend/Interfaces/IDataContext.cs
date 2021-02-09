using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Interfaces
{
    public interface IDataContext
    {
        public DbSet<TotalJobResult> TotalJobResults { get; set; }
    }
}
