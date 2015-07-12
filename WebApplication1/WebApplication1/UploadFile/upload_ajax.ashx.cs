using Leo.FrameWork.Common.Web;
using Leo.FrameWork.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace Cfld.Conference.Web
{
    /// <summary>
    /// upload_ajax 的摘要说明
    /// </summary>
    public class upload_ajax : IHttpHandler
    {
        /// <summary>
        /// 删除附件信息
        /// </summary>
        /// <param name="BusinessAttachmentID"></param>
        /// <returns></returns>
        private AjaxUploadResult DeleteAttachment(string businessAttachmentID)
        {
            Guid BusinessAttachmentID = businessAttachmentID.ToGuid();
            if (!string.IsNullOrEmpty(businessAttachmentID) && BusinessAttachmentID != Guid.Empty)
            {
                try
                {
                    //调用删除附件表中的信息的方法
                    //AttachmentOperator.Instance.RemoveAttachmentt(BusinessAttachmentID);
                    return new AjaxUploadResult("success", "");
                }
                catch (Exception ex)
                {
                    return new AjaxUploadResult("error", ex.Message);
                }
            }
            else
            {
                return new AjaxUploadResult("error", "参数有误");
            }
        }

        private List<Attachments> GetAttachments(HttpFileCollection files)
        {
            List<Attachments> list = new List<Attachments>();
            if (files != null && files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile userPostedFile = files[i];
                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                            list.Add(new FileUploadHelper(userPostedFile).SaveHttpPostedFile());
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return list;
        }


        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection files = context.Request.Files;
            string action = context.Request["action"];
            string BusinessType = context.Request["BusinessType"];
            string BusinessID = context.Request["BusinessID"];
            string BusinessAttachmentID = context.Request["BusinessAttachmentID"];
            string AlwaysDelete = context.Request["AlwaysDelete"];
            string DeleteWithType = context.Request["DeleteWithType"];
            AjaxUploadResult result = new AjaxUploadResult();
            if (action == "delete")
            {
                result = DeleteAttachment(BusinessAttachmentID);
                context.Response.Write(result);
            }
            else
            {
                try
                {
                    List<Attachments> list = GetAttachments(files);
                    if (list.Count == 0)
                    {
                        context.Response.Write(new AjaxUploadResult("error", "不允许上传空文件！"));
                    }

                    if (!string.IsNullOrEmpty(action) && action.ToLower() == "save")
                    {
                        if (!string.IsNullOrEmpty(AlwaysDelete) && AlwaysDelete.ToLower() == "true")
                        {
                            if (!string.IsNullOrEmpty(DeleteWithType) && DeleteWithType.ToLower() == "true")
                            {
                                //根据BusinessID和BusinessType移除数据
                                //AttachmentOperator.Instance.RemoveByBIDAndType(BusinessID.ToGuid(), BusinessType);
                            }
                            else
                            {
                                //根据BusinessID移除数据
                                //AttachmentOperator.Instance.RemoveByBID(BusinessID.ToGuid());
                            }
                        }
                        else
                        {
                            if (BusinessAttachmentID != "" && BusinessAttachmentID != Guid.Empty.ToString())
                            {
                                try
                                {
                                    //从数据库中移除数据
                                    //AttachmentOperator.Instance.RemoveAttachmentt(Guid.Parse(BusinessAttachmentID));
                                }
                                catch (Exception ex)
                                { }
                            }
                        }
                        //保存数据到数据库
                        //LoginUserInfo userInfo = WebHelper.Instance.GetLoginUserInfo();
                        //foreach (var item in list)
                        //{
                        //    Attachment model = new Attachment();
                        //    model.BusinessID = BusinessID.ToGuid();
                        //    model.BusinessType = BusinessType;
                        //    model.FileName = item.OriginalFileName;
                        //    model.Address = item.FileServerPath;
                        //    model.Size = item.Size;
                        //    model.ID = Guid.Parse(item.AttachmentID);
                        //    model.CreateTime = DateTime.Parse(item.CreateTime);
                        //    model.Url = item.Url;
                        //    model.CreatorID = userInfo.UserID;
                        //    model.CreatorName = userInfo.Name;
                        //    model.CreateTime = DateTime.Now;
                        //    AttachmentOperator.Instance.ADDAttachmentModel(model);
                        //}
                    }
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new AjaxUploadResult
                        {
                            status = "success",
                            Attachment = list
                        });
                    context.Response.Write(json);
                }
                catch (Exception ex)
                {
                    context.Response.Write(new AjaxUploadResult("error", ex.Message));
                }
            }
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 上传后返回的对象
    /// </summary>
    public class AjaxUploadResult
    {
        public AjaxUploadResult()
        {
        }
        public AjaxUploadResult(string status, string msg)
        {
            this.status = status;
            this.msg = msg;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachments> Attachment { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 序列化后的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return json;
        }
    }
}