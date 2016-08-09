using System;
using System.Linq;
using Iris.Attributes;
using Iris.Ioc;
using Iris.Messaging;
using Iris.Messaging.Configuration;
using Iris.Persistence;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server.InitializerBuilder
{
    [InitializationOrder(Order = 3)]
    public abstract class RoleScopeInitializer : INeedToInitializeSomething
    {
        protected readonly string MutexKey;

        protected abstract string Scope { get; }
        protected abstract Guid RoleId { get; }

        protected RoleScopeInitializer()
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
            var roleScopeService = container.GetInstance<RoleScopeService>();
            var roleScopeQuery = container.GetInstance<IDatabaseQuery>().GetQueryable<RoleScope>();

            if (roleScopeQuery.Any(x => x.RoleId == RoleId && x.ScopeValue == Scope))
                return;

            roleScopeService.AddRoleScope(RoleId, Scope);

            foreach (var unitOfWork in unitsOfWork)
            {
                unitOfWork.Commit();
            }
        }
    }
}