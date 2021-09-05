using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CustomerInviter.Core.Models;
using Nancy;
using Nancy.Testing;
using Serilog;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit.Abstractions;

namespace CustomerInvite.Api.Service.Tests.Scenarios
{

    public class UploadedCustomersByDistanceScenario : ApiScenario
    {
        private BrowserResponse _response;
        private Stream _fileStream;
        
        public UploadedCustomersByDistanceScenario(ITestOutputHelper output) : base(output)
        {
            Output = output;
        }

        public async Task GivenAnUploadedCustomerFile()
        {
            _fileStream = GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream("CustomerInvite.Api.Service.Tests.customers.txt");

            _response = await Browser.Put("/customers/import", with =>
            {
                with.MultiPartFormData(new BrowserContextMultipartFormData(m => 
                    m.AddFile("customers", "customers.txt", "application/text",_fileStream)));
            });

            _response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        }

        public async Task WhenFetchingCustomersInDistance()
        {
            _response = await Browser.Get($"/customers/distance/{100}");
        }

        public void ThenTheResponseShouldBeOK()
        {
            _response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        public void AndThenTheResponseShouldBeOnlyTwoCustomersInRange()
        {
            var result = _response.Body.DeserializeJson<List<CustomerModel>>();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(16);
        }

        public void AndThenTheResultsShouldBeSorted()
        {
            var result = _response.Body.DeserializeJson<List<CustomerModel>>();
            var customer = result.FirstOrDefault();
            customer.Latitude.ShouldBe("53.2451022");
            customer.Longitude.ShouldBe("-6.238335");
            customer.Name.ShouldBe("Ian Kehoe");
            customer.User_Id.ShouldBe(4);

            result.ForEach(c => Log.Information($"{c.Name}, {c.User_Id}, {c.Latitude}, {c.Longitude}"));
        }

        [BddfyFact]
        public void Fact()
        {
            this.BDDfy();
        }
    }
}
