using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Data
{
    public class DataContext : DbContext, IDataContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TestForHangfire> TestForHangFire { get; set; }
        public DbSet<JobResult> JobResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestForHangfire>().ToTable("TestForHangfire");
        }
    }
}
