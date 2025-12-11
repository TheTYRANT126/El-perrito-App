using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Adapter
{
    /// <summary>
    /// Interfaz común para gateways de pago
    /// </summary>
    public interface IPaymentGateway
    {
        Task<(bool success, string transactionId)> ProcessPaymentAsync(decimal amount, string cardNumber, string cvv);
        Task<bool> RefundAsync(string transactionId, decimal amount);
        string GetProviderName();
    }
}
