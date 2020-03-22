using System;

namespace TAPD.CSharpSDK
{
    public class TAPDResponse<T>
    {
        public TAPDHttpStatus status;

        public string info;

        public T data;
    }
}
