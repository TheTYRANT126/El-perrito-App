using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Bridge
{
    public class CreditCardProcessor : IPaymentProcessor
    {
        private readonly Logger _logger = Logger.Instance;

        public async Task<bool> ProcessAsync(decimal amount)
        {
            _logger.LogInfo($"Procesando pago con tarjeta de crédito: ${amount}");
            await Task.Delay(100);
            return true;
        }

        public string GetProcessorName() => "Tarjeta de Crédito";
    }
}
