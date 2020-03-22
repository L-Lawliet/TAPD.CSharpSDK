using System;
using System.Text;

namespace TAPD.CSharpSDK
{
    public class TAPD
    {
        /// <summary>
        /// 身份验证信息
        /// </summary>
        private string m_Authorization;

        /// <summary>
        /// 身份验证信息
        /// </summary>
        public string authorization
        {
            get
            {
                return m_Authorization;
            }
        }

        private string m_ApiUser;

        /// <summary>
        /// API 账号
        /// </summary>
        public string apiUser
        {
            get
            {
                return m_ApiUser;
            }
            set
            {
                if(m_ApiUser != value)
                {
                    m_ApiUser = value;

                    SetAuthorization();
                }
            }
        }

        private string m_ApiPassword;

        /// <summary>
        /// API 口令
        /// </summary>
        public string apiPassword
        {
            get
            {
                return m_ApiPassword;
            }
            set
            {
                if (m_ApiPassword != value)
                {
                    m_ApiPassword = value;

                    SetAuthorization();
                }
            }
        }

        /// <summary>
        /// TAPD构建函数
        /// </summary>
        public TAPD()
        {

        }

        /// <summary>
        /// TAPD构建函数
        /// </summary>
        /// <param name="apiUser">账号</param>
        /// <param name="apiPassword">口令</param>
        public TAPD(string apiUser, string apiPassword)
        {
            m_ApiUser = apiUser;
            m_ApiPassword = apiPassword;

            SetAuthorization();
        }

        /// <summary>
        /// 请求需求数据
        /// </summary>
        /// <returns></returns>
        public TAPDResponse<TAPDStory[]> RequestStories()
        {
            return Request<TAPDStory[]>(TAPDHttpAPI.STORIES);
        }

        /// <summary>
        /// 请求协议
        /// </summary>
        /// <param name="method">API方法名</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回的包结构</returns>
        public TAPDResponse<T> Request<T>(string method = "")
        {
            string path = "";

            if (string.IsNullOrEmpty(method))
            {
                path = TAPDHttpAPI.BASE_URL;
            }
            else
            {
                path = string.Format("{0}/{1}", TAPDHttpAPI.BASE_URL, method);
            }

            TAPDResponse<T> response = TAPDHttp.Request<TAPDResponse<T>>(path, authorization);

            return response;
        }

        /// <summary>
        /// 设置签名
        /// </summary>
        private void SetAuthorization()
        {
            string userPassword = string.Format("{0}:{1}", m_ApiUser, m_ApiPassword);

            m_Authorization = string.Format("Basic {0}", EncodeBase64("utf-8", userPassword));
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="codeType">内容编码类型</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        private static string EncodeBase64(string codeType, string content)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(codeType).GetBytes(content);

            encode = Convert.ToBase64String(bytes);

            return encode;
        }

    }
}
