using System;
using System.Reflection;

namespace TAPD.CSharpSDK
{
    public class ReflectionUtil
    {
        /// <summary>
        /// 获取自定义的特性组
        /// 只返回最先找到的一个
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static TAttribute GetCustomAttributes<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(TAttribute), false);

            foreach (object attribute in attributes)
            {
                if (attribute.GetType() == typeof(TAttribute))
                {
                    return attribute as TAttribute;
                }
            }

            return null;
        }
    }
}
