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

        /// <summary>
        /// 整型枚举属性构造函数测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase(123, ExpectedResult = "123")]
        [TestCase(123, 456, 777, ExpectedResult = "123|456|777")]
        public string EnumIntProperty_Structure_Succeed(params int[] enums)
        {
            EnumIntProperty property = new EnumIntProperty(enums);

            return property.ToString();
        }

        /// <summary>
        /// 整型枚举属性添加测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase(123, ExpectedResult = "123")]
        [TestCase(123, 456, 777, ExpectedResult = "123|456|777")]
        public string EnumIntProperty_Add_Succeed(params int[] enums)
        {
            EnumIntProperty property = new EnumIntProperty();

            foreach (var item in enums)
            {
                property.AddEnum(item);
            }

            return property.ToString();
        }

        /// <summary>
        /// 字符串枚举属性构造函数测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase("A", ExpectedResult = "A")]
        [TestCase("A", "B", "C", ExpectedResult = "A|B|C")]
        public string EnumStringProperty_Structure_Succeed(params string[] enums)
        {
            EnumStringProperty property = new EnumStringProperty(enums);

            return property.ToString();
        }

        /// <summary>
        /// 字符串枚举属性添加测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase("A", ExpectedResult = "A")]
        [TestCase("A", "B", "C", ExpectedResult = "A|B|C")]
        public string EnumStringProperty_Add_Succeed(params string[] enums)
        {
            EnumStringProperty property = new EnumStringProperty();

            foreach (var item in enums)
            {
                property.AddEnum(item);
            }

            return property.ToString();
        }

        /// <summary>
        /// ID属性构造函数测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase(13211, ExpectedResult = "13211")]
        [TestCase(12345, 4664, 46546, ExpectedResult = "12345,4664,46546")]
        public string IDProperty_Structure_Succeed(params int[] ids)
        {
            IDProperty property = new IDProperty(ids);

            return property.ToString();
        }

        /// <summary>
        /// ID属性添加测试
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        [TestCase(13211, ExpectedResult = "13211")]
        [TestCase(12345, 4664, 46546, ExpectedResult = "12345,4664,46546")]
        public string IDProperty_Add_Succeed(params int[] enums)
        {
            IDProperty property = new IDProperty();

            foreach (var item in enums)
            {
                property.AddID(item);
            }

            return property.ToString();
        }

        /// <summary>
        /// 人员属性构造函数测试
        /// </summary>
        /// <param name="isAnd"></param>
        /// <param name="persons"></param>
        /// <returns></returns>
        [TestCase(false, "A", "B", "C", ExpectedResult = "A|B|C")]
        [TestCase(true, "A", "B", "C", ExpectedResult = "A;B;C")]
        public string PersonProperty_Structure_Succeed(bool isAnd = false, params string[] persons)
        {
            PersonProperty property = new PersonProperty(isAnd, persons);

            return property.ToString();
        }

        /// <summary>
        /// 人员属性添加测试
        /// </summary>
        /// <param name="isAnd"></param>
        /// <param name="persons"></param>
        /// <returns></returns>
        [TestCase(false, "A", "B", "C", ExpectedResult = "A|B|C")]
        [TestCase(true, "A", "B", "C", ExpectedResult = "A;B;C")]
        public string PersonProperty_Add_Succeed(bool isAnd = false, params string[] persons)
        {
            PersonProperty property = new PersonProperty(isAnd);

            foreach (var item in persons)
            {
                property.AddPerson(item);
            }
            
            return property.ToString();
        }
    }
}
