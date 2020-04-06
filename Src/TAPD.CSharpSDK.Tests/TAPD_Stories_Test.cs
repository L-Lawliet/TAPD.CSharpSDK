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
            m_Tapd = new TAPD(TAPDTestAuthorization.API_USER, TAPDTestAuthorization.API_PASSWORD, TAPDTestProjectSetting.WORK_SPACE_ID);
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
            Assert.AreEqual(response.data[0].workspaceID, TAPDTestProjectSetting.WORK_SPACE_ID.ToString());

            //TODO：后续断言与项目需求有关，因此注释掉，可根据需求内容设置
            Assert.AreEqual(response.data[0].creator, TAPDTestProjectSetting.CREATOR);

        }

        /// <summary>
        /// 根据ID查询
        /// 需要填写正确的需求ID
        /// </summary>
        /// <param name="id">查询的需求ID</param>
        [TestCase(TAPDTestProjectSetting.STORY_ID_1)]
        [TestCase(TAPDTestProjectSetting.STORY_ID_2)]
        [TestCase(TAPDTestProjectSetting.STORY_ID_3)]
        public void Stories_GetByID_Succeed(long id)
        {
            TAPDStoriesRequest request = new TAPDStoriesRequest();
            request.id = new IDProperty(id);

            TAPDResponse<TAPDStory[]> response = m_Tapd.RequestStories(request);

            Assert.AreNotEqual(response.data, null);
            Assert.AreNotEqual(response.data.Length, 0);
            Assert.AreEqual(response.data[0].id, id.ToString());
        }

        /// <summary>
        /// 根据ID查询需求的数量
        /// </summary>
        /// <param name="id">查询的需求ID</param>
        [TestCase(1111111111111111111, ExpectedResult = 0)]  //错误的ID
        [TestCase(TAPDTestProjectSetting.STORY_ID_1, ExpectedResult = 1)]
        [TestCase(TAPDTestProjectSetting.STORY_ID_2, TAPDTestProjectSetting.STORY_ID_1, ExpectedResult = 2)]
        [TestCase(TAPDTestProjectSetting.STORY_ID_1, TAPDTestProjectSetting.STORY_ID_2, TAPDTestProjectSetting.STORY_ID_3, ExpectedResult = 3)]
        public int StoriesCount_GetByID_Succeed(params long[] ids)
        {
            TAPDStoriesCountRequest request = new TAPDStoriesCountRequest();
            request.id = new IDProperty(ids);

            TAPDResponse<int> response = m_Tapd.RequestStoriesCount(request);

            return response.data;
        }

        /// <summary>
        /// 创建需求
        /// 测试需谨慎，因为没有删除的API，需要手动删除
        /// </summary>
        [TestCase("New Story 1")]
        [TestCase("New Story 2", TAPDTestProjectSetting.STORY_ID_1)]
        public void CreateStory_Succeed(string name, long parentID = 0)
        {
            TAPDCreateStoryRequest request = new TAPDCreateStoryRequest();

            request.name = name;

            request.creator = "TAPD.SDK";

            if (parentID != 0)
            {
                request.parentID = parentID;
            }

            TAPDResponse<TAPDStory> response = m_Tapd.RequestCreateStory(request);

            Assert.IsNotNull(response.data.id);
        }

        /// <summary>
        /// 修改需求的名称
        /// </summary>
        [TestCase(TAPDTestProjectSetting.STORY_ID_2, "change_name_2", TAPDTestProjectSetting.STORY_NAME_2)]
        [TestCase(TAPDTestProjectSetting.STORY_ID_4, "change_name_4", TAPDTestProjectSetting.STORY_NAME_4)]
        public void ChangeStory_Name_Succeed(long id, string name, string originalName)
        {
            TAPDChangeStoryRequest request = new TAPDChangeStoryRequest();

            request.id = id;

            request.name = name;

            TAPDResponse<TAPDStory> response1 = m_Tapd.RequestChangeStory(request);

            Assert.AreEqual(response1.data.name, name);

            //重置父需求
            request.name = originalName;

            TAPDResponse<TAPDStory> response2 = m_Tapd.RequestChangeStory(request);

            Assert.AreEqual(response2.data.name, originalName);
        }
    }
}
