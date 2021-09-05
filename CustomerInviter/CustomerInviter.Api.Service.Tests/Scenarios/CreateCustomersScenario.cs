using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerInviter.Core.Models;
using Nancy;
using Nancy.Testing;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit.Abstractions;

namespace CustomerInvite.Api.Service.Tests.Scenarios
{

    public class CreateCustomersScenario : ApiScenario
    {
        private BrowserResponse _response;
        private List<dynamic> _model;
        
        public CreateCustomersScenario(ITestOutputHelper output) : base(output)
        {
            Output = output;
        }

        public void GivenACustomerToCreate()
        {
            _model = new List<dynamic>
            {
                new
                {
                    Latitude = "52.986375", 
                    user_id = 12, 
                    Name  = "Christina McArdle", 
                    Longitude = "-6.043701" 
                },
                new CustomerModel
                {
                    User_Id = 4, 
                    Name = "Ian Kehoe", 
                    Latitude = "53.2451022", 
                    Longitude = "-6.238335" 
                }
            };
        }

        public async Task WhenCreatingTheCustomer()
        {
            _response = await Browser.Put("/customers", with =>
            {
                with.JsonBody(_model);
            });
        }

        public void ThenTheResponseShouldBeAccepted()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        }

        [BddfyFact]
        public void Fact()
        {
            this.BDDfy();
        }
    }
}
