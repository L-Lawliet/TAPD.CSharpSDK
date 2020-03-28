using System;
using System.Collections.Generic;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// 基于整型的枚举
    /// </summary>
    public class EnumIntProperty : EnumProperty<int>
    {
        public EnumIntProperty(): base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enums"></param>
        public EnumIntProperty(params int[] enums):base(enums)
        {
            
        }
    }

    /// <summary>
    /// 基于字符串的枚举属性
    /// </summary>
    public class EnumStringProperty : EnumProperty<string>
    {
        public EnumStringProperty() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enums"></param>
        public EnumStringProperty(params string[] enums) : base(enums)
        {

        }
    }

    /// <summary>
    /// 枚举属性基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumProperty<T>
    {
        /// <summary>
        /// 或者的连接符
        /// </summary>
        private const char OR_CHAR = '|';

        /// <summary>
        /// 枚举组合
        /// </summary>
        public List<T> m_Enums;

        /// <summary>
        /// 
        /// </summary>
        public EnumProperty()
        {
            m_Enums = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enums"></param>
        public EnumProperty(params T[] enums)
        {
            m_Enums = new List<T>(enums);
        }

        /// <summary>
        /// 添加枚举
        /// </summary>
        /// <param name="enumValue"></param>
        public void AddEnum(T enumValue)
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
        public void RemovePerson(T enumValue)
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
                result = StringUtil.Join<T>(OR_CHAR, m_Enums);
            }

            return result;
        }
    }
}
