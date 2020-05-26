using System;
using System.Linq;
using NUnit.Framework;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(
    new object[] {12, 3, 4, true, "12", "2", 13.56, "6", null, 17.901, false},
    new int[] {12, 3, 4},
    TypeArgs = new Type[] {typeof(int)})]
    [TestFixture(new object[] {12, null, 3, 4, true, "12", "2", 13.56, "6", 17.901, false},
        new string[] {"12", "2", "6"},
        TypeArgs = new Type[] {typeof(string)})]
    [TestFixture(new object[] {12, -123.543, 3, null, 4, true, "12", "2", 13.56, "6", 17.901, false},
        new double[] {-123.543, 13.56, 17.901},
        TypeArgs = new Type[] {typeof(double)})]
    [TestFixture(new object[] {-123.543, 12, 3, 4, true, "12", "2", null, 13.56, "6", 17.901, false},
        new bool[] {true, false},
        TypeArgs = new Type[] {typeof(bool)})]
    [TestFixture(new object[] {'s', -123.543, '\n', 12, 3, 4, true, "12", "2", null, 13.56, "6", 17.901, false},
        new char[] {'s', '\n'},
        TypeArgs = new Type[] {typeof(char)})]
    internal class EnumerableSequencesTypeOfFixture<T>
    {
        private readonly T[] expected;
        private readonly object[] source;
    
        public EnumerableSequencesTypeOfFixture(object[] source, T[] expected)
        {
            this.expected = expected;
            this.source = source;
        }
    
        [Test]
        public void TypeOfTest()
        {
            CollectionAssert.AreEqual(expected, source.TypeOf<T>());
        }
    }
}