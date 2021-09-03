using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CustomerInviter.Api.Service
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
        }
    }
}