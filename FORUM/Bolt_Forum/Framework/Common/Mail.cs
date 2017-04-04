#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Com.Comm100.Framework.Common
{
    public class Mail
    {
        public static void SendEmail(string fromMail, string fromName, string toMail, string ToName, string mailSubject, string mailBody,
            bool isBodyHtml, string smtpclient, string username, string password, bool enablessl,int port)
        {
            //create a MailMessage object
            MailMessage mail = new MailMessage();

            //Define the sender and recipient
            mail.From = new MailAddress(fromMail, fromName);
            mail.To.Add(new MailAddress(toMail, ToName));

            //Define the subject and body
            mail.Subject = mailSubject;
            mail.Body = mailBody;
            mail.IsBodyHtml = isBodyHtml;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            //configure the mail server
            SmtpClient smtpClient = new SmtpClient(smtpclient);
            smtpClient.EnableSsl = enablessl;
            smtpClient.Port = port;
            smtpClient.Credentials = new NetworkCredential(username, password);

            //send the message and notify the user of success
            smtpClient.Send(mail);
        }
    }
}
