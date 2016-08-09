using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class LoginCredentialsUser : IDomainCommand
    {
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public Guid SessionKey { get; protected set; }

        public LoginCredentialsUser(string username, string password)
        {
            Username = username;
            SessionKey = SequentialGuid.New();
            Password = password;
        }
    }
}