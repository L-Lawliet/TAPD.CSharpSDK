using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace TAPD.CSharpSDK
{
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
        public static T Request<T>(string url, string authorization = "", string data = "", string method = "Get", string contentType = "")
        {
            string content = Request(url, authorization, data, method, contentType);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        public static string Request(string url, string authorization = "", string data = "", string method = "Get", string contentType = "")
        {
            string content = "";

            HttpWebRequest webRequest = null;

            try
            {
                switch (method)
                {
                    case "Get":
                        webRequest = CreateGetRequest(url, data);
                        break;
                    case "Post":
                        break;
                    default:
                        return "";
                }

                webRequest.Method = method;
                webRequest.Headers.Add("Authorization", authorization);
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

        private static HttpWebRequest CreateGetRequest(string url, string data = "")
        {
            url = string.Format("{0}?{1}", url, data);

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

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

            Type type = typeof(T);

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                var ignoreAttribute = GetCustomAttributes<TAPDIgnoreAttribute>(property);

                if (ignoreAttribute != null)
                {
                    continue;
                }

                var propertyAttribute = GetCustomAttributes<TAPDPropertyNameAttribute>(property);

                var requiredAttribute = GetCustomAttributes<TAPDRequiredAttribute>(property);

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

        private static TAttribute GetCustomAttributes<TAttribute>(PropertyInfo propertyInfo) where TAttribute : Attribute
        {
            object[] attributes = propertyInfo.GetCustomAttributes(typeof(TAttribute), false);

            foreach (object attribute in attributes)
            {
                if(attribute.GetType() == typeof(TAttribute))
                {
                    return attribute as TAttribute;
                }
            }

            return null;
        }
    }
}
