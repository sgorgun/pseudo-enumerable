using System;
using System.Linq;
using PseudoEnumerableTask.Interfaces;
using Adapters;
using NUnit.Framework;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(
        new[] {122.625, -255.255, 255.255, 4294967295.012, -451387.2345, 0.2345E-12}, new[]
        {
            "0100000001011110101010000000000000000000000000000000000000000000",
            "1100000001101111111010000010100011110101110000101000111101011100",
            "0100000001101111111010000010100011110101110000101000111101011100",
            "0100000111101111111111111111111111111111111000000110001001001110",
            "1100000100011011100011001110110011110000001000001100010010011100",
            "0011110101010000100000000110000001011111000011101110100001011011"
        },
        TypeArgs = new Type[] {typeof(double), typeof(string)})]
    public class EnumerableSequencesTransformFixture<TSource, TResult>
    {
        private readonly TSource[] source;
        private readonly TResult[] expected;
        private readonly ITransformer<TSource, TResult> transformer;

        public EnumerableSequencesTransformFixture(TSource[] source, TResult[] expected)
        {
            this.expected = expected;
            this.source = source;
            this.transformer = TransformerCreator(typeof(TSource), typeof(TResult));
        }

        [Test]
        public void TransformerTest()
        {
            var actual = source.Transform(transformer).ToArray();
            CollectionAssert.AreEqual(actual, expected);
        }

        private static ITransformer<TSource, TResult> TransformerCreator(Type typeSource, Type typeResult)
        {
            if (typeSource == typeof(double) && typeResult == typeof(string))
            {
                return (ITransformer<TSource, TResult>) new GetIEEE754FormatAdapter();
            }

            return null;
        }
    }
}