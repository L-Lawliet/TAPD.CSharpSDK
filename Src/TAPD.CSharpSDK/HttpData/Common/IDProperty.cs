using System;
using System.Collections.Generic;

namespace TAPD.CSharpSDK
{
    public class IDProperty
    {
        /// <summary>
        /// 或者的连接符
        /// </summary>
        private const char OR_CHAR = ',';

        /// <summary>
        /// ID组合
        /// </summary>
        public List<long> m_IDs;

        /// <summary>
        /// 
        /// </summary>
        public IDProperty()
        {
            m_IDs = new List<long>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        public IDProperty(params long[] ids)
        {
            m_IDs = new List<long>(ids);
        }

        /// <summary>
        /// 添加ID
        /// </summary>
        /// <param name="id"></param>
        public void AddID(long id)
        {
            if (!m_IDs.Contains(id))
            {
                m_IDs.Add(id);
            }
        }

        /// <summary>
        /// 移除ID
        /// </summary>
        /// <param name="id"></param>
        public void RemovePerson(long id)
        {
            m_IDs.Remove(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";

            if(m_IDs != null && m_IDs.Count > 0)
            {
                result = StringUtil.Join<long>(OR_CHAR, m_IDs);
            }

            return result;
        }
    }
}
