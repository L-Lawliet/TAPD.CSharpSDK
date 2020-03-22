using System;

namespace TAPD.CSharpSDK
{
    public class TAPDResponse<T>
    {
        public TAPDHttpStatus status { get; set; }

        public string info { get; set; }

        public T data { get; set; }
    }
}
