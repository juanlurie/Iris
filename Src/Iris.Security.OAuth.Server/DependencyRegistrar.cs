using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Iris.EntityFramework;
using Iris.Ioc;
using Iris.Security.OAuth.Server.Model;
using Iris.Security.OAuth.Server.QueryServices;
using Iris.Security.OAuth.Services;

namespace Iris.Security.OAuth.Server
{
    public class OAuthContext : FrameworkContext
    {
        public IDbSet<Accountability> Accountabilities { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<RoleScope> RoleScopes { get; set; }
        public IDbSet<Scope> Scopes { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserClaim> UserClaims { get; set; }


        public OAuthContext()
        {
        }

        public OAuthContext(string databaseName) : base(databaseName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<RoleScope>()
                       .HasRequired(p => p.Role)
                       .WithMany(p => p.RoleScopes)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleScope>()
                        .HasRequired(p => p.Scope)
                        .WithMany(p => p.RoleScopes)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class OAuthServerDependencyRegistrar : IRegisterDependencies
    {
        public void Register(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AccessTokenFactory>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<UserQueryService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<UserSessionQueryService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<UserService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<RoleService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<RoleQueryService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<RoleScopeQueryService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<RoleScopeService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<ScopeQueryService>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<ScopeService>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}