using System;

namespace TAPD.CSharpSDK
{
    public class TAPDHttpAPI
    {
        /// <summary>
        /// 基础地址
        /// </summary>
        public const string BASE_URL = "https://api.tapd.cn/";

        /// <summary>
        /// 需求列表 Get
        /// 新建需求 Post
        /// 更新需求 Post
        /// </summary>
        public const string STORIES = "stories";

        /// <summary>
        /// 需求数量
        /// </summary>
        public const string STORIES_COUNT = "stories/count";
    }
}
