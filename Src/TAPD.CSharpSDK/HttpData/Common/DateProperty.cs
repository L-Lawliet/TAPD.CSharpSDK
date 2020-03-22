using System;
using System.Globalization;

namespace TAPD.CSharpSDK
{
    public class DateProperty 
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private const string DATE_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// 日期格式信息
        /// </summary>
        private static DateTimeFormatInfo m_DateFormat = new DateTimeFormatInfo() { ShortDatePattern = DATE_FORMAT };

        /// <summary>
        /// 开始日期的字符串形式
        /// </summary>
        private string m_StartDateString;

        private DateTime m_StartDate;

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime startDate
        {
            get
            {
                return m_StartDate;
            }
            set
            {
                m_StartDate = value;

                m_StartDateString = value.ToString(DATE_FORMAT);
            }
        }

        /// <summary>
        /// 结束日期的字符串形式
        /// </summary>
        private string m_EndDateString;

        private DateTime m_EndDate;

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime endDate
        {
            get
            {
                return m_EndDate;
            }
            set
            {
                m_EndDate = value;

                m_EndDateString = value.ToString(DATE_FORMAT);
            }
        }

        /// <summary>
        /// 字符串形式的构造函数
        /// 参数格式必须为“yyyy-MM-dd”
        /// </summary>
        /// <param name="startDateString">开始日期的字符串形式</param>
        /// <param name="endDateString">结束日期的字符串形式</param>
        public DateProperty(string startDateString = "", string endDateString = "")
        {
            DateTime startDate;

            if (TryToDateTime(startDateString, out startDate))
            {
                m_StartDate = startDate;

                m_StartDateString = startDateString;
            }

            DateTime endDate;

            if (TryToDateTime(endDateString, out endDate))
            {
                m_EndDate = endDate;

                m_EndDateString = endDateString;
            }
        }

        /// <summary>
        /// 时间形式的构造函数
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        public DateProperty(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        /// <summary>
        /// 尝试将字符串转换成DateTime类型
        /// </summary>
        /// <param name="dateString">日期的字符串内容</param>
        /// <param name="dateTime">返回的DateTime日期</param>
        /// <returns>是否转换成功</returns>
        private bool TryToDateTime(string dateString, out DateTime dateTime)
        {
            bool result = false;

            try
            {
                dateTime = Convert.ToDateTime(dateString, m_DateFormat);

                result = true;
            }
            catch
            {
                dateTime = DateTime.Now;
            }

            return result;
        }

        /// <summary>
        /// 重写ToString
        /// 返回TAPD API要求的格式
        /// </summary>
        /// <returns>API的参数格式</returns>
        public override string ToString()
        {
            bool hasStart = !string.IsNullOrEmpty(m_StartDateString);
            bool hasEnd = !string.IsNullOrEmpty(m_EndDateString);

            if (hasStart && hasEnd)
            {
                return string.Format("{0}~{1}", m_StartDateString, m_EndDateString);
            }
            else if(hasStart)
            {
                return string.Format(">{0}", m_StartDateString);
            }
            else if (hasEnd)
            {
                return string.Format("<{0}", m_EndDateString);
            }

            return "";
        }
    }
}
