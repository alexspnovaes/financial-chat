using FinancialChat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterAsync(UserModel model);
    }
}
