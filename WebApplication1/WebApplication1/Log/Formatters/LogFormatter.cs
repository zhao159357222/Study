using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Log.Formatters
{
    /// <summary>
    /// 抽象基类，实现ILogFormatter接口
    /// </summary>
    /// <remarks>
    /// 所有LogFormatter的基类，
    /// 派生时，为使定制的Formatter支持可配置，必须在该派生类中实现参数为LogConfigurationElement对象的构造函数
    /// </remarks>
    public abstract class LogFormatter : ILogFormatter
    {
        public string name = string.Empty;

        public string Name
        {
            get { return this.name; }
        }

        public LogFormatter();

        /// <summary>
        /// 构造函数，根据Name创建LogFormatter对象
        /// </summary>
        /// <param name="formattername">Formatter的名称</param>\
        /// <remarks>
        /// formattername参数不能为空，否则抛出异常
        /// </remarks>
        public LogFormatter(string formattername)
        {
            //TODO 添加异常检查。
            this.name = formattername;
        }
        /// <summary>
        /// 抽象方法，格式化LogEntity对象成一个字符串
        /// </summary>
        /// <param name="log">待格式化的LogEntity对象</param>
        /// <returns>格式化成的字符串</returns>
        /// <remarks>
        /// 由派生类具体实现
        /// </remarks>
        public abstract string Format(LogEntity log);
    }
}