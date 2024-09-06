using System;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using static GlobalVariables.Variables;

public class EmailHandler
{

    public static void SendTestEmail(string toWho)
    {
        SendMail(toWho, "Test email", "This is a test email sent to check the functionality of the system. If you receive this message, the system is working as expected.");
    }

    public static void SendReminder()
    {
        SendMail(personalEmail, "Login Reminder", "You haven't logged in for " + reminderDay + " days. Please log in to avoid sending a false absence warning.");
    }

    public static void SendAbsenceNotification()
    {
        foreach (string recepient in contacts)
        {
            if (recepient != "")
                SendMail(recepient, contactsSubject, contactsMessage);
            Thread.Sleep(1000);
        }
    }

    public static void SendSpecialAbsenceNotification()
    {
        if(specialContacts != "")
        {
            SendMail(specialContacts, specialContactsSubject, specialContactsMessage);
            Thread.Sleep(1000);
        }
    }
    static void SendMail(string toWho, string subject, string message)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(personalEmail);
        mail.Subject = subject;

        mail.To.Add(new MailAddress(toWho));

        mail.Body = message;
        mail.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(personalEmail, emailPassword),
            EnableSsl = true,
        };

        smtpClient.Send(mail);
    }
}
