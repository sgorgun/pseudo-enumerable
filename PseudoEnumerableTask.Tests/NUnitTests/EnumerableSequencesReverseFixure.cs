using System;
using NUnit.Framework;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(new[] {"Beg", null, "Life", "I", "i", "I", null, "To"},
        new[] {"To", null, "I", "i", "I", "Life", null, "Beg"},
        TypeArgs = new Type[] {typeof(string)})]
    [TestFixture(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
        new[] {10, 9, 8, 7, 6, 5, 4, 3, 2, 1},
        TypeArgs = new Type[] {typeof(int)})]
    public class EnumerableSequencesReverseTestFixture<T>
    {
        private readonly T[] source;
        private readonly T[] expected;

        public EnumerableSequencesReverseTestFixture(T[] source, T[] expected)
        {
            this.expected = expected;
            this.source = source;
        }

        [Test]
        public void ReverseTest() => CollectionAssert.AreEqual(source.Reverse(), expected);
    }
}