using System.Net;
using System.Net.Mail;

using MediCon.Core.Configurations.Settings;
using MediCon.Core.Features.Mail.Model;

using Lib.ErrorOr;

using Serilog;
namespace MediCon.Core.Features.Mail.Service
{
    public class MailService
    {
        private readonly ISettingsHelper _settingsHelper;
        public MailService( ISettingsHelper settingsHelper) : base()
        {
            _settingsHelper = settingsHelper;
        }

        public async Task<ErrorOr<Success>> SendEmail(MailRequestModel model, CancellationToken cancellationToken)
        {
            try
            {
                //string fileName = "abc.xls";
                MailSettings? mailSettings = new MailSettings();
                mailSettings = _settingsHelper.Get<MailSettings>(MailSettings.SectionName);
                if (mailSettings != null)
                {
                    //===>>>
                    //
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailSettings.MailSendFrom);

                    //mail.To.Add(model.To);
                    if (mailSettings.MailSendTo != null && mailSettings.MailSendTo != "NA" && mailSettings.MailSendTo != "")
                    {
                        mail.To.Add(mailSettings.MailSendTo);
                        //mail.CC.Add(mailSettings.MailSendCC);
                        Log.Information("mail to: " + mail.To[0].ToString());
                        //Log.Information("mail cc: " + mail.CC[0].ToString());
                    }

                    //if (model.MailCCList != null)
                    //{
                    //    foreach (String ccItem in model.MailCCList)
                    //    {
                    //        mail.CC.Add(ccItem.Trim());
                    //    }
                    //}

                    // Create the attachment for the email
                    if (model.Attachments != null && model.Attachments.Count > 0)
                    {
                        foreach (var item in model.Attachments)
                        {
                            //mail.Attachments.Add(item.Trim());
                            if (item.AttachmentFile != null)
                            {
                                mail.Attachments.Add(new Attachment(item.AttachmentFile, item.Name, item.ApplicationType));
                            }
                        }
                    }
                    //set the content
                    mail.Subject = model.Subject;
                    mail.IsBodyHtml = true;
                    // mail.Body = GetUserWiseMailBody(userid) + GetNoteForMailBody();
                    mail.Body = model.Body;
                    //Log.Warning("mail to: " + mail.To[0].ToString());

                    //==
                    // Embed the image using CID
                    //var logoPath = @"wwwroot/BL Icons/logo.png";
                    //var logo = new LinkedResource(logoPath)
                    //{
                    //    ContentId = "logoImage"  // Set a unique Content-ID
                    //};
                    //var alternateView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                    //alternateView.LinkedResources.Add(logo);
                    //mail.AlternateViews.Add(alternateView);
                    //==

                    // SmtpClient smtpClient = new SmtpClient("172.16.7.57", 25);
                    SmtpClient smtpClient = new SmtpClient(mailSettings.SMTPServer);
                    smtpClient.Port = mailSettings.SMTPPort;

                    //== bongobondhuovidhan@gmail.com
                    //smtpClient.Credentials = new NetworkCredential(mailSettings.MailSendFrom, mailSettings.Password);
                    //smtpClient.EnableSsl = true;
                    //smtpClient.UseDefaultCredentials = false;
                    //==

                    if (!String.IsNullOrEmpty(mail.Body))
                    {
                        if (mail.To.Count > 0)
                        {
                            smtpClient.Send(mail);
                            Log.Information("Mail sent successfully.");
                        }
                     
                   
                    }
                }
                return SuccessType.Success;
            }
            catch (Exception ex)
            {

                Log.Error("Failed to send email. Error is : " + ex.Message);
                return Error.Failure(ex.Message);
            }          
        }

        //private string GetNoteForMailBody()
        //{

        //    String Note = "<span style='background-color: #FFFF00'><font size='2' font-family: 'Times New Roman'><br/>Note:   " +
        //           "<br/>1.	All KPIs (except Network and Data KPI) are updated on T-1   " +
        //           "<br/>2.	Network KPIs are updated on T-2.  " +
        //           "<br/>3.	Data Volume and Data Revenue (GPRS) KPIs are updated on T-4.  " +
        //           "<br/>4.	BTS Attachment Vs KPI: <br/> </font></span>";


