using Autofac;
using CustomerInviter.Core.Services;

namespace CustomerInviter.Core.AutofacModules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ICustomerService).Assembly)
                .InNamespaceOf<ICustomerService>()
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}