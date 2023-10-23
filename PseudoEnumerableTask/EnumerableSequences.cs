using System;
using System.Collections.Generic;
using PseudoEnumerableTask.Interfaces;

namespace PseudoEnumerableTask
{
    public static class EnumerableSequences
    {
        /// <summary>
        /// Filters a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="predicate">A <see cref="IPredicate{T}"/> to test each element of a sequence for a condition.</param>
        /// <returns>An sequence of elements from the source sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source sequence or predicate is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can not be null.");
            }

            return FilterIterator();

            IEnumerable<TSource> FilterIterator()
            {
                foreach (var item in source)
                {
                    if (predicate.Verify(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Transforms each element of source sequence from one type to another type by some rule.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of result sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="transformer">A <see cref="ITransformer{TSource,TResult}"/> that defines the rule of transformation.</param>
        /// <returns>A sequence, each element of which is transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence or transformer is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            }

            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer), "Transformer can not be null.");
            }

            return TransformIterator();

            IEnumerable<TResult> TransformIterator()
            {
                foreach (var item in source)
                {
                    yield return transformer.Transform(item);
                }
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>An ordered by comparer sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null, and one or more elements
        /// of the sequence do not implement the <see cref="IComparable{T}"/>  interface.
        ///</exception>

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer), "Comparer can not be null.");
            }

            if (typeof(TSource) == typeof(IComparable<TSource>))
            {
                throw new ArgumentNullException(nameof(comparer), "One or more elements of the sequence do not implement the IComparable<T> interface.");
            }

            return SortByIterator();

            IEnumerable<TSource> SortByIterator()
            {
                TSource[] array = ToArray(source);

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (comparer.Compare(array[i], array[j]) > 0)
                        {
                            Swap(ref array[i], ref array[j]);
                        }
                    }
                }

                for (int i = 0; i < array.Length; i++)
                {
                    yield return array[i];
                }
            }

        }

        /// <summary>
        /// Filters the elements of source sequence based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">Type selector to return.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence that contains the elements from source sequence that have type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static IEnumerable<TResult> TypeOf<TResult>(this object[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            }

            return TypeIterator();

            IEnumerable<TResult> TypeIterator()
            {
                foreach (var item in source)
                {
                    if (item is TResult result)
                    {
                        yield return result;
                    }
                }
            }
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of sequence.</typeparam>
        /// <param name="source">A sequence of elements to reverse.</param>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            }
            return ReverseIterator();

            IEnumerable<TSource> ReverseIterator()
            {
                TSource[] array = ToArray(source);

                for (int i = 0, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }

                for (int i = 0; i < array.Length; i++)
                {
                    yield return array[i];
                }
            }
        }

        /// <summary>
        /// Swaps two objects.
        /// </summary>
        /// <typeparam name="T">The type of parameters.</typeparam>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        internal static void Swap<T>(ref T left, ref T right)
        {
            (left, right) = (right, left);
        }

        /// <summary>
        /// Convert sourse to array.
        /// </summary>
        /// <typeparam name="T">The type of parameters.</typeparam>
        /// <param name="source">Initial data for conversion.</param>
        /// <returns>Array</returns>
        internal static T[] ToArray<T>(IEnumerable<T> source)
        {
            if (source is T[] sourceArray)
            {
                T[] array = new T[sourceArray.Length];
                Array.Copy(sourceArray, array, sourceArray.Length);
                return array;
            }

            T[] tempArray = new T[2];
            int count = 0;
            foreach (var item in source)
            {
                if (count == tempArray.Length)
                {
                    Array.Resize(ref tempArray, tempArray.Length * 2);
                }

                tempArray[count++] = item;
            }

            Array.Resize(ref tempArray, count);
            return count == 0 ? Array.Empty<T>() : tempArray;
        }
    }
}