using FinancialChat.Domain.Models;
using FinancialChat.Domain.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IDictionary<string, UserModel>> Get(int[] ids);
        Task<IDictionary<string, UserModel>> GetOnline();

        Task OnStartSession(UserInput user);
        Task OnStopSession(UserInput user);
    }
}
