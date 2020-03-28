using System;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD Http忽略参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TAPDIgnoreAttribute : Attribute
    {
        public TAPDIgnoreAttribute()
        {
            
        }
    }
}
