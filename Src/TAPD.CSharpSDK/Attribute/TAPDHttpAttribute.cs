using System;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD Http请求的参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TAPDHttpAttribute : Attribute
    {
        public TAPDHttpMethod method { get; set; }

        public TAPDHttpAttribute(TAPDHttpMethod method)
        {
            this.method = method;
        }
    }
}
