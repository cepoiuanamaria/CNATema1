using Microsoft.Extensions.Logging;
using GrpcServer.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService: Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Bob";
                output.SecondName = "Dylan";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Bon";
                output.SecondName = "Jovi";
            }
            else
            {
                output.FirstName = "Joe";
                output.SecondName = "Nickleson";
            }
            return Task.FromResult(output);
        }
        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName="Jill",
                    SecondName="Jimmy",
                    Age=4,
                    IsAlive=true
                },
                new CustomerModel
                {
                    FirstName="Bob",
                    SecondName="Bonny",
                    Age=7,
                    IsAlive=false
                },
                new CustomerModel
                {
                    FirstName="Mat",
                    SecondName="Damon",
                    Age=21,
                    IsAlive=true
                }
            };
            foreach(var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
