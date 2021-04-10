using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
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
