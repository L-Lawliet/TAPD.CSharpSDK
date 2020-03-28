using System;

namespace TAPD.CSharpSDK
{
    public class TAPDRequiredParameterMissingException : TAPDException
    {
        public TAPDRequiredParameterMissingException(string parameterName) : base(string.Format("Required Parameter Missing:{0}", parameterName))
        {
            
        }
    }
}
