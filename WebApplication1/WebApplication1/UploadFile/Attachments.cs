using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Common.Web
{
    [Serializable]
    public class Attachments
    {
        public Attachments()
        {
            CreateTime = DateTime.Now.ToString();
        }
        public string AttachmentID { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileServerPath { get; set; }
        public string Url { get; set; }
        public string Size { get; set; }
        public string CreatorName { get; set; }
        public string CreateTime { get; set; }
    }
}