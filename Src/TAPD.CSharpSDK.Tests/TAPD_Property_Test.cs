using NUnit.Framework;
using System;

namespace TAPD.CSharpSDK.Tests
{
    /// <summary>
    /// TAPD属性测试
    /// </summary>
    [TestFixture]
    class TAPD_Property_Test
    {
        /// <summary>
        /// 日期类型的字符串构建函数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>输出的字符串</returns>
        [TestCase("2020-01-12", "", ExpectedResult = ">2020-01-12")]
        [TestCase("2020-01-12", "asd", ExpectedResult = ">2020-01-12")]
        [TestCase(null, "2020-03-22", ExpectedResult = "<2020-03-22")]
        [TestCase("0", "2020-03-22", ExpectedResult = "<2020-03-22")]
        [TestCase("2020-01-12", "2020-03-22", ExpectedResult = "2020-01-12~2020-03-22")]
        public string DateProperty_Structure_String_Succeed(string startTime, string endTime)
        {
            DateProperty property = new DateProperty(startTime, endTime);

            return property.ToString();
        }

        /// <summary>
        /// 日期类型的字符串构建函数
        /// </summary>
        /// <returns>输出的字符串</returns>
        [Test(ExpectedResult = "2020-01-12~2020-03-22")]
        public string DateProperty_Structure_DateTime_Succeed()
        {
            DateTime startTime = new DateTime(2020, 1, 12);
            DateTime endTime = new DateTime(2020, 3, 22);

            DateProperty property = new DateProperty(startTime, endTime);

            return property.ToString();
        }

        /// <summary>
        /// 日期类型的赋值测试
        /// </summary>
        /// <param name="hasStart">是否有开始时间</param>
        /// <param name="hasEnd">是否有结束时间</param>
        /// <returns>输出的字符串</returns>
        [TestCase(true, false, ExpectedResult = ">2020-01-12")]
        [TestCase(false, true, ExpectedResult = "<2020-03-22")]
        [TestCase(true, true, ExpectedResult = "2020-01-12~2020-03-22")]
        public string DateProperty_DateTime_Succeed(bool hasStart, bool hasEnd)
        {
            DateProperty property = new DateProperty();

            if (hasStart)
            {
                property.startDate = new DateTime(2020, 1, 12);
            }

            if (hasEnd)
            {
                property.endDate = new DateTime(2020, 3, 22);
            }

            return property.ToString();
        }
    }
}
