using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Gabriel";
                output.LastName = "Stancu";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Adrian";
                output.LastName = "Buciuman";
            }
            else
            {
                output.FirstName = "Daniel";
                output.LastName = "Bancos";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Gabriel",
                    LastName = "Stancu",
                    EmailAddress = "gs7@yahoo.com",
                    Age = 22,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Adrian",
                    LastName = "Buciuman",
                    EmailAddress = "ab3@yahoo.com",
                    Age = 22,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Daniel",
                    LastName = "Bancos",
                    EmailAddress = "db5@yahoo.com",
                    Age = 21,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                // await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
