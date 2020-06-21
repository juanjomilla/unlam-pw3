using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AyudandoEnLaPandemia.CustomDataAnnotations
{
    public class EnumerableMinLength : ValidationAttribute
    {
        private readonly int _minValue;

        public EnumerableMinLength(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            if (value is IList list)
            {
                return list.Count >= _minValue;
            }

            return false;
        }
    }
}