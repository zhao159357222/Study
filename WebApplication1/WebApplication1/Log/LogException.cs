using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Log
{
    /// <summary>
    /// 日志异常类
    /// </summary>
    public sealed class LogException : ApplicationException
    {
        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public LogException()
            : base()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public LogException(string message) :
            base(message)
        {

        }
        /// <summary>
        /// 重载的构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="ex">原始异常对象</param>
        public LogException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}