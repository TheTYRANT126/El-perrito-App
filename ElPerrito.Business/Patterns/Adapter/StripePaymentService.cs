using System;
using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Adapter
{
    /// <summary>
    /// Servicio externo de Stripe con su propia interfaz
    /// </summary>
    public class StripePaymentService
    {
        public async Task<string> ChargeCard(string cardToken, int amountInCents)
        {
            await Task.Delay(100); // Simular llamada API
            return $"stripe_tx_{Guid.NewGuid():N}";
        }

        public async Task<bool> CreateRefund(string chargeId, int amountInCents)
        {
            await Task.Delay(50);
            return true;
        }
    }
}
