using Newtonsoft.Json;
using System;
using System.Text;

namespace TAPD.CSharpSDK
{
    public class Tapd
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

        public int m_WorkspaceID;

        /// <summary>
        /// 项目ID
        /// 原则上，单个TAPD类只能操控单个项目，以免两个项目请求混淆
        /// </summary>
        public int workspaceID
        {
            get
            {
                return m_WorkspaceID;
            }
            set
            {
                m_WorkspaceID = value;
            }
        }

        /// <summary>
        /// TAPD构建函数
        /// </summary>
        public Tapd()
        {

        }

        /// <summary>
        /// TAPD构建函数
        /// </summary>
        /// <param name="apiUser">账号</param>
        /// <param name="apiPassword">口令</param>
        public Tapd(string apiUser, string apiPassword, int workspaceID = 0)
        {
            m_ApiUser = apiUser;
            m_ApiPassword = apiPassword;
            m_WorkspaceID = workspaceID;

            SetAuthorization();
        }

        /// <summary>
        /// 请求需求数据
        /// </summary>
        /// <returns></returns>
        public TAPDResponse<TAPDStory[]> RequestStories(TAPDStoriesRequest request = null)
        {
            return Request<TAPDStory[]>(TAPDHttpAPI.STORIES, request, new TAPDConverter<TAPDStory>(TAPDHttpPropertyName.STORY));
        }

        /// <summary>
        /// 查询需求数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TAPDResponse<Int32> RequestStoriesCount(TAPDStoriesCountRequest request = null)
        {
            return Request<Int32>(TAPDHttpAPI.STORIES_COUNT, request, new TAPDConverter<Int32>(TAPDHttpPropertyName.COUNT));
        }

        /// <summary>
        /// 创建需求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TAPDResponse<TAPDStory> RequestCreateStory(TAPDCreateStoryRequest request)
        {
            return Request<TAPDStory>(TAPDHttpAPI.STORIES, request, new TAPDConverter<TAPDStory>(TAPDHttpPropertyName.STORY));
        }

        /// <summary>
        /// 创建需求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TAPDResponse<TAPDStory> RequestChangeStory(TAPDChangeStoryRequest request)
        {
            return Request<TAPDStory>(TAPDHttpAPI.STORIES, request, new TAPDConverter<TAPDStory>(TAPDHttpPropertyName.STORY));
        }


        /// <summary>
        /// 请求协议
        /// </summary>
        /// <param name="api">API方法名</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回的包结构</returns>
        public TAPDResponse<T> Request<T>(string api = "", TAPDRequest request = null, JsonConverter converter = null)
        {
            string path = "";

            string data = "";

            TAPDHttpMethod method = TAPDHttpMethod.Get;

            //拼接参数
            data = TAPDHttp.JoinHttpParameters<TAPDRequest>(m_WorkspaceID, request);

            if (string.IsNullOrEmpty(api))
            {
                path = TAPDHttpAPI.BASE_URL;
            }
            else
            {
                path = string.Format("{0}{1}", TAPDHttpAPI.BASE_URL, api);
            }

            if(request != null)
            {
                //获取请求的http参数
                TAPDHttpAttribute httpAttribute = ReflectionUtil.GetCustomAttributes<TAPDHttpAttribute>(request.GetType());

                if (httpAttribute != null)
                {
                    method = httpAttribute.method;
                }
            }

            TAPDResponse<T> response;

            if (converter != null)
            {
                response = TAPDHttp.Request<T>(path, converter, authorization, data, method);
            }
            else
            {
                response = TAPDHttp.Request<T>(path, authorization, data, method);
            }

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
