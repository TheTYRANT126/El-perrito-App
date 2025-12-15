using System.Threading.Tasks;

namespace ElPerrito.Business.Patterns.Bridge
{
    public abstract class Payment
    {
        protected IPaymentProcessor _processor;

        protected Payment(IPaymentProcessor processor)
        {
            _processor = processor;
        }

        public abstract Task<bool> Pay(decimal amount);
    }

    public class OnlinePayment : Payment
    {
        public OnlinePayment(IPaymentProcessor processor) : base(processor) { }

        public override async Task<bool> Pay(decimal amount)
        {
            System.Console.WriteLine($"Iniciando pago en línea con {_processor.GetProcessorName()}");
            return await _processor.ProcessAsync(amount);
        }
    }

    public class RecurringPayment : Payment
    {
        private readonly int _installments;

        public RecurringPayment(IPaymentProcessor processor, int installments) : base(processor)
        {
            _installments = installments;
        }

        public override async Task<bool> Pay(decimal amount)
        {
            System.Console.WriteLine($"Iniciando pago recurrente en {_installments} cuotas con {_processor.GetProcessorName()}");
            decimal installmentAmount = amount / _installments;
            return await _processor.ProcessAsync(installmentAmount);
        }
    }
}
