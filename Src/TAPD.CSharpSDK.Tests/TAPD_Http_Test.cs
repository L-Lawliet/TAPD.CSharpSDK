using NUnit.Framework;
using System;

namespace TAPD.CSharpSDK.Tests
{
    [TestFixture]
    public class TAPD_Http_Test
    {
        [Test]
        public void Request_API_Succeed()
        {
            var response = TAPDHttp.Request<TAPDResponse<string>>(TAPDHttpAPI.BASE_URL);

            Assert.AreEqual(response.status, TAPDHttpStatus.Succeed);
        }
    }
}
