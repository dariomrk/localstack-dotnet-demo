using System.ComponentModel.DataAnnotations;
using System.Net;

using Amazon.SimpleEmail;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

var awsOptions = builder.Configuration.GetAWSOptions();

builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonSimpleEmailService>();

builder.WebHost.UseKestrelHttpsConfiguration();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("send-email", async ([FromBody] SendEmailRequest request, [FromServices] IAmazonSimpleEmailService ses) =>
{
    var sesResponse = await ses.SendEmailAsync(new()
    {
        Source = "sender@demo.app",
        Destination = new()
        {
            ToAddresses = [request.Recipient],
        },
        Message = new()
        {
            Body = new()
            {
                Text = new()
                {
                    Charset = "UTF-8",
                    Data = request.Content,
                }
            },
            Subject = new()
            {
                Charset = "UTF-8",
                Data = "Hello via LocalStack!"
            },
        }
    });

    if (sesResponse.HttpStatusCode is not HttpStatusCode.OK)
    {
        return Results.Ok(new StatusResponse($"Failed to send the email.", DateTime.UtcNow.Ticks));
    }

    return Results.Ok(new StatusResponse($"Successfully sent email to {request.Recipient}.", DateTime.UtcNow.Ticks));
});

app.MapGet("", () => Results.Ok(new StatusResponse("Application is running.", DateTime.UtcNow.Ticks)));

app.Run();

record SendEmailRequest([Required] string Recipient, [Required] string Content);
record StatusResponse(string Details, long TimeStamp);
