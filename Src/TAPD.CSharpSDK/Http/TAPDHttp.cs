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
        public static T Request<T>(string url, string data = "", string method = "", string contentType = "")
        {
            string content = Request(url, data, method, contentType);

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
        public static string Request(string url, string data = "", string method = "", string contentType = "")
        {
            string content = "";

            HttpWebRequest webRequest = null;

            try
            {
                webRequest = WebRequest.Create(url) as HttpWebRequest;

                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                
                if(webResponse != null)
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }

                    webResponse.Close();
                }
            }
            catch (WebException webException)
            {
                throw webException;
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
    }
}
