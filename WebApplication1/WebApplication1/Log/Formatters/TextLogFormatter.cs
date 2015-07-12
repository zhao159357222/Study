using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Log.Formatters
{
    public class TextLogFormatter : LogFormatter
    {
        /// <summary>
        /// 格式模板
        /// </summary>
        private string template;
        /// <summary>
        /// Array of token formatters.
        /// </summary>
        private ArrayList tokenFunctions;

        private const string TimeStampToken = "{timestamp}";
        private const string MessageToken = "{message}";
        private const string PriorityToken = "{priority}";
        private const string EventIdToken = "{eventid}";
        private const string EventTypeToken = "{eventtype}";
        private const string TitleToken = "{title}";
        private const string MachineToken = "{machine}";
        private const string ActivityidToken = "{activity}";
        private const string NewLineToken = "{newline}";
        private const string TabToken = "{tab}";
        private const string StackTraceToken = "{stacktrace}";

        public override string Format(LogEntity log)
        {
            throw new NotImplementedException();
        }
    }
}