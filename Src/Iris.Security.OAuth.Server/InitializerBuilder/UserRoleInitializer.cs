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
    [InitializationOrder(Order = 1)]
    public abstract class UserRoleInitializer : INeedToInitializeSomething
    {
        protected readonly string MutexKey;

        protected abstract string Name { get; }
        protected abstract string Description { get; }
        protected abstract Guid Id { get; }

        protected UserRoleInitializer()
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
            var roleService = container.GetInstance<RoleService>();
            var roleQuery = container.GetInstance<IDatabaseQuery>().GetQueryable<Role>();

            if (roleQuery.Any(x => x.Id == Id))
                return;

            roleService.AddRole(Id, Name, Description);

            foreach (var unitOfWork in unitsOfWork)
            {
                unitOfWork.Commit();
            }
        }
    }
}