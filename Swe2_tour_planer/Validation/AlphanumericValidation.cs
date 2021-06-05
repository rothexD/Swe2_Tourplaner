using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Swe2_tour_planer.Validation
{
    public class AlphaNumvericValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "was not set");
            }
            string duration = (string)value;

            if (Regex.IsMatch(duration, "^[1-9,.]+$"))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Must be alphanumeric Only [1-9,.]");
            }
        }
    }
}
