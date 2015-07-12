using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Leo.FrameWork.Log.Config
{
    /// <summary>
    /// LoggersElement集合
    /// </summary>
    public sealed class LoggerElementCollection : ConfigurationElementCollection
    {
        private LoggerElementCollection()
        {

        }
        /// <summary>
        /// 创建新的LoggerElement对象
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggerElement();
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggerElement)element).Name;
        }
        /// <summary>
        /// 根据键值索引，返回LoggerElement对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public LoggerElement this[string name]
        {
            get
            {
                return (LoggerElement)base.BaseGet(name);
            }
        }
    }
}