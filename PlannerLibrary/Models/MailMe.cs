using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace PlannerLibrary.Models
{
    public static class MailMe
    {
        public static bool IsValidEmail(string userEmail)
        {
            return new EmailAddressAttribute().IsValid(userEmail);
        }

        public static bool SendMail(string userEmail)
        {
            bool flag = false;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("20103016smtpmail@gmail.com");
                mail.To.Add(userEmail.ToString());
                mail.Subject = "Study Zen OTP Pin";
                mail.Body = "<h1>Hello, Thank you for Registering with Study Zen.</h1>" +
                    "<p>Your OTP Pin is : <p> " +
                    "<p>" + Global.OneTimePin + "<p>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("20103016smtpmail@gmail.com", "P@ssword1.");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    Console.WriteLine("Mail successfully sent.");
                    flag = true;
                }
            }
            return flag;
        }
    }
}


/*
Code Attribution
Topic  : The SMTP server requires a secure connection or the client was not authenticated. 
         The server response was: 5.5.1 Authentication Required?
Author : mjb
Link   : https://stackoverflow.com/questions/18503333/the-smtp-server-requires-a-secure-connection-or-the-client-was-not-authenticated
	     [answered Aug 9 '14 at 6:20]
*/

/*
 * Code Attribution 
 *  Author : Manik Arora
 *  Topic : C# code to validate email address
 *  Code available at : https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address [answered Nov 26 '15 at 5:58]
 */

