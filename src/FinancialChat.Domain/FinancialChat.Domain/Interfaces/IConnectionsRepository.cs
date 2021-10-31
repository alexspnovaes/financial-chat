using FinancialChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Interfaces
{
    public interface IConnectionsRepository
    {
        void Add(string uniqueID, User user);
    }
}
