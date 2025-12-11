using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Bridge
{
    /// <summary>
    /// Patrón Bridge - Implementación de procesamiento de pago
    /// </summary>
    public interface IPaymentProcessor
    {
        Task<bool> ProcessAsync(decimal amount);
        string GetProcessorName();
    }
}
