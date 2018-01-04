using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoSmart.Linq
{
    public static class ExtendedLinq
    {
        public abstract class IClass<T> where T : class
        {
        }

        public abstract class IStruct<T> where T : struct
        {
        }

        public static bool Contains<T>(this IEnumerable<T> container, Func<T, bool> where)
        {
            return container.Where(where).Any();
        }

        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, IEnumerable<T> values)
        {
            return ContainsAnyOf(container, new SortedSet<T>(values));
        }

        public static bool ContainsAnyOf<T>(this IEnumerable<T> container, SortedSet<T> values)
        {
            foreach (var value in container)
            {
                if (container.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAllOf<T>(this IEnumerable<T> container, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (!container.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool None<T>(this IEnumerable<T> container)
        {
            return container.Empty();
        }

        public static bool None<T>(this IEnumerable<T> container, Func<T, bool> where)
        {
            return !Contains(container, where);
        }

        public static bool In<T>(this T t, IEnumerable<T> container)
        {
            return container.Contains(t);
        }

        public static bool Empty<T>(this IEnumerable<T> container)
        {
            return !container.Any();
        }

        //The goal here is to act like `object as T` for classes (null on invalid conversion) but still
        //allow the method to be used to avoid parentheses hell for structs, e.g. `if (((T)t).X)` can be
        //written as simply `if (t.As<T>().X`.
        //
        //If C# allowed for overloading generic functions by constraints, we could split this
        //into As<T> where T: class and As<T> where T: struct, with one return null and the other
        //throwing an InvalidCastException; but we can't.
        //public static T As<T>(this object @object)
        //    where T: class
        //{
        //    if (@object == null)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        return (T)@object;
        //    }
        //    catch (InvalidCastException)
        //    {
        //        Type type = @object.GetType();
        //        if (type.GetTypeInfo().IsValueType)
        //        {
        //            //reference type
        //            return null;
        //        }
        //        else if (Nullable.GetUnderlyingType(type) != null)
        //        {
        //            //Nullable<x>, also return null on invalid cast
        //            return null;
        //        }

        //        throw;
        //    }
        //}

#if false
        //class.As<struct>: throw exception if not possible
        public static Y As<Y,X>(this X x, IClass<X> _  = null, IStruct<Y> _y = null)
            where X : class
            where Y : struct
        {
            return (Y)(object)x;
        }

        //class.As<class>: return null if not possible
        public static Y As<Y,X>(this X x, IClass<X> _ = null)
            where X : class
            where Y : class
        {
            return x as Y;
        }

        //struct.As<struct>: throw exception if not possible
        public static Y As<Y,X>(this X x, IStruct<X> _ = null, IStruct<Y> _y = null)
            where X : struct
            where Y : struct
        {
            return (Y)(object)x;
        }

        //struct.As<class>: return null if not possible
        public static Y As<Y, X>(this X x, IStruct<X> _ = null, IClass<Y> _y = null)
            where X : struct
            where Y : class
        {
            try
            {
                return (Y)(object)x;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }

        //Nullable<struct>.As<struct>: throw exception if not possible
        public static Y As<Y,X>(this X? x, IStruct<X> _ = null, IStruct<Y> _y = null)
            where X : struct
            where Y : struct
        {
            return (Y)(object)x;
        }

        //Nullable<struct>.As<class>: return null if not possible
        public static Y As<Y, X>(this X? x, IStruct<X> _ = null, IClass<Y> _y = null)
            where X : struct
            where Y : class
        {
            try
            {
                return (Y)(object)x;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }
#endif
        //object.As<class>: return null if not possible
        public static Y As<Y>(this object x, IClass<Y> _ = null)
            where Y : class
        {
            return x as Y;
        }

        //object.As<struct>: throw exception if not possible
        public static Y As<Y>(this object x, IStruct<Y> _ = null)
            where Y : struct
        {
            return (Y)(object)x;
        }

        public static bool Is<T>(this object @object)
        {
            return @object is T;
        }

        public static bool IsNot<T>(this object @object)
        {
            return !(@object is T);
        }
    }
}
