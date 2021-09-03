using Autofac;
using CustomerInviter.Api.Service.Services;

namespace CustomerInviter.Api.Service.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ICustomerFileIngester).Assembly)
                .InNamespaceOf<ICustomerFileIngester>()
                .AsImplementedInterfaces();
        }
    }
}