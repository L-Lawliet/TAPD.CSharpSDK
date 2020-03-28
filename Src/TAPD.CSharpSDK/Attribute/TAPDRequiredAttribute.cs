using System;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD Http必要参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TAPDRequiredAttribute : Attribute
    {
        public TAPDRequiredAttribute()
        {
        }
    }
}
