namespace BrusnikaPay.Configuration;

public class PaymentOptions
{
    public const string SectionName = "Payment";

    public string BaseUrl { get; set; } = default!;
    public string EndpointUrl { get; set; } = default!;
    public string DefaultPaymentMethod { get; set; } = "toCard";
    public string AuthToken { get; set; } = default!;
}