using Functions.Task3.ThirdParty;
using System;

namespace Functions.Task3
{
    public abstract class UserAuthenticator : IUserService
    {
        private readonly ISessionManager sessionManager;

        protected UserAuthenticator(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        public void Login(string userName, string password)
        {
            LoginUser(GetUserByName(userName), password);
        }

        private void LoginUser(IUser user, string password)
        {
            if (IsNotPasswordCorrect(user, password))
            {
                throw new AuthenticationException();
            }
         
            sessionManager.SetCurrentUser(user);
        }

        private bool IsNotPasswordCorrect(IUser user, string password) => !IsPasswordCorrect(user, password);

        public abstract bool IsPasswordCorrect(IUser user, string password);

        public abstract IUser GetUserByName(string userName);
    }
}