using IEEE754FormatTask;
using PseudoEnumerableTask.Interfaces;

namespace Adapters
{
    public class GetIEEE754FormatAdapter : ITransformer<double, string>
    {   
        public string Transform(double obj)
        {
            return DoubleExtension.GetIEEE754Format(obj);
        }
    }
}