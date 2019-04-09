using System;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    public SendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    SmtpClient mailClient = new SmtpClient();
    MailMessage Email = new MailMessage();
    public void CreateMail(string sender, string receipt, String subject, String body)
    {

        Email = new MailMessage(sender, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
    }
    public bool sendMail()
    {
        try
        {
            mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            mailClient.Send(Email);
            return true;
        }
        catch (SmtpFailedRecipientException e)
        {
            return false;
        }
    }
    public bool sendMail(string host, Int32 port, string username, string password)
    {
        // for your information host is "smtp.rcpl.in" , port is 25, 587( tried both), username is "auto@rcpl.in"  pwd is "Mda*KTI8"
        NetworkCredential basicAuthenticationInfo;
        mailClient = new System.Net.Mail.SmtpClient(host, port);
        basicAuthenticationInfo = new System.Net.NetworkCredential(username, password);
        //'Put your own, or your ISPs, mail server name on this next line
        mailClient.EnableSsl = true;
        mailClient.UseDefaultCredentials = true;
        mailClient.Credentials = basicAuthenticationInfo;
        try
        {
            mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            mailClient.Send(Email);
            return true;
        }
        catch (SmtpFailedRecipientException e)
        {
            return false;
        }
    }
    public void CreateInvoiceMail(string s, string receipt, String subject, String body, string path, string path1)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (path != "")
        {
            Email.Attachments.Add(new Attachment(path));
        }
        if (path1 != "")
        {
            Email.Attachments.Add(new Attachment(path1));
        }
    }
    public void CreateMail(string s, string receipt, String subject, String body, string path)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (path != "")
        {
            Email.Attachments.Add(new Attachment(path));
        }
    }
    public void CreateMailCC(string From, string receipt, String subject, String body, String cc)
    {
        Email = new MailMessage(From, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        Email.To.Add(cc);
    }
}