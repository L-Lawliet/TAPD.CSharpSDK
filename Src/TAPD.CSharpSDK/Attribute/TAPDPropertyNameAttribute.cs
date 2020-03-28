using System;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD属性名字，用于映射HTTP参数
    /// </summary>
    public class TAPDPropertyNameAttribute : Attribute
    {
        public string name { get; set; }

        public TAPDPropertyNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
