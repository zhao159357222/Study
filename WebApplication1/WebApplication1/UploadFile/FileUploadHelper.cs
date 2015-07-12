using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Leo.FrameWork.Common.Web
{
    public enum ProcessFileType
    {
        HttpPostedFile = 1,
        FileInfo = 2,
    }

    public class FileAttachmentInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileRenameName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileLength { get; set; }
        /// <summary>
        /// 文件原始名称
        /// </summary>
        public string OriginalFileName { get; set; }
        /// <summary>
        /// 文件大小（字符串）
        /// </summary>
        public string FileSize
        {
            get
            {
                string attachmentsize = string.Empty;
                if (FileLength > 1024 * 1024)
                {
                    attachmentsize = (FileLength / (1024 * 1024)).ToString("N") + " M";
                }
                else
                {
                    attachmentsize = (FileLength / (1024)).ToString("N") + " KB";
                }
                return attachmentsize;
            }
        }
    }

    public class FileUploadHelper
    {
        #region 配置属性
        private string UploadFilePathAll
        {
            get
            {
                string filePath = ConfigurationManager.AppSettings["AttachmentPath"];
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ApplicationException("未在配置中指定上传文件的的保存路径(AttachmentPath)");
                }
                return Path.Combine(filePath, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            }
        }
        private string UploadFileUrl
        {
            get
            {
                return "/public/download.aspx?path1=" + DateTime.Now.Year.ToString() + "&path2=" + DateTime.Now.Month.ToString() + "&id=";
            }
        }
        #endregion

        #region 属性
        private HttpPostedFile HttpPostedFile { get;  set; }
        private ProcessFileType ProcessFileType { get;  set; }
        private string FilePath { get;  set; }
        private FileInfo FileInfo { get;  set; }
        private FileAttachmentInfo FileAttachmentInfo { get;  set; }
        #endregion

        #region 构造函数
        public FileUploadHelper(HttpPostedFile file)
        {
            HttpPostedFile = file;
            ProcessFileType = Web.ProcessFileType.HttpPostedFile;
            FileAttachmentInfo = new FileAttachmentInfo();
            FileAttachmentInfo.FileLength = file.ContentLength;
            FileAttachmentInfo.OriginalFileName = file.FileName;
            FileAttachmentInfo.FileRenameName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(FileAttachmentInfo.OriginalFileName), DateTime.Now.ToString("yyyymmddhhMMss").Trim(), Path.GetExtension(FileAttachmentInfo.OriginalFileName));
        }

        public FileUploadHelper(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            ProcessFileType = Web.ProcessFileType.FileInfo;
            FileAttachmentInfo.FileLength = FileInfo.Length;
            FileAttachmentInfo.OriginalFileName = FileInfo.Name;
            FileAttachmentInfo.FileRenameName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(FileAttachmentInfo.OriginalFileName), DateTime.Now.ToString("yyyymmddhhMMss").Trim(), Path.GetExtension(FileAttachmentInfo.OriginalFileName));
        }

        public FileUploadHelper(string filePath)
        {
            try
            {
                FileInfo fileInfo = new System.IO.FileInfo(filePath);
                FileInfo = fileInfo;
                ProcessFileType = Web.ProcessFileType.FileInfo;
                FileAttachmentInfo.FileLength = FileInfo.Length;
                FileAttachmentInfo.OriginalFileName = FileInfo.Name;
                FileAttachmentInfo.FileRenameName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(FileAttachmentInfo.OriginalFileName), DateTime.Now.ToString("yyyymmddhhMMss").Trim(), Path.GetExtension(FileAttachmentInfo.OriginalFileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 私有方法
        #endregion

        public string GetStoreagePath()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["UseRemoteFileServer"] == "true")
            {
                NetworkSharedFolder.Connect(System.Configuration.ConfigurationManager.AppSettings["AttachmentPath"],
                    System.Configuration.ConfigurationManager.AppSettings["FileServer_LocalPath"],
                    System.Configuration.ConfigurationManager.AppSettings["FileServer_UserName"],
                    System.Configuration.ConfigurationManager.AppSettings["FileServer_UserPassword"]);
            }
            if (!Directory.Exists(UploadFilePathAll))
            {
                Directory.CreateDirectory(UploadFilePathAll);
            }
            string storeagePath = Path.Combine(UploadFilePathAll, FileAttachmentInfo.FileRenameName);
            return storeagePath;
        }

        public Attachments SaveHttpPostedFile()
        {
            Guid ID = Guid.NewGuid();

            string storeagePath = GetStoreagePath();
            HttpPostedFile.SaveAs(storeagePath);

            Attachments Attachment = new Attachments
            {
                AttachmentID = ID.ToString(),
                FileName = FileAttachmentInfo.FileRenameName,
                OriginalFileName = FileAttachmentInfo.OriginalFileName,
                FileServerPath = storeagePath,
                Size = FileAttachmentInfo.FileSize,
                Url = UploadFileUrl + ID.ToString()
            };
            return Attachment;
        }
    }
}
