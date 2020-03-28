using System;
using System.Collections.Generic;

namespace TAPD.CSharpSDK
{
    public class EnumProperty
    {
        /// <summary>
        /// 或者的连接符
        /// </summary>
        private const char OR_CHAR = '|';

        /// <summary>
        /// 枚举组合
        /// </summary>
        public List<string> m_Enums;

        /// <summary>
        /// 
        /// </summary>
        public EnumProperty()
        {
            m_Enums = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enums"></param>
        public EnumProperty(params string[] enums)
        {
            m_Enums = new List<string>(enums);
        }

        /// <summary>
        /// 添加枚举
        /// </summary>
        /// <param name="enumValue"></param>
        public void AddEnum(string enumValue)
        {
            if (!m_Enums.Contains(enumValue))
            {
                m_Enums.Add(enumValue);
            }
        }

        /// <summary>
        /// 移除枚举
        /// </summary>
        /// <param name="enumValue"></param>
        public void RemovePerson(string enumValue)
        {
            m_Enums.Remove(enumValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";

            if(m_Enums != null && m_Enums.Count > 0)
            {
                result = StringUtil.Join<string>(OR_CHAR, m_Enums);
            }

            return result;
        }
    }
}
