using Autofac;
using CustomerInviter.Core.Validators;

namespace CustomerInviter.Core.AutofacModules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ICoordinateValidator).Assembly)
                .InNamespaceOf<ICoordinateValidator>()
                .Where(t => t.Name.EndsWith("Validator"))
                .AsImplementedInterfaces();
        }
    }
}