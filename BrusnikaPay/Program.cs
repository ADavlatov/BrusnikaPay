using System.Net.Http.Headers;
using BrusnikaPay.Configuration;
using BrusnikaPay.Interfaces;
using BrusnikaPay.Middleware;
using BrusnikaPay.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
});

builder.Services.Configure<PaymentOptions>(
    builder.Configuration.GetSection(PaymentOptions.SectionName));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IPaymentService, PaymentService>()
    .ConfigureHttpClient((sp, client) =>
    {
        var options = sp.GetRequiredService<IOptions<PaymentOptions>>().Value;

        client.BaseAddress = new Uri(options.BaseUrl);

        if (!string.IsNullOrWhiteSpace(options.AuthToken))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", options.AuthToken);
        }
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();