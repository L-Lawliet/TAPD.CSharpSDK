using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TAPD.CSharpSDK
{
    public class TAPDHttp
    {
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
                webRequest = WebRequest.Create(url) as HttpWebRequest;

                webRequest.Method = method;
                webRequest.Headers.Add("Authorization", authorization);
                
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
    }
}
