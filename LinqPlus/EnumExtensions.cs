using System;
using System.Collections.Generic;
using System.Reflection;

namespace NeoSmart.Linq
{
    public abstract class EnumClassUtils<TClass>
        where TClass : class
    {
        public static TEnum Parse<TEnum>(string value)
            where TEnum : struct, TClass
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }

    public abstract class EnumTypeUtils<TClass>
        where TClass: class
    {
        public static int ToInt<T>(T t)
            where T : struct, IConvertible
        {
            return (int)(IConvertible)t;
        }
    }

    public class Enum<T>
        where T : struct, Enum, IConvertible
    {
        public static int Count
        {
            get
            {
                if (!typeof(T).GetTypeInfo().IsEnum)
                {
                    throw new ArgumentException("T must be an enumerated type");
                }

                return Enum.GetNames(typeof(T)).Length;
            }
        }

        public static T[] ToArray()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static List<T> ToList()
        {
#if NETCOREAPP1_0_OR_GREATER
            return new List<T>(Enum.GetValues<T>());
#else
            return new List<T>((T[])Enum.GetValues(typeof(T)));
#endif
        }
    }
}
