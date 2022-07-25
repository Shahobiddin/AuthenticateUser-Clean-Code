using System;
using System.Collections.Generic;
using System.Linq;
using Functions.Task1.ThirdParty;

namespace Functions.Task1
{
    public class AccountService
    {
        private const int AccountNameMinLength = 6;
        private const int AccountPasswordMaxLength = 9;

        private IPasswordChecker _passwordChecker;
        private IAccountManager _accountManager;

        public AccountService(IPasswordChecker passwordChecker, IAccountManager accountManager)
        {
            _passwordChecker = passwordChecker;
            _accountManager = accountManager;
        }

        public void Register(IAccount account)
        {
            ValidateAccount(account);

            SetAccountDetails(account);

            _accountManager.CreateNewAccount(account);
        }

        private void ValidateAccount(IAccount account)
        {
            ValidateNameForMinLength(account.GetName());
            ValidatePassword(
                   account.GetPassword(), 
                   password => password.Length < AccountPasswordMaxLength
            );
        }

        private void ValidateNameForMinLength(string name)
        {
            if (name.Length < AccountNameMinLength)
            {
                throw new WrongAccountNameException();
            }
        }

        private void ValidatePassword(string password, Func<string, bool> isPasswordCheckerRequired)
        {
            if (isPasswordCheckerRequired(password))
            {
                ValidatePasswordWithPasswordChecker(password);
            }
        }

        private void ValidatePasswordWithPasswordChecker(string password)
        {
            if (_passwordChecker.Validate(password) != CheckStatus.Ok)
            {
                throw new WrongPasswordException();
            }
        }

        private void SetAccountDetails(IAccount account)
        {
            account.SetCreatedDate(GetAccountCreatedDate());

            account.SetAddresses(GetAddresses(account));
        }

        private DateTime GetAccountCreatedDate()
        {
            return new DateTime();
        }

        private static IList<IAddress> GetAddresses(IAccount account)
        {
            return new[]
            {
                account.GetHomeAddress(),
                account.GetWorkAddress(),
                account.GetAdditionalAddress()
            };
        }
    }
}