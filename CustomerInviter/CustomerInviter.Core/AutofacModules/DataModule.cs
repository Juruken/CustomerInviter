using Autofac;
using CustomerInviter.Core.Data;
using CustomerInviter.Core.Models;
using Module = Autofac.Module;

namespace CustomerInviter.Core.AutofacModules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IDatabase<Customer>).Assembly)
                .InNamespaceOf<IDatabase<Customer>>()
                .SingleInstance()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ICreateCustomerAction).Assembly)
                .InNamespaceOf<ICreateCustomerAction>()
                .Where(t => t.Name.EndsWith("Action") || t.Name.EndsWith("Query"))
                .AsImplementedInterfaces();
        }
    }
}