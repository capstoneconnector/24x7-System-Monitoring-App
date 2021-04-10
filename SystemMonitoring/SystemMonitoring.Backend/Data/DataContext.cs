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

        public DbSet<CurrentJobResult> CurrentJobResults { get; set; }
        public DbSet<ReoccurringJob> ReoccurringJob { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }


    }
}
