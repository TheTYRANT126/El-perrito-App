using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Bridge
{
    public interface IPaymentProcessor
    {
        Task<bool> ProcessAsync(decimal amount);
        string GetProcessorName();
    }
}
