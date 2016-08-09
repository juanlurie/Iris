using Iris.Messaging;
using Iris.Security.OAuth.Server.Contracts;

namespace Iris.Security.OAuth.Server.Handlers
{
    public class UserCommandHandlers : IHandleMessage<LoginCredentialsUser>
                                      ,IHandleMessage<RegisterCredentialsUser>
    {
        private readonly UserService userService;

        public UserCommandHandlers(UserService userService)
        {
            this.userService = userService;
        }

        public void Handle(LoginCredentialsUser message)
        {
            userService.LoginCredentialsUser(message.SessionKey,message.Username, message.Password);
        }      
     
        public void Handle(RegisterCredentialsUser message)
        {
            userService.RegisterCredentialsAccount(message.UserId, message.Username, message.Password, message.GivenName, message.Surname,message.Email);
        }
    }
}