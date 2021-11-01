
namespace FinancialChat.Domain.Models.Inputs
{
    public class UserInput
    {
        public string Id { get; set; }
        public bool IsOnline { get; set; } = false;
        public string Username { get; set; }
    }
}
