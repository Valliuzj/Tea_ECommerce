using Microsoft.AspNetCore.Mvc;

public class SendEmailModel
{
    // [BindProperty]
    public string ToEmail { get; set; }

    // [BindProperty]
    public string Subject { get; set; }

    // [BindProperty]
    public string Body { get; set; }

}