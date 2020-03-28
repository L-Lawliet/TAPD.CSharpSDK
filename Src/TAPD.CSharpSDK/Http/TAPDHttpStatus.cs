using System;

namespace TAPD.CSharpSDK
{
    public enum TAPDHttpStatus
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        Succeed = 1,

        /// <summary>
        /// 未授权
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// 没有访问权限
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 远程服务器返回错误
        /// </summary>
        ParamError = 422,
    }
}
