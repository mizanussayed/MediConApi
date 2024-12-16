using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.Core.Features.Mail.Model
{
    public class MailRequestModel
    {
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string[]? MailCCList { get; set; }
        public List<MailAttachment>? Attachments { get; set; }

    }

    public class MailAttachment
    {
        public MailAttachment
            (MemoryStream? AttachmentFile,string Name, string ApplicationType) { 
            this.Name = Name;
            this.AttachmentFile = AttachmentFile;
            this.ApplicationType = ApplicationType;
            }
        public string Name { get; set; } = string.Empty;
        public string ApplicationType { get; set; } = string.Empty;
        public MemoryStream? AttachmentFile { get; set; }
    }
}