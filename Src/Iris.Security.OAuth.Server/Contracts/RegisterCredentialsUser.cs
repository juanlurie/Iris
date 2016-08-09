using System;
using Iris.Messaging;

namespace Iris.Security.OAuth.Server.Contracts
{
    public class RegisterCredentialsUser : IDomainCommand
    {
        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }
        public string Surname { get; protected set; }
        public string GivenName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }

        public RegisterCredentialsUser(string username, string givenName, string surname, string email, string password)
        {
            Mandate.ParameterNotNullOrEmpty(username, "username");
            Mandate.ParameterNotNullOrEmpty(givenName, "givenName");
            Mandate.ParameterNotNullOrEmpty(surname, "surname");
            Mandate.ParameterNotNullOrEmpty(email, "email");
            Mandate.ParameterNotNullOrEmpty(password, "password");

            UserId = SequentialGuid.New();
            GivenName = givenName;
            Username = username;
            Surname = surname;
            Email = email;
            Password = password;
        }
    }
}