using Autofac;
using CustomerInviter.Api.Service;

namespace CustomerInvite.Api.Service.Tests
{
    internal class TestableBootstrapper : Bootstrapper
    {
        private ILifetimeScope _scope;

        internal ILifetimeScope Scope
        {
            get
            {
                if (_scope != null) return _scope;

                var container = base.GetApplicationContainer();

                // Here we can add any mocked/dummy external services we may need or setup a database etc.

                _scope = container;
                return _scope;
            }
        }

        protected override ILifetimeScope GetApplicationContainer() => Scope;
    }
}