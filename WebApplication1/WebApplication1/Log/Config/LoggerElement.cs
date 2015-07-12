using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Leo.FrameWork.Log.Config
{
    /// <summary>
    /// Logger配置节点
    /// <remarks>
    /// Logger配置节对象，包含Filters和Listeners集合对象
    /// </remarks>
    /// </summary>
    public class LoggerElement : ConfigurationElement
    {
        internal LoggerElement();

        /// <summary>
        /// Logger的名称
        /// </summary>
        /// <remarks>
        /// 键值，必配项
        /// </remarks>
        [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }
        [ConfigurationProperty("enable")]
        public bool IsEnabled
        {
            get
            {
                return (bool)this["enable"];
            }
        }
    }
}