        //    Note = Note + Environment.NewLine + Environment.NewLine + "<table border=1 cellpadding=3 cellspacing=0 style='border: 1pt solid #000000; border-Collapse: collapse'>" +
        //        "<tr bgcolor='#FFFF00'>" + "<td><font size='1.5' font-family: 'Times New Roman'> BTS Attachment </font></td>" + "<td><font size='2' font-family: 'Times New Roman'>KPI</font></td>" + "</tr>" +
        //        "<tr bgcolor='#FFFF00'>" + "<td><font size='1.5' font-family: 'Times New Roman'> First Call BTS (First Call done under BTS) </font></td>" + "<td><font size='1.5' font-family: 'Times New Roman'>GROSS_ADD</font></td>" + "</tr>" +
        //        "<tr bgcolor='#FFFF00'>" + "<td><font size='1.5' font-family: 'Times New Roman'> General BTS (Spends most time under the BTS) </font></td>" + "<td><font size='1.5' font-family: 'Times New Roman'>CHURN, REACTIVATION</font></td>" + "</tr>" +
        //        "<tr bgcolor='#FFFF00'>" + "<td><font size='1.5' font-family: 'Times New Roman'> Actual / Current BTS (Currently using BTS )	</font></td>" + "<td><font size='1.5' font-family: 'Times New Roman'>DATA_REVENUE, OUTGOING_REVENUE, INCOMING_REVENUE,<br/> SMS_INCOMING_REVENUE,  SMS_OUTGOING_REVENUE</font></td>" + "</tr></table>";


        //    Note = Note + "<br/><br/><font size='1' font-family: 'Times New Roman'>This email and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom it is addressed. It may also be legally privileged. If you are not the named addressee, you should not copy, forward, disclose or use any part of it. If you have received this message in error, please delete it and all copies from your system and notify the sender immediately by return e-mail. Internet communications cannot be guaranteed to be timely, secure, error or virus-free. The sender does not accept liability for any errors or omissions.</font>";

        //    return Note;
        //}


        //private string GetUserWiseMailBody(int userid)
        //{

        //    string mailBody = "";
        //    dal_dmsRoleWiseUser daldmsRoleWiseUser = new dal_dmsRoleWiseUser();
        //    dal_SalesDashboardInfo dalSalesDashboardInfo = new dal_SalesDashboardInfo();
        //    String distributorList = daldmsRoleWiseUser.GetUserWiseDistributors(userid);

        //    if (!String.IsNullOrEmpty(distributorList))
        //    {
        //        List<be_SalesDashboardMailBody> beSalesDashboardMailBody = dalSalesDashboardInfo.GetUserWiseMailBody(distributorList);

        //        string val_name = "gross_add";

        //        mailBody = "<br/><br/><table border=1 cellpadding=3 cellspacing=0 style='border: 1pt solid #000000; border-Collapse: collapse'>";

        //        mailBody = mailBody + "<tr  bgcolor='#fc6924'>" + "<td colspan='5' style='text-align:center'><font size='2' font-family: 'Times New Roman'>" + val_name.ToUpper() + "</font></td>" + "</tr>";
        //        mailBody = mailBody + "<tr  bgcolor='#fc6924'>" + "<td><font size='2' font-family: 'Times New Roman'> Rd Region</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>HRS_B2C</font></td>" +
        //                       "<td><font size='2' font-family: 'Times New Roman'>DD Code</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>BTS Code</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>" + val_name.ToUpper() + "</font></td>" + "</tr>";

        //        foreach (be_SalesDashboardMailBody item in beSalesDashboardMailBody)
        //        {
        //            if (val_name != item.Val_name)
        //            {
        //                val_name = item.Val_name;

        //                mailBody = mailBody + "</table><br/><br/><br/><table border=1 cellpadding=3 cellspacing=0 style='border: 1pt solid #000000; border-Collapse: collapse'>";
        //                mailBody = mailBody + "<tr bgcolor='#fc6924'>" + "<td colspan='5' style='text-align:center'><font size='2' font-family: 'Times New Roman'>" + val_name.ToUpper() + "</font></td>" + "</tr>";
        //                mailBody = mailBody + "<tr  bgcolor='#fc6924'>" + "<td><font size='2' font-family: 'Times New Roman'> Rd Region</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>HRS_B2C</font></td>" +
        //                        "<td><font size='2' font-family: 'Times New Roman'>DD Code</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>BTS Code</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>" + val_name.ToUpper() + "</font></td>" + "</tr>";
        //            }

        //            mailBody = mailBody + "<tr>" + "<td><font size='2' font-family: 'Times New Roman'>" + item.Rd_region + "</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>" + item.HRS_B2C + "</font></td>" +
        //                        "<td><font size='2' font-family: 'Times New Roman'>" + item.Distributor_code + "</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>" + item.Bts_code + "</font></td>" + "<td><font size='2' font-family: 'Times New Roman'>" + item.Mtd.ToString() + "</font></td>" + "</tr>";

        //        }

        //        mailBody = mailBody + "</table>";
        //    }


        //    return mailBody;

        //}

    }
}
