using Functions.Task3.ThirdParty;
using System;

namespace Functions.Task3
{
    public abstract class UserController : IController
    {
        private readonly UserAuthenticator userAuthenticator;

        protected UserController(UserAuthenticator userAuthenticator)
        {
            this.userAuthenticator = userAuthenticator;
        }

        public void AuthenticateUser(string userName, string password)
        {
            try
            {
                userAuthenticator.Login(userName, password);

                GenerateSuccessLoginResponse(userName);
            }
            catch
            {
                GenerateFailLoginResponse();
            }
        }

        public abstract void GenerateFailLoginResponse();

        public abstract void GenerateSuccessLoginResponse(string user);
    }
}