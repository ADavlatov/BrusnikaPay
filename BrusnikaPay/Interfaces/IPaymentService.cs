using BrusnikaPay.Models;

namespace BrusnikaPay.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponse?> CreatePaymentAsync(PaymentRequest request);
}
