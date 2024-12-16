using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.Core.Configurations.Settings
{
    public class MailSettings : ISettings
    {
        public const string SectionName = "MailSettings";
        public string MailSendFrom { get; set; } = string.Empty;
        public string MailSendTo { get; set; } = string.Empty;
        public string MailSendCC { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MailSubject { get; set; } = string.Empty;
        public string SMTPServer { get; set; } = string.Empty;
        public int SMTPPort { get; set; }
    }
}