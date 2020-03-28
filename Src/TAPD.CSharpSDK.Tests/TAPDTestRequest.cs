using System;

namespace TAPD.CSharpSDK.Tests
{
    public class TAPDTestRequest : TAPDRequest
    {
        public int intValue { get; set; }

        public string stringValue { get; set; }

        public float floatValue { get; set; }

        [TAPDIgnore]
        public int ignoreValue { get; set; }

        [TAPDPropertyName("reset_name_value")]
        public int resetNameValue { get; set; }

        [TAPDRequired]
        public string requiredValue { get; set; }
    }
}
