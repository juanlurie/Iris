using System.Linq;
using Iris.Attributes;
using Iris.Ioc;
using Iris.Messaging;
using Iris.Messaging.Configuration;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.InitializerBuilder
{
    [InitializationOrder(Order = 2)]
    public abstract class ScopeInitializer : INeedToInitializeSomething
    {
        protected readonly string MutexKey;

        protected abstract string Name { get; }
        protected abstract string Description { get; }

        protected ScopeInitializer()
        {
            MutexKey = GetType().FullName;
        }

        public void Initialize()
        {
            try
            {
                using (var container = Settings.RootContainer.BeginLifetimeScope())
                using (new SingleGlobalInstance(30000, MutexKey))
                {
                    ServiceLocator.Current.SetCurrentLifetimeScope(container);
                    RegisterTemplate(container);
                }
            }
            finally
            {
                ServiceLocator.Current.SetCurrentLifetimeScope(null);
            }
        }

        private void RegisterTemplate(IContainer container)
        {
            var unitsOfWork = container.GetAllInstances<IUnitOfWork>();
            var scopeService = container.GetInstance<ScopeService>();
            var scopeQuery = container.GetInstance<IDatabaseQuery>().GetQueryable<Scope>();

            if (scopeQuery.Any(x => x.Name == Name))
                return;

            scopeService.AddScope(Name, Description);

            foreach (var unitOfWork in unitsOfWork)
            {
                unitOfWork.Commit();
            }
        }
    }
}