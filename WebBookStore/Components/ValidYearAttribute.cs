using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Components
{
    public class ValidYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            int year;
            if (int.TryParse(value.ToString(), out year))
            {
                int currentYear = DateTime.Now.Year;
                return year >= 1300 && year <= currentYear;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} phải nằm trong khoảng từ 1300 đến {DateTime.Now.Year}.";
        }
    }
}
