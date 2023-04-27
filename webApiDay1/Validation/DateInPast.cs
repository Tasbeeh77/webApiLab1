using System.ComponentModel.DataAnnotations;

namespace webApiDay1.Validation
{
    public class DateInPast : ValidationAttribute
    {
        public override bool IsValid(object? value)
        => value is DateTime date && date < DateTime.Now;
    }
}
