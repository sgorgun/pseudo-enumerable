using ContainsDigitPredicate;
using PseudoEnumerableTask.Interfaces;

namespace Adapters
{
    public class ContainsDigitContainsDigitValidatorAdapter : IPredicate<int>
    {
        private readonly ContainsDigitValidator _validator;
        public ContainsDigitContainsDigitValidatorAdapter(ContainsDigitValidator validator)
        {
            this._validator = validator;
        }
        public bool Verify(int obj)
        {
            return _validator.Verify(obj);
        }
    }
}