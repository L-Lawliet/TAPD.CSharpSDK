using System;
using System.Collections.Generic;

namespace TAPD.CSharpSDK
{
    public class PersonProperty
    {
        /// <summary>
        /// 或者的连接符
        /// </summary>
        private const char OR_CHAR = '|';

        /// <summary>
        /// 并且的连接符
        /// </summary>
        private const char AND_CHAR = ';';

        /// <summary>
        /// 人员名称组合
        /// </summary>
        public List<string> m_Persons;

        /// <summary>
        /// 是否使用并且
        /// </summary>
        public bool m_IsAnd = false;

        /// <summary>
        /// 是否使用并且
        /// </summary>
        public bool isAnd
        {
            get
            {
                return m_IsAnd;
            }
            set
            {
                m_IsAnd = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAnd">人员是否并且关系</param>
        public PersonProperty(bool isAnd = false)
        {
            m_IsAnd = isAnd;

            m_Persons  = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAnd">人员是否并且关系</param>
        /// <param name="persons">人员列表</param>
        public PersonProperty(bool isAnd = false, params string[] persons)
        {
            m_IsAnd = isAnd;

            m_Persons = new List<string>(persons);
        }

        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="person"></param>
        public void AddPerson(string person)
        {
            if (!m_Persons.Contains(person))
            {
                m_Persons.Add(person);
            }
        }

        /// <summary>
        /// 移除人员
        /// </summary>
        /// <param name="person"></param>
        public void RemovePerson(string person)
        {
            m_Persons.Remove(person);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";

            if(m_Persons != null && m_Persons.Count > 0)
            {
                if (m_IsAnd)
                {
                    result = StringUtil.Join<string>(AND_CHAR, m_Persons);
                }
                else
                {
                    result = StringUtil.Join<string>(OR_CHAR, m_Persons);
                }
            }

            return result;
        }
    }
}
