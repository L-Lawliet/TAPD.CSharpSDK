using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.CompilerServices;

namespace TAPD.CSharpSDK
{
    /// <summary>
    /// TAPD 子对象转换器
    /// 
    /// 返回结果样例
    /// {
    ///     "status": 1,
    ///     "data": {
    ///         "Story": {
    ///             "id": "...."
    ///             ....
    ///         }
    ///     }
    /// }
    /// 
    /// 因为有一层子结构[Story]，会增加结构和使用的复杂性。因此使用Converter去除中间层
    /// 改为以下结构
    /// 
    /// {
    ///     "status": 1,
    ///     "data": {
    ///         "id": "...."
    ///         ....
    ///     }
    /// }
    /// 
    /// 转换时，会根据传入的泛型类型转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TAPDConverter<T> : JsonConverter
    {
        /// <summary>
        /// 子属性名字
        /// </summary>
        private string m_PropertyName;

        public TAPDConverter(string propertyName)
        {
            m_PropertyName = propertyName;
        }

        /// <summary>
        /// 判断是否可转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (typeof(T[]) == objectType)
            {
                return true;
            }

            if (typeof(T) == objectType)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 读取Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //Console.WriteLine(reader.TokenType);

            T objectResult = default(T);
            T[] arrayResult = null;

            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    JArray arrayObject = serializer.Deserialize<JArray>(reader);

                    arrayResult = new T[arrayObject.Count];

                    int i = 0;

                    foreach (var item in arrayObject)
                    {
                        foreach (var child in item)
                        {
                            if(child.Type == JTokenType.Property)
                            {
                                JProperty property = child as JProperty;

                                if (property.Name == m_PropertyName)
                                {
                                    arrayResult[i] = property.Value.ToObject<T>();
                                }
                            }
                        }

                        i++;
                    }
                    return arrayResult;
                case JsonToken.StartObject:
                    JObject jObject = serializer.Deserialize<JObject>(reader);

                    if(typeof(T[]) == objectType)
                    {
                        arrayResult = new T[1];

                        foreach (var pair in jObject)
                        {
                            if (pair.Value.Path == m_PropertyName)
                            {
                                arrayResult[0] = pair.Value.ToObject<T>();
                            }
                        }

                        return arrayResult;
                    }
                    else
                    {
                        foreach (var pair in jObject)
                        {
                            if (pair.Value.Path == m_PropertyName)
                            {
                                objectResult = pair.Value.ToObject<T>();
                            }
                        }

                        return objectResult;
                    }
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
