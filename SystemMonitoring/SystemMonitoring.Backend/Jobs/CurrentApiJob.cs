using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Jobs
{
    public class CurrentApiJob : IApiJob
    {
        private readonly ReoccurringJob _task;
        private readonly DataContext _dataContext;

        public CurrentApiJob(DataContext dataContext, ReoccurringJob task)
        {
            _task = task;
            _dataContext = dataContext;
            Console.WriteLine(_dataContext);
        }
        public async Task Run<T>() where T : class
        {
            string field = _task.PriorityField;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync(_task.Url);

            if (result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                var messageAsObject = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
                var status = "Failed";
                var conditionalExpression = false;
                messageAsObject.TryGetValue(field, out object value);
                if (int.TryParse(value.ToString(), out int intValue))
                {
                    conditionalExpression = GetExpressionInt(int.Parse(_task.ConditionalExpression[1]), _task.ConditionalExpression[0])
                    .Compile()(intValue);
                }
                else
                {
                    conditionalExpression = GetExpressionString(_task.ConditionalExpression[1], _task.ConditionalExpression[0])
                    .Compile()(value.ToString());
                }


                if (conditionalExpression)
                {
                    status = "Successful";
                    var task = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == _task.Id);
                    task.AlertSent = false;
                    _dataContext.SaveChanges();
                }
                else if (_task.AlertSent == false)
                {

                    ContactGroup group = _dataContext.ContactGroups
                        .Include(e => e.Contacts)
                        .ThenInclude(e => e.User)
                        .ThenInclude(e => e.ContactInfo)
                        .FirstOrDefault(e => e.Id == _task.ContactGroup_Id);

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    foreach (var contact in group.Contacts)
                    {
                        mail.To.Add(contact.User.ContactInfo.Email);
                    }
                    mail.From = new MailAddress("24x7SystemMonitoring@gmail.com", "24x7 System Monitoring", System.Text.Encoding.UTF8);
                    mail.Subject = "Task " + _task.Name + " has failed";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = "We found a error occuring in the " + _task.Name + " task at " + DateTime.UtcNow.ToString() + ". Please log on to check the error";
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient smtp_client = new SmtpClient();
                    smtp_client.Credentials = new System.Net.NetworkCredential("24x7SystemMonitoring@gmail.com", "Plant123");
                    smtp_client.Port = 587;
                    smtp_client.Host = "smtp.gmail.com";
                    smtp_client.EnableSsl = true;
                    try
                    {
                        smtp_client.Send(mail);
                    }
                    catch (Exception)
                    {
                    }

                    var task = _dataContext.ReoccurringJob.FirstOrDefault(e => e.Id == _task.Id);
                    task.AlertSent = true;
                    _dataContext.SaveChanges();

                }

                try
                {
                    CurrentJobResult current_job = _dataContext.CurrentJobResults.FirstOrDefault(job => job.ReoccurringJobId == _task.Id);

                    if (current_job != null)
                    {
                        current_job.Date = DateTime.Now;
                        current_job.Status = status;
                    }
                    else
                    {
                        _dataContext.CurrentJobResults.Add(new CurrentJobResult
                        {
                            Date = DateTime.Now,
                            ReoccurringJobId = _task.Id,
                            Name = _task.Name,
                            Status = status,
                        });
                    }

                    _dataContext.JobHistories.Add(new JobHistory
                    {
                        Date = DateTime.Now,
                        ReoccurringJobId = _task.Id,
                        Name = _task.Name,
                        Status = status,
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                _dataContext.SaveChanges();

            }
        }

        public Expression<Func<int, bool>> GetExpressionInt(int value, string conditional)
        {
            ParameterExpression input = Expression.Parameter(typeof(int), "field value");
            ConstantExpression constValue = Expression.Constant(value, typeof(int));
            BinaryExpression operation = conditional switch
            {
                ">=" => Expression.GreaterThanOrEqual(input, constValue),
                ">" => Expression.GreaterThan(input, constValue),
                "<=" => Expression.LessThanOrEqual(input, constValue),
                "<" => Expression.LessThan(input, constValue),
                "!=" => Expression.NotEqual(input, constValue),
                _ => Expression.Equal(input, constValue),
            };
            Expression<Func<int, bool>> exprs =
               Expression.Lambda<Func<int, bool>>(
                   operation,
                   new ParameterExpression[] { input });
            return exprs;
        }
        public Expression<Func<string, bool>> GetExpressionString(string value, string conditional)
        {
            ParameterExpression input = Expression.Parameter(typeof(string), "field value");
            ConstantExpression constValue = Expression.Constant(value, typeof(string));
            BinaryExpression operation = conditional switch
            {
                "!=" => Expression.NotEqual(input, constValue),
                _ => Expression.Equal(input, constValue),
            };
            Expression<Func<string, bool>> exprs =
               Expression.Lambda<Func<string, bool>>(
                   operation,
                   new ParameterExpression[] { input });
            return exprs;
        }

    }
}
