using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Adapter
{
    public class StripePaymentAdapter : IPaymentGateway
    {
        private readonly StripePaymentService _stripeService;

        public StripePaymentAdapter()
        {
            _stripeService = new StripePaymentService();
        }

        public async Task<(bool success, string transactionId)> ProcessPaymentAsync(
            decimal amount,
            string cardNumber,
            string cvv)
        {
            // Convertir a centavos como requiere Stripe
            int amountInCents = (int)(amount * 100);

            // Simular tokenización de tarjeta
            string cardToken = $"tok_{cardNumber.Substring(cardNumber.Length - 4)}";

            string chargeId = await _stripeService.ChargeCard(cardToken, amountInCents);

            return (true, chargeId);
        }

        public async Task<bool> RefundAsync(string transactionId, decimal amount)
        {
            int amountInCents = (int)(amount * 100);
            return await _stripeService.CreateRefund(transactionId, amountInCents);
        }

        public string GetProviderName() => "Stripe";
    }
}
