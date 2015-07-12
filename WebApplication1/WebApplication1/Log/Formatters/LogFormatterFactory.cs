using Leo.FrameWork.Log.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Log.Formatters
{
    internal class LogFormatterFactory
    {
        public static ILogFormatter GetFormatter(LogConfigurationElement formatterElement)
        {
            if (formatterElement != null)
            {
                try
                {
                    return new LogFormatter();
                }
                catch (Exception ex)
                {
                    throw new LogException("创建Formatter对象时出错：" + ex.Message, ex);
                }
            }
        }
    }
}