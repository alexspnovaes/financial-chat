using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Infra.Data.Repositories
{
    public class ConnectionsRepository : IConnectionsRepository
    {
        private readonly Dictionary<string, User> connections =
            new();


        public void Add(string uniqueID, User user)
        {
            if (!connections.ContainsKey(uniqueID))
                connections.Add(uniqueID, user);
        }
    }
}
