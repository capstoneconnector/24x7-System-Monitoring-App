using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
           
            if (context.TestForHangFire.Any())
            {
                return;   // DB has been seeded
            }

            var testData = new TestForHangfire[]
            {
                new TestForHangfire{Id=1,Job="Janitor",RunTime="12am"},
                new TestForHangfire{Id=2,Job="Manager",RunTime="1pam"},
                new TestForHangfire{Id=3,Job="Tester",RunTime="2pm"},


            };
            foreach (TestForHangfire s in testData)
            {
                context.TestForHangFire.Add(s);
            }
            context.SaveChanges();

        }
    }
}
