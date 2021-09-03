using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit.Abstractions;

namespace CustomerInvite.Api.Service.Tests.Scenarios
{

    public class UploadCustomerFileScenario : ApiScenario
    {
        private BrowserResponse _response;
        private Stream _fileStream;
        
        public UploadCustomerFileScenario(ITestOutputHelper output) : base(output)
        {
            Output = output;
        }

        public void GivenACustomerFile()
        {
            _fileStream = GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream("CustomerInvite.Api.Service.Tests.customers.txt");
        }

        public async Task WhenUploadingTheCustomerFile()
        {
            _response = await Browser.Put("/customers/import", with =>
            {
                with.MultiPartFormData(new BrowserContextMultipartFormData(m => 
                    m.AddFile("customers", "customers.txt", "application/text",_fileStream)));
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
