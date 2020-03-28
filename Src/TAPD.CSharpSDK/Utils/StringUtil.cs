using System;
using System.Collections.Generic;
using System.Text;

namespace TAPD.CSharpSDK
{
    public class StringUtil
    {
        private static StringBuilder m_StringBuilder = new StringBuilder(128);

        /// <summary>
        /// 合并字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join<T>(Char separator, IEnumerable<T> values)
        {
            m_StringBuilder.Length = 0;

            int n = 0;

            foreach (var item in values)
            {
                if (n != 0)
                {
                    m_StringBuilder.Append(separator);
                }

                m_StringBuilder.Append(item.ToString());

                n++;
            }

            string result = m_StringBuilder.ToString();

            m_StringBuilder.Length = 0;

            return result;
        }

        /// <summary>
        /// 合并字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Join<T>(String separator, IEnumerable<T> values)
        {
            m_StringBuilder.Length = 0;

            int n = 0;

            foreach (var item in values)
            {
                if(n != 0)
                {
                    m_StringBuilder.Append(separator);
                }
               
                m_StringBuilder.Append(item.ToString());

                n++;
            }

            string result = m_StringBuilder.ToString();

            m_StringBuilder.Length = 0;

            return result;
        }
    }
}
