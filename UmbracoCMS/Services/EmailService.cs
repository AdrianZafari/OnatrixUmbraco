using Azure.Communication.Email;
using Azure;
namespace UmbracoCMS.Services;

public class EmailService // This section was made with the help of ChatGPT
{
    private readonly EmailClient _emailClient;
    private readonly string _senderAddress;

    public EmailService(IConfiguration config)
    {
        var acsConfig = config.GetSection("ACS");
        var connectionString = acsConfig["ConnectionString"];
        _senderAddress = acsConfig["SenderAddress"]!;

        _emailClient = new EmailClient(connectionString);
    }

    public async Task<bool> SendConfirmationEmailAsync(string toEmail, string userName)
    {
        try
        {
            var emailMessage = new EmailMessage(
                senderAddress: _senderAddress,
                content: new EmailContent("Thank you for your request")
                {
                    PlainText = $"Hello {userName}, thanks for your request.",
                    Html = $@"
                    <html>
                        <body>
                            <h2>Thank you, {userName}!</h2>
                            <p>We have received your request and will contact you soon.</p>
                        </body>
                    </html>"
                },
                recipients: new EmailRecipients(new List<EmailAddress>
                {
                new EmailAddress(toEmail)
                }));

            EmailSendOperation operation = await _emailClient.SendAsync(
                WaitUntil.Completed,
                emailMessage);

            return operation.HasCompleted; 
        }
        catch
        {
            return false;
        }
    }
}
