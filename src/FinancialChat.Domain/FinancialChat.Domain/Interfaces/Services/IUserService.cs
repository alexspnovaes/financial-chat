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
        Task<List<string>> GetOnlineUsersButMeAsync(string roomId);
        Task OnStartSession(UserInput user, string roomId);
        Task OnStopSession(UserInput user, string roomId);
    }
}
