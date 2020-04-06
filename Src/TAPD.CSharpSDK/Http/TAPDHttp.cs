using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD Http请求实现类
    /// 
    /// note:
    ///     1. POST请求下，需要先设置Authorization之后，再写入参数，不然服务器无法识别Authorization
    /// </summary>
    public class TAPDHttp
    {
        private static StringBuilder m_StringBuilder = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        public static TAPDResponse<T> Request<T>(string url, JsonConverter converter, string authorization = "", string data = "", TAPDHttpMethod method = TAPDHttpMethod.Get, string contentType = "")
        {
            string content = Request(url, authorization, data, method, contentType);

            TAPDResponse<T> result = JsonConvert.DeserializeObject<TAPDResponse<T>>(content, converter);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        public static TAPDResponse<T> Request<T>(string url, string authorization = "", string data = "", TAPDHttpMethod method = TAPDHttpMethod.Get, string contentType = "")
        {
            string content = Request(url, authorization, data, method, contentType);

            TAPDResponse<T> result = JsonConvert.DeserializeObject<TAPDResponse<T>>(content);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        public static string Request(string url, string authorization = "", string data = "", TAPDHttpMethod method = TAPDHttpMethod.Get, string contentType = "")
        {
            string content = "";

            HttpWebRequest webRequest = null;

            try
            {
                switch (method)
                {
                    case TAPDHttpMethod.Get:
                        webRequest = CreateGetRequest(url, authorization, data);
                        break;
                    case TAPDHttpMethod.Post:
                        webRequest = CreatePostRequest(url, authorization, data);
                        break;
                    default:
                        return "";
                }

                //webRequest.Method = Enum.GetName(typeof(TAPDHttpMethod), method);
                //webRequest.Headers.Add("Authorization", authorization);
                webRequest.ContentType = contentType;

                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;

                content = ReadWebResponse(webResponse);
            }
            catch (WebException webException)
            {
                HttpWebResponse webResponse = webException.Response as HttpWebResponse;

                content = ReadWebResponse(webResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }

            return content;
        }

        /// <summary>
        /// 创建Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authorization"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static HttpWebRequest CreateGetRequest(string url, string authorization, string data = "")
        {
            url = string.Format("{0}?{1}", url, data);

            Console.WriteLine(url);

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

            webRequest.Headers.Add("Authorization", authorization);

            webRequest.Method = "Get";

            return webRequest;
        }

        /// <summary>
        /// 创建Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authorization"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static HttpWebRequest CreatePostRequest(string url, string authorization, string data = "")
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

            webRequest.Headers.Add("Authorization", authorization);

            webRequest.Method = "Post";

            webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            Console.WriteLine(data);

            var byteData = Encoding.UTF8.GetBytes(data);
            var length = byteData.Length;
            webRequest.ContentLength = length;
            var writer = webRequest.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();

            return webRequest;
        }


        /// <summary>
        /// 读取返回的数据
        /// </summary>
        /// <param name="webResponse"></param>
        /// <returns></returns>
        private static string ReadWebResponse(HttpWebResponse webResponse)
        {
            string content = null;

            if (webResponse != null)
            {
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }

                webResponse.Close();
            }

            return content;
        }

        /// <summary>
        /// 拼接Http参数
        /// </summary>
        /// <param name="workspaceID"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string JoinHttpParameters<T>(int workspaceID, T request) where T : TAPDRequest
        {
            m_StringBuilder.Length = 0;

            m_StringBuilder.AppendFormat("workspace_id={0}", workspaceID);

            if(request == null)
            {
                return m_StringBuilder.ToString();
            }

            Type type = request.GetType();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                var ignoreAttribute = ReflectionUtil.GetCustomAttributes<TAPDIgnoreAttribute>(property);

                if (ignoreAttribute != null)
                {
                    continue;
                }

                var propertyAttribute = ReflectionUtil.GetCustomAttributes<TAPDPropertyNameAttribute>(property);

                var requiredAttribute = ReflectionUtil.GetCustomAttributes<TAPDRequiredAttribute>(property);

                bool hasProperty = false;

                if (property != null && property.CanRead)
                {
                    object value = property.GetValue(request, null);

                    if(value != null)
                    {
                        string valueString = value.ToString();

                        if (!string.IsNullOrEmpty(valueString))
                        {
                            string name = property.Name;

                            if (propertyAttribute != null && !string.IsNullOrEmpty(propertyAttribute.name))
                            {
                                name = propertyAttribute.name;
                            }

                            m_StringBuilder.AppendFormat("&{0}={1}", name, valueString);

                            hasProperty = true;
                        }
                    }
                }

                if (!hasProperty && requiredAttribute != null)
                {
                    if(property != null)
                    {
                        throw new TAPDRequiredParameterMissingException(property.Name);
                    }
                    else
                    {
                        throw new TAPDRequiredParameterMissingException("unknown property");
                    }
                    
                }
            }

            string result = m_StringBuilder.ToString();

            return result;
        }
    }
}
