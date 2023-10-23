using System;

namespace ContainsDigitPredicate
{
    public class ContainsDigitValidator
    {
        private int digit;

        /// <summary>
        /// Gets or sets a digit.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when Digit more than 9 or less than 0.</exception>
        public int Digit
        {
            get => this.digit;
            set => this.digit = (value is < 0 or > 9) ? throw new ArgumentOutOfRangeException(nameof(value), "Ñan not be less than zero or more then 9.") : value;
        }

        /// <summary>
        /// Verify value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Verify(int value)
        {
            long longNumber = value;

            if (this.digit == 0 && longNumber == 0)
            {
                return true;
            }

            longNumber = (longNumber < 0) ? -longNumber : longNumber;

            for (; longNumber != 0; longNumber /= 10)
            {
                if (longNumber % 10 == this.digit)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
