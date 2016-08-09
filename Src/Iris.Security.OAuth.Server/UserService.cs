using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using Iris.EntityFramework;
using Iris.Security.OAuth.Server.Exceptions;
using Iris.Security.OAuth.Server.Model;

namespace Iris.Security.OAuth.Server
{
    public class UserService
    {
        private const string DefaultRole = "User";

        private readonly IDbSet<User> users;
        private readonly IDbSet<Role> roles;
        private readonly IDbSet<Accountability> accountabilities;

        public UserService(IRepositoryFactory repositoryFactory)
        {
            users = repositoryFactory.GetRepository<User>();
            roles = repositoryFactory.GetRepository<Role>();
            accountabilities = repositoryFactory.GetRepository<Accountability>();
        }

        public void LoginCredentialsUser(Guid sessionKey,  string username, string password)
        {
            User account = FetchUser(username);

            ValidateCredentialsAccountLogin( username, account);

            VerifyPassword(account.HashedPassword, password);

            account.LastLogin = DateTime.Now;
            account.SessionKey = sessionKey;
        }

        public void RegisterCredentialsAccount(Guid userId, string username, string password, string givenName, string surname, string emailAddress)
        {
            //we are not yet encrypting passwords as this type of login is mainly used for test
            RegisterAccount(userId, username, Encoding.UTF8.GetBytes(password), true, givenName, surname, emailAddress);
        }

        public void RegisterAccount(Guid userId, string username, byte[] hashedPassword, bool isWindowsAccount, string givenName, string surname, string emailAddress)
        {
            User account = FetchUser(username);

            if (account != null)
                throw new UserRegistrationException(String.Format("User {0} has already been registered.", username));

            account = new User
            {
                Id = userId,
                Username = username,
                FailedLoginCount = 0,
                FailedPasswordResetCount = 0,
                IsAccountVerified = true,
                IsLoginAllowed = true,
                IsWindowsAccount = isWindowsAccount,
                RequiresPasswordReset = false,
                HashedPassword = hashedPassword,
                SessionKey = SequentialGuid.New(),
                FirstName = givenName,
                Surname = surname,
                Email = emailAddress
            };

            AddDefaultAccountability(account);

            users.Add(account);
        }

        public void LoginWindowsUserById(Guid userId, Guid sessionKey,  string username, string password = "")
        {
            User account = FetchUserById(userId);

            ValidateWindowsAccountLogin( username, account);

            account.LastLogin = DateTime.Now;
            account.SessionKey = sessionKey;
        }

        private void AddDefaultAccountability(User account)
        {
            var defaultRole = roles.First();

            var accountability = new Accountability
            {
                Id = SequentialGuid.New(),
                UserId = account.Id,
                StartOfAccountability = DateTime.Now,
                EndOfAccountability = DateTime.MaxValue,
                RoleId = defaultRole.Id,
                IsActive = true
            };

            accountabilities.Add(accountability);
        }

        private static void ValidateWindowsAccountLogin(string username, User account)
        {
            if (account == null)
            {
                throw new WindowsUserNotRegisteredException(String.Format("User {0} is not registered.",  username));
            }

            if (!account.IsAccountVerified)
            {
                throw new WindowsUserNotVerfiedException(String.Format("User {0} is not verified.",  account.Username));
            }

            if (!account.IsLoginAllowed)
            {
                throw new AuthenticationException(String.Format("Logon is denied for account {0}",  account.Username));
            }

            if (account.RequiresPasswordReset)
            {
                throw new AuthenticationException(String.Format("Account {0} requires a password reset.", account.Username));
            }
        }

        private static void ValidateCredentialsAccountLogin(string username, User account)
        {
            if (account == null)
            {
                throw new CredentialsUserNotRegisteredException(String.Format("User {0} is not registered.",  username));
            }

            if (!account.IsAccountVerified)
            {
                throw new CredentialsUserNotVerfiedException(String.Format("User {0} is not verified.", account.Username));
            }

            if (!account.IsLoginAllowed)
            {
                throw new CredentialsUserLoginDeniedException(String.Format("Logon is denied for account {0}", account.Username));
            }

            if (account.RequiresPasswordReset)
            {
                throw new CredentialsUserPasswordResetException(String.Format("Account {0} requires a password reset.", account.Username));
            }
        }

        private User FetchUser(string username)
        {
            return users.SingleOrDefault(a => a.Username == username);
        }

        private void VerifyPassword(byte[] hashedPassword, string password)
        {
            if (hashedPassword == null || String.IsNullOrWhiteSpace(password))
                throw new AuthenticationException("No password was provided or the user may not login using credentials.");

            string decodedPassword = Encoding.UTF8.GetString(hashedPassword);

            if (password.Equals(decodedPassword, StringComparison.Ordinal))
            {
                return;
            }

            throw new AuthenticationException("Invalid password.");
        }

        private User FetchUserById(Guid userId)
        {
            return users.Find(userId);
        }
    }
}