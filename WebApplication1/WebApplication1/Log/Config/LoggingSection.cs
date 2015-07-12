using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Leo.FrameWork.Log.Config
{
    /// <summary>
    /// 日志配置类
    /// </summary>
    public sealed class LoggingSection : ConfigurationSection
    {
        private LoggingSection()
        {

        }

        /// <summary>
        /// 获取日志配置节对象
        /// </summary>
        /// <returns></returns>
        public static LoggingSection GetConfig()
        {
            try
            {
                LoggingSection section = (LoggingSection)ConfigurationManager.GetSection("LoggingSettings");
                if (section == null)
                {
                    section = new LoggingSection();
                }
                return section;

            }
            catch (Exception ex)
            {
                if (ex is LogException)
                {
                    throw;
                }
                else
                {
                    throw new LogException("读取日志配置信息错误：" + ex.Message);
                }
            }
        }

        [ConfigurationProperty("Loggers")]
        public LoggerElementCollection Loggers
        {
            get
            {
                return (LoggerElementCollection)this["Loggers"];
            }
        }


    }
}