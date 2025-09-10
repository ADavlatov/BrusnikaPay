namespace BrusnikaPay.Models;

public class PaymentResponse
{
    public Result Result { get; set; } = default!;
    public PaymentData Data { get; set; } = default!;
}

public class Result
{
    public string Status { get; set; } = default!;
    public string Message { get; set; } = default!;
}

public class PaymentData
{
    public Guid Id { get; set; }
    public string Status { get; set; } = default!;
    public string LinkPaymentForm { get; set; } = default!;
}
