using System.ComponentModel.DataAnnotations;

namespace Intuit_Challenge.Validations
{
    public class RequiredGreaterInYears : ValidationAttribute
    {
        /// <summary>
        /// Designed for dropdowns to ensure that a selection is valid and not the dummy "SELECT" entry
        /// </summary>
        /// <param name="value">The integer value of the selection</param>
        /// <returns>True if dateTime is oldder than 18 years</returns>
        public override bool IsValid(object value)
        {
            DateTime legalDateTime = DateTime.UtcNow.AddYears(-18);
            DateTime date;
            return value != null && DateTime.TryParse(value.ToString(), out date) && date <= legalDateTime;
        }
    }
}
