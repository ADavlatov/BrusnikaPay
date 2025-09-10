using BrusnikaPay.Configuration;
using BrusnikaPay.Interfaces;
using BrusnikaPay.Models;
using Microsoft.Extensions.Options;

namespace BrusnikaPay.Services;

public class PaymentService(HttpClient httpClient, IOptions<PaymentOptions> options, ILogger<PaymentService> logger) : IPaymentService
{
    public async Task<PaymentResponse?> CreatePaymentAsync(PaymentRequest request)
    {
        if (string.IsNullOrEmpty(request.PaymentMethod))
            request.PaymentMethod = options.Value.DefaultPaymentMethod;
        
        logger.LogInformation("Creating payment for ClientID={ClientID}, Amount={Amount}, Method={Method}",
            request.ClientID, request.Amount, request.PaymentMethod);

        try
        {
            var response = await httpClient.PostAsJsonAsync(options.Value.EndpointUrl, request);

            logger.LogInformation("Received HTTP {StatusCode} from API", response.StatusCode);

            response.EnsureSuccessStatusCode();

            var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponse>();

            if (paymentResponse?.Result.Status == "success")
            {
                logger.LogInformation("Payment created successfully. PaymentId={PaymentId}, Link={Link}",
                    paymentResponse.Data.Id, paymentResponse.Data.LinkPaymentForm);
            }
            else
            {
                logger.LogWarning("Payment creation failed: {@Response}", paymentResponse);
            }

            return paymentResponse;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while creating payment for ClientID={ClientID}", request.ClientID);
            throw;
        }
    }
}
