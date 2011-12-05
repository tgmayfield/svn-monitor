namespace SVNMonitor.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class Enumerable
    {
        public static bool All<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (T item in collection)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (T item in collection)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<T> Cast<T>(this IEnumerable collection)
        {
            List<T> results = new List<T>();
            foreach (object item in collection)
            {
                results.Add((T) item);
            }
            return results;
        }

        public static bool Contains<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Contains<T>(t => t.Equals(item));
        }

        public static bool Contains<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (T item in collection)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static int Count<T>(this IEnumerable<T> collection)
        {
            int count = 0;
            using (IEnumerator<T> CS$5$0000 = collection.GetEnumerator())
            {
                while (CS$5$0000.MoveNext())
                {
                    T current = CS$5$0000.Current;
                    count++;
                }
            }
            return count;
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> collection)
        {
            List<T> list = new List<T>();
            foreach (T item in collection)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static T First<T>(this IEnumerable<T> collection)
        {
            using (IEnumerator<T> CS$5$0001 = collection.GetEnumerator())
            {
                while (CS$5$0001.MoveNext())
                {
                    return CS$5$0001.Current;
                }
            }
            throw new InvalidOperationException("Collection is empty");
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (T item in collection)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        public static T Max<T>(this IEnumerable<T> collection) where T: IComparable
        {
            T max = collection.First<T>();
            foreach (T item in collection)
            {
                if (item.CompareTo(max) >= 0)
                {
                    max = item;
                }
            }
            return max;
        }

        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> func)
        {
            List<TResult> results = new List<TResult>();
            foreach (T item in collection)
            {
                results.Add(func(item));
            }
            return results;
        }

        public static T[] ToArray<T>(this IEnumerable<T> collection)
        {
            List<T> list = new List<T>(collection);
            return list.ToArray();
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            List<T> results = new List<T>();
            foreach (T item in collection)
            {
                if (predicate(item))
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }
}

