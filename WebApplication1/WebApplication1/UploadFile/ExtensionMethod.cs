using System.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace Leo.FrameWork.Common
{
    /// <summary>
    /// 整理归纳常用的扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(value, out result);
            return result;
        }
        /// <summary>
        /// 如果字符串为空字符串，则返回null,否则返回原字符串
        /// 为了个给Filter生成过滤字段使用的扩展方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFilterString(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// 将一个字符串转换为Filter的字符串，如果为String.Empty，就返回null
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isFrom">是否是开始时间秒</param>
        /// <returns></returns>
        public static string ToFilterDateString(this string value, bool isFrom = true)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            var secondString = " 00:00:00";
            if (!isFrom)
            {
                secondString = " 23:59:59";
            }
            return string.Format("{0}{1}", value.Trim(), secondString);
        }

        public static string ToFilterDateStringWithDefaultValue(this string value, DateTime defaultDateTime, bool isFrom = true)
        {
            var resultValue = ToFilterDateString(value, isFrom);
            if (string.IsNullOrEmpty(resultValue))
            {
                return defaultDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return resultValue;
        }

        /// <summary>
        /// 将一个字符串转换为FilterInt，目的是下拉框用，如果选中的是 ”请选择“项，就返回Int.MinValue
        /// </summary>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Int32 ToFilterInt32(this string value, string condition = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return int.MinValue;
            }
            return value == condition ? int.MinValue : value.ToInt32();
        }

        public static string ToFilterInt32ChangeToString(this string value, string condition = "")
        {
            return ToFilterInt32(value, condition).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 扩展foreach方法
        /// </summary>
        /// <typeparam name="T">集合中元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="action">执行的方法</param>
        /// <returns>执行后的集合</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
            return list;
        }

        /// <summary>
        /// 将字符串转换成为int
        /// </summary>
        /// <param name="context">要转换的字符串</param>
        /// <returns>转换后的数字</returns>
        public static int ToInt(this string context)
        {
            var t = 0;
            int.TryParse(context, out t);
            return t;
        }
        /// <summary>
        /// 将字符串转换为Int32（写这个方法是因为 2.00这种格式，用int.TryParse是转换不了的，所以我先转换成decimal类型）
        /// </summary>
        /// <param name="context">要转换的字符串</param>
        /// <returns></returns>
        public static int ToInt32(this string context)
        {
            decimal decimalResult;
            decimal.TryParse(context, out decimalResult);
            return (int)decimalResult;
        }
        /// <summary>
        /// 将字符串转换成为bool
        /// </summary>
        /// <param name="context">要转换的字符串</param>
        /// <returns>转换后的布尔值</returns>
        public static bool ToBool(this string context)
        {
            var t = false;
            bool.TryParse(context, out t);
            return t;
        }

        /// <summary>
        /// 将对象的属性名和属性值，组成key/value的形式的xml数据
        /// ！！注意：暂时不能处理属性是集合的情况
        /// </summary>
        /// <param name="t">对象</param>
        /// <returns>转换完成的xml</returns>
        public static XElement ToXml<T>(this T t) where T : class
        {
            return t.ToXml("root");
        }

        /// <summary>
        /// 将对象的属性名和属性值，组成key/value的形式的xml数据
        /// ！！注意：暂时不能处理属性是集合的情况
        /// </summary>
        /// <param name="t">对象</param>
        /// <returns>转换完成的xml</returns>
        public static XElement ToXml<T>(this T t, string rootName) where T : class
        {
            var root = new XElement(rootName);
            var type = t.GetType();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(t);
                var element = new XElement(name, value);
                root.Add(element);
            }
            return root;
        }

        /// <summary>
        /// 初始化开始时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>开始时间为00:00:00</returns>
        public static DateTime ToStartDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// 初始化结束时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结束时间为24:59:59</returns>
        public static DateTime ToEndDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        /// <summary>
        /// decimal类型保留两位小数
        /// 千分位分割，保留两位小数，单位元
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatString(this decimal value)
        {
            if (value != 0)
            {
                return value.ToString("N");
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 验证DateTime时间是否小于sql中的最小时间，如果小于，则使用sql中的最小时间
        /// </summary>
        /// <param name="date">输入时间</param>
        /// <returns>验证过的时间</returns>
        public static DateTime ValidateSqlMinDate(this DateTime date)
        {
            var minDateTime = Convert.ToDateTime(SqlDateTime.MinValue.ToString());
            if (date < minDateTime)
            {
                date = minDateTime;
            }
            return date;
        }
        /// <summary>
        /// 验证DateTime时间是否小于sql中的最小时间，如果小于，则使用sql中的最小时间
        /// </summary>
        /// <param name="date">输入时间</param>
        /// <returns>验证过的时间</returns>
        public static DateTime ValidateSqlMaxDate(this DateTime date)
        {
            var maxDateTime = Convert.ToDateTime(SqlDateTime.MaxValue.ToString());
            if (date > maxDateTime)
            {
                date = maxDateTime;
            }
            return date;
        }
        ///// <summary>
        ///// 从资源中加载字符串
        ///// </summary>
        ///// <param name="assembly"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string LoadStringFromResource(this Assembly assembly, string path)
        //{
        //    using (Stream stm = GetResourceStream(assembly, path))
        //    {
        //        using (StreamReader sr = new StreamReader(stm))
        //        {
        //            return sr.ReadToEnd();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 从资源中得到流
        ///// </summary>
        ///// <param name="assembly"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static Stream GetResourceStream(Assembly assembly, string path)
        //{
        //    ExceptionHelper.FalseThrow<ArgumentNullException>(assembly != null, "assembly");
        //    ExceptionHelper.CheckStringIsNullOrEmpty(path, "path");

        //    Stream stm = assembly.GetManifestResourceStream(path);

        //    ExceptionHelper.FalseThrow(stm != null, "不能在Assembly:{0}中找到资源{1}", assembly.FullName, path);

        //    return stm;
        //}

        /// <summary>
        /// 将时间转换为时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToTimeStamp(this DateTime datetime)
        {
            var ts = datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static string ToUTF8Encoding(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return HttpUtility.UrlEncode(str);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
