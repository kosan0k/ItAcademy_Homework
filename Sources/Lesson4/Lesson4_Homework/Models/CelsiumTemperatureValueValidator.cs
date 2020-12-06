using IT_AcademyHomework.Common.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Lesson4_Homework.Models
{
    public class CelsiumTemperatureValueValidator : IValidator<double>
    {
        private const double _minPossibleCelsiumValue = -273.15d;

        public ValidationResult Validate(double parameter)
        {
            var validationErrorMessages = new List<string>();

            if (parameter <= _minPossibleCelsiumValue)
            {
                validationErrorMessages.Add("Value can not be less than -273.15 (lowest limit of the thermodynamic temperature scale)");
            }
            else if (parameter > double.MaxValue) 
            {
                validationErrorMessages.Add($"Can not convert values more than {double.MaxValue}");
            }

            return new ValidationResult(!validationErrorMessages.Any() ,validationErrorMessages);
        }
    }
}
