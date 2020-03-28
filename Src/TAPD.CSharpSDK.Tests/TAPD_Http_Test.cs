using NUnit.Framework;
using System;

namespace TAPD.CSharpSDK.Tests
{
    [TestFixture]
    public class TAPD_Http_Test
    {
        /// <summary>
        /// 授权编码成功
        /// </summary>
        [Test]
        public void Encode_Authorization_Succeed()
        {
            TAPD tapd = new TAPD("api_user","api_password");

            Assert.AreEqual(tapd.authorization, "Basic YXBpX3VzZXI6YXBpX3Bhc3N3b3Jk");
        }

        /// <summary>
        /// 请求API成功
        /// </summary>
        [Test]
        public void Request_API_Succeed()
        {
            var response = TAPDHttp.Request<TAPDResponse<string>>(TAPDHttpAPI.BASE_URL);

            Assert.AreEqual(response.status, TAPDHttpStatus.Succeed);
        }

        /// <summary>
        /// 请求API授权成功
        /// 由于保密原因，因此不提供账号和密钥进行测试。
        /// 请向TAPD申请账号和密钥，不然会导致测试失败。
        /// </summary>
        [Test]
        public void Request_API_Authorized()
        {
            TAPD tapd = new TAPD(TAPDTestAuthorization.API_USER, TAPDTestAuthorization.API_PASSWORD);

            var response = tapd.Request<object>("quickstart/testauth");

            Assert.AreEqual(response.status, TAPDHttpStatus.Succeed);
        }

        /// <summary>
        /// 请求API没有授权
        /// </summary>
        [Test]
        public void Request_API_Unauthorized()
        {
            TAPD tapd = new TAPD("null", "null");

            var response = tapd.Request<string>("quickstart/testauth");

            Assert.AreEqual(response.status, TAPDHttpStatus.Unauthorized);
        }

        /// <summary>
        /// Http参数拼接成功测试
        /// </summary>
        /// <param name="workspaceID"></param>
        /// <param name="intValue"></param>
        /// <param name="stringValue"></param>
        /// <param name="floatValue"></param>
        /// <param name="resetNameValue"></param>
        /// <param name="requiredValue"></param>
        /// <returns></returns>
        [TestCase(12345678, 100, "TestString", 0.618f, 312, "A", ExpectedResult = new string[] { "floatValue=0.618", "intValue=100", "requiredValue=A", "reset_name_value=312", "stringValue=TestString", "workspace_id=12345678" })]
        [TestCase(12345678, 100, null, 0.618f, 312, "A", ExpectedResult = new string[] { "floatValue=0.618", "intValue=100", "requiredValue=A", "reset_name_value=312", "workspace_id=12345678" })]
        public string[] HttpParameters_Join_Succeed(int workspaceID, int intValue, string stringValue, float floatValue, int resetNameValue, string requiredValue)
        {
            var request = new TAPDTestRequest();

            request.intValue = intValue;
            request.stringValue = stringValue;
            request.floatValue = floatValue;
            request.resetNameValue = resetNameValue;
            request.requiredValue = requiredValue;

            string result = TAPDHttp.JoinHttpParameters(workspaceID, request);

            string[] stringList = result.Split('&');

            Array.Sort(stringList);

            return stringList;
        }

        /// <summary>
        /// Http参数缺少必要参数异常测试
        /// </summary>
        [Test]
        public void HttpParameters_Join_Required_Missing_Exception()
        {
            TAPDRequiredParameterMissingException exception = null;

            try
            {
                var request = new TAPDTestRequest();

                string result = TAPDHttp.JoinHttpParameters(12345678, request);
            }
            catch (TAPDRequiredParameterMissingException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}
