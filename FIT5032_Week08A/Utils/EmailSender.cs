using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendAttachment(String toEmail, String subject, String contents, string path, string filename)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var bytes = File.ReadAllBytes(path);
            msg.AddAttachment(path, Convert.ToBase64String(bytes));

            var response = client.SendEmailAsync(msg);
        }
    }
}