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
    }
}
