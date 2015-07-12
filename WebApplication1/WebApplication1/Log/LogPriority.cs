using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Leo.FrameWork.Log
{
    /// <summary>
    /// 日志优先级
    /// </summary>
    /// <remarks>
    /// 共分5个优先级
    /// </remarks>
    public enum LogPriority
    {
        /// <summary>
        /// 最低
        /// </summary>
        [Description("最低")]
        Lowest = 1,

        /// <summary>
        /// 低
        /// </summary>
        [Description("低")]
        BelowNormal = 2,

        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        Normal = 3,

        /// <summary>
        /// 高
        /// </summary>
        [Description("高")]
        AboveNormal = 4,

        /// <summary>
        /// 最高
        /// </summary>
        [Description("最高")]
        Highest = 5

    }
}