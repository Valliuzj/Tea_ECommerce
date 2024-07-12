
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using tea.Models;

namespace tea.Controllers;

public class EmailController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendEmail([Bind("ToEmail,Subject,Body")] SendEmailModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                SendEmail(model.ToEmail, model.Subject, model.Body);
                ViewBag.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error sending email: " + ex.Message;
            }
        }

        return View("Index", model);
    }

    private void SendEmail(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Tea Corporation Support Team", "lichunxiajerry@163.com"));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { TextBody = body };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect("smtp.163.com", 465, SecureSocketOptions.SslOnConnect); 
                client.Authenticate("lichunxiajerry@163.com", "SWTJWNBRPPLLWOGA");
                client.Send(message);
                Console.WriteLine("Email sent successfully.");
            }
            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP Error: {ex.Message}");
                Console.WriteLine($"StatusCode: {ex.StatusCode}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
            }
            catch (SmtpProtocolException ex)
            {
                Console.WriteLine($"Protocol Error: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
            }
            finally
            {
                client.Disconnect(true);
            }
        }

    }

}