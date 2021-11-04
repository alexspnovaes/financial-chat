using FinancialChat.Domain.Models.Inputs;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Interfaces.ExternalServices
{
    public interface IStockApiExternalService
    {
        Task PostAsync(StockInput model);
    }
}
