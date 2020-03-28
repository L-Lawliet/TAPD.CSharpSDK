using System;

namespace TAPD.CSharpSDK
{
    public class TAPDException : Exception
    {
        public TAPDException(string message) : this(message, null)
        {
            
        }

        public TAPDException(string message, Exception innerException) : base(string.Format("[TAPD]{0}", message), innerException)
        {
            
        }
    }
}
