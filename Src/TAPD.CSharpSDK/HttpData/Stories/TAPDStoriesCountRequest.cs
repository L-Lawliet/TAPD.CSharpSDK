using System;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// 获取需求数量接口
    /// 
    /// https://www.tapd.cn/help/view#1120003271001001487
    /// </summary>
    [TAPDHttpAttribute(TAPDHttpMethod.Get)]
    public class TAPDStoriesCountRequest : TAPDRequest
    {
        /// <summary>
        /// ID	
        /// 支持多ID查询
        /// </summary>
        public IDProperty id { get; set; }
        
        /// <summary>
        /// 标题	
        /// 支持模糊匹配
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 优先级	
        /// 支持枚举查询
        /// </summary>
        public EnumStringProperty priority { get; set; }

        /// <summary>
        /// 业务价值
        /// </summary>
        [TAPDPropertyName("business_value")]
        public Nullable<int> businessValue { get; set; }

        /// <summary>
        /// 状态	
        /// 支持枚举查询
        /// </summary>
        public EnumStringProperty status { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string module { get; set; }

        /// <summary>
        /// 测试重点
        /// </summary>
        [TAPDPropertyName("test_focus")]
        public string testFocus { get; set; }

        /// <summary>
        /// 规模
        /// </summary>
        public Nullable<int> size { get; set; }

        /// <summary>
        /// 当前处理人
        /// 支持模糊匹配
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 抄送人 
        /// 支持模糊匹配
        /// </summary>
        public string cc { get; set; }

        /// <summary>
        /// 创建人 
        /// 支持模糊匹配
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 开发人员
        /// </summary>
        public string developer { get; set; }

        /// <summary>
        /// 预计开始    
        /// 支持时间查询
        /// </summary>
        public DateProperty begin { get; set; }

        /// <summary>
        /// 预计结束 
        /// 支持时间查询
        /// </summary>
        public DateProperty due { get; set; }

        /// <summary>
        /// 创建时间    
        /// 支持时间查询
        /// </summary>
        public DateProperty created { get; set; }

        /// <summary>
        /// 最后修改时间 
        /// 支持时间查询
        /// </summary>
        public DateProperty modified { get; set; }

        /// <summary>
        /// 完成时间    
        /// 支持时间查询
        /// </summary>
        public DateProperty completed { get; set; }

        /// <summary>
        /// 迭代 
        /// 支持不等于查询
        /// </summary>
        [TAPDPropertyName("iteration_id")]
        public Nullable<int> iterationID { get; set; }

        /// <summary>
        /// 预估工时
        /// </summary>
        public string effort { get; set; }

        /// <summary>
        /// 完成工时
        /// </summary>
        [TAPDPropertyName("effort_completed")]
        public string effortCompleted { get; set; }

        /// <summary>
        /// 剩余工时
        /// </summary>
        public Nullable<float> remain { get; set; }

        /// <summary>
        /// 超出工时
        /// </summary>
        public Nullable<float> exceed  { get; set; }

        /// <summary>
        /// 需求分类   
        /// 支持枚举查询
        /// </summary>
        [TAPDPropertyName("category_id")]
        public EnumIntProperty categoryID { get; set; }

        /// <summary>
        /// 发布计划
        /// </summary>
        [TAPDPropertyName("release_id")]
        public Nullable<int> releaseID { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 父需求
        /// </summary>
        [TAPDPropertyName("parent_id")]
        public Nullable<long> parentID { get; set; }

        /// <summary>
        /// 子需求
        /// </summary>
        [TAPDPropertyName("children_id")]
        public string childrenID { get; set; }

        /// <summary>
        /// 详细描述    
        /// 支持模糊匹配
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 自定义字段参数，具体字段名通过接口 https://www.tapd.cn/help/view#1120003271001001456 获取	
        /// 未实现
        /// </summary>
        [TAPDIgnore]
        public string[] custom_fields { get; set; }
    }
}
