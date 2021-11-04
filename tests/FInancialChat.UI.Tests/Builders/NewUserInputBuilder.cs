using FinancialChat.Domain.Entities;
using FinancialChat.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInancialChat.UI.Tests.Builders
{
    public class NewUserInputBuilder
    {
        private string _Username { get; set; }
        private string _Password { get; set; }
        private string _PasswordConfirmation { get; set; }
        private string _Email { get; set; }

        internal NewUserInputBuilder()
        {
            _Username = "user";
            _Password = "P@ssword1";
            _PasswordConfirmation = "P@ssword1";
            _Email = "user@user.com";
        }
       
        internal RegisterViewModel Build()
        {
            return new RegisterViewModel
            {
                UserName = _Username,
                Password = _Password,
                ConfirmPassword = _PasswordConfirmation,
                Email = _Email,
            };
        }
    }
}
