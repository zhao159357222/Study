using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Leo.FrameWork.Log
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    /// <remarks>
    /// 一条日志记录类对应一条记录
    /// </remarks>
    [Serializable]
    [XmlRoot("LogEntity")]
    public sealed class LogEntity : ICloneable
    {
        private LogPriority defaultPriority = LogPriority.Normal;
        private TraceEventType logEventType = TraceEventType.Resume;
        private int defaultEventID = 0;
        private Guid activityID = Guid.Empty;
        private string defaultTitle = string.Empty;
        private string message = string.Empty;
        private string stackTrace = string.Empty;
        private string source = string.Empty;
        private DateTime timeStamp = DateTime.MinValue;
        private string machineName = string.Empty;
        private IDictionary<string, object> extendedProperties = null;



        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}