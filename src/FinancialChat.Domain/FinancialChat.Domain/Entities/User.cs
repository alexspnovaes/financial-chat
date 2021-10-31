using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FinancialChat.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; }

    }
}
