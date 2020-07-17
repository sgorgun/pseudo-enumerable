using System;
using System.Collections.Generic;
using Comparers;
using NUnit.Framework;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(
        new[] {"Beg", null, "Life", "I", "i", "I", null, "To"},
        new[] {null, null, "I", "i", "I", "To", "Beg", "Life"},
        TypeArgs = new Type[] {typeof(string)})]
    [TestFixture(
        new[] {0, 12, -12, 34, 0, 2, -567, 12, -12, 89, int.MaxValue, -1000},
        new[] {0, 0, 2, 12, -12, 12, -12, 34, 89, -567, -1000, int.MaxValue},
        TypeArgs = new Type[] {typeof(int)})]
    public class EnumerableSequencesSortByFixture<T>
    {
        private readonly T[] source;
        private readonly T[] expected;
        private readonly IComparer<T> comparer;

        public EnumerableSequencesSortByFixture(T[] source, T[] expected)
        {
            this.expected = expected;
            this.source = source;
            this.comparer = ComparerCreator(typeof(T));
        }

        [Test]
        public void SortByTest() => CollectionAssert.AreEqual(expected, source.SortBy(comparer));
        private static IComparer<T> ComparerCreator(Type type)
        {
            if (type == typeof(string))
            {
                return (IComparer<T>) new StringByLengthComparer();
            }

            if (type == typeof(int))
            {
                return (IComparer<T>) new IntegerByAbsComparer();
            }

            return null;
        }
    }
}