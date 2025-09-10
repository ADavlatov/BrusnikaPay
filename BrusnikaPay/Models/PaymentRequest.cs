namespace BrusnikaPay.Models;

public class PaymentRequest
{
    public string ClientID { get; set; } = default!;
    public string ClientIP { get; set; } = default!;
    public DateTime ClientDateCreated { get; set; }
    public string PaymentMethod { get; set; } = "toCard";
    public string IdTransactionMerchant { get; set; } = Guid.NewGuid().ToString();
    public decimal Amount { get; set; }
}
