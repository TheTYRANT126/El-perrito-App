using System.Threading.Tasks;
using ElPerrito.Core.Logging;

namespace ElPerrito.Business.Patterns.Bridge
{
    public class PayPalProcessor : IPaymentProcessor
    {
        private readonly Logger _logger = Logger.Instance;

        public async Task<bool> ProcessAsync(decimal amount)
        {
            _logger.LogInfo($"Procesando pago con PayPal: ${amount}");
            await Task.Delay(150);
            return true;
        }

        public string GetProcessorName() => "PayPal";
    }
}
