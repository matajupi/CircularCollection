using System;
using System.Collections.Generic;
using NUnit.Framework;
using CircularCollection;
using System.Linq;

namespace TestCollection
{
    public class TestCase
    {
        private static readonly int size = 100;
        private static readonly string template = "{0} expected, but got {1}.";

        public enum InstanceType
        {
            CircularArray,
            CircularLinkedList,
        }
        
        public static ICircularCollection<int> CreateCircularCollection(InstanceType t)
        {
            ICircularCollection<int> collection;
            switch (t)
            {
                case InstanceType.CircularArray:
                    collection = new CircularArray<int>(size);
                    break;
                case InstanceType.CircularLinkedList:
                    var instance = new CircularLinkedList<int>();
                    for (var i = 0; size > i; i++)
                    {
                        instance.AddFirst(0);
                    }

                    collection = instance;
                    break;
                default:
                    collection = default;
                    return collection;
            }
            for (var i = 1; size >= i; i++)
                collection[i - 1] = i;
            return collection;
        }
        
        public static void Sum_IntegerCollection_Success(ICircularCollection<int> collection)
        {
            var expected = (size) * (size + 1) / 2;
            var actual = collection.Sum();
            Assert.AreEqual(expected, actual, string.Format(template, expected, actual));
        }
        
        public static void Sum_IntegerCollection_Fail(ICircularCollection<int> collection)
        {
            var expected = (size) * (size + 1) / 2 + 1;
            var actual = collection.Sum();
            Assert.AreNotEqual(expected, actual, string.Format(template, expected, actual));
        }
        
        public static void Rotate_IntegerCollection_Success(ICircularCollection<int> collection, int shiftRight)
        {
            var exs = shiftRight;
            if (shiftRight < 0)
            {
                var shiftLeft = Math.Abs(exs);
                var m = shiftLeft / collection.Length;
                if (shiftLeft % collection.Length != 0)
                    m++;
                exs += collection.Length * m;
            }
            var expected = exs % collection.Length + 1;
            collection.Rotate(shiftRight);
            var actual = collection[0];
            Assert.AreEqual(expected, actual, string.Format(template, expected, actual));
        }
        
        public static void Foreach_IntegerCollection_Success(ICircularCollection<int> collection)
        {
            var flag = true;
            var expected = collection[0];
            foreach (var actual in collection)
            {
                flag = flag && actual == expected;
                expected++;
                expected = expected > collection.Length ? 0 : expected;
            }
            Assert.AreEqual(true, flag, string.Format(template, true, flag));
        }
    }
}