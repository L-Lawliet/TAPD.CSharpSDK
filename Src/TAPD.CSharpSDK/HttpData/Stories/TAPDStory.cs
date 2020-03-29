using Newtonsoft.Json;
using System;

namespace TAPD.CSharpSDK
{
    public class TAPDStory
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id;

        /// <summary>
        /// 标题
        /// </summary>
        public string name;

        /// <summary>
        /// 描述
        /// </summary>
        public string description;

        /// <summary>
        /// 项目ID
        /// </summary>
        [JsonProperty("workspace_id")]
        public string workspaceID;

        /// <summary>
        ///  创建人
        /// </summary>
        public string creator;

        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<DateTime> created;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public Nullable<DateTime> modified;

        /// <summary>
        /// 状态
        /// </summary>
        public string status;

        /// <summary>
        /// 当前处理人
        /// </summary>
        public string owner;

        /// <summary>
        /// 抄送人
        /// </summary>
        public string cc;

        /// <summary>
        /// 预计开始
        /// </summary>
        public Nullable<DateTime> begin;

        /// <summary>
        /// 预计结束
        /// </summary>
        public Nullable<DateTime> due;

        /// <summary>
        /// 规模
        /// </summary>
        public string size;

        /// <summary>
        /// 优先级
        /// </summary>
        public string priority;

        /// <summary>
        /// 开发人员
        /// </summary>
        public string developer;

        /// <summary>
        /// 迭代
        /// </summary>
        [JsonProperty("iteration_id")]
        public string iterationID;

        /// <summary>
        /// 测试重点
        /// </summary>
        [JsonProperty("test_focus")]
        public string testFocus;

        /// <summary>
        /// 类型
        /// </summary>
        public string type;

        /// <summary>
        /// 来源
        /// </summary>
        public string source;

        /// <summary>
        /// 模块
        /// </summary>
        public string module;

        /// <summary>
        /// 版本
        /// </summary>
        public string version;

        /// <summary>
        /// 完成时间
        /// </summary>
        public string completed;

        /// <summary>
        /// 需求分类
        /// </summary>
        [JsonProperty("category_id")]
        public string categoryID;

        /// <summary>
        /// 层级
        /// </summary>
        public string path;

        /// <summary>
        /// 层级
        /// </summary>
        [JsonProperty("parent_id")]
        public string parentID;

        /// <summary>
        /// 子需求
        /// '|'分割
        /// </summary>
        [JsonProperty("children_id")]
        public string childrenID;

        /// <summary>
        /// 根需求
        /// </summary>
        [JsonProperty("ancestor_id")]
        public string ancestorID;


        /// <summary>
        /// 业务价值
        /// </summary>
        [JsonProperty("business_value")]
        public string businessValue;

        /// <summary>
        /// 预估工时
        /// </summary>
        public string effort;

        /// <summary>
        /// 完成工时
        /// </summary>
        [JsonProperty("effort_completed")]
        public string effortCompleted;

        /// <summary>
        /// 剩余工时
        /// </summary>
        public string remain;

        /// <summary>
        /// 超出工时
        /// </summary>
        public string exceed;

        /// <summary>
        /// 发布计划
        /// </summary>
        [JsonProperty("release_id")]
        public string releaseID;
    }
}
