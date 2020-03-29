using NUnit.Framework;
using System;

namespace TAPD.CSharpSDK.Tests
{
    /// <summary>
    /// 需求API测试
    /// </summary>
    [TestFixture]
    public class TAPD_Stories_Test
    {
        private TAPD m_Tapd;

        /// <summary>
        /// 初始化TAPD
        /// 需要填写正确的账号、密码和项目ID
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            m_Tapd = new TAPD(TAPDTestAuthorization.API_USER, TAPDTestAuthorization.API_PASSWORD, TAPDTestProjectSetting.workspaceID);
        }

        /// <summary>
        /// 获取需求列表测试
        /// </summary>
        [Test]
        public void Stories_Get_Succeed()
        {
            TAPDResponse<TAPDStory[]> response = m_Tapd.RequestStories();

            Assert.AreNotEqual(response.data, null);
            Assert.AreNotEqual(response.data.Length, 0);
            Assert.AreEqual(response.data[0].workspaceID, TAPDTestProjectSetting.workspaceID.ToString());

            //TODO：后续断言与项目需求有关，因此注释掉，可根据需求内容设置
            //Assert.AreEqual(response.data[0].creator, "XXX");

        }

        /// <summary>
        /// 根据ID查询
        /// 需要填写正确的需求ID
        /// </summary>
        /// <param name="id">查询的需求ID</param>
        [TestCase(100000000)]
        [TestCase(100000001)]
        [TestCase(100000002)]
        public void Stories_GetByID_Succeed(long id)
        {
            TAPDStoriesRequest request = new TAPDStoriesRequest();
            request.id = new IDProperty(id);

            TAPDResponse<TAPDStory[]> response = m_Tapd.RequestStories(request);

            Assert.AreNotEqual(response.data, null);
            Assert.AreNotEqual(response.data.Length, 0);
            Assert.AreEqual(response.data[0].id, id.ToString());
        }
    }
}
