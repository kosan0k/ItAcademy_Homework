using IT_AcademyHomework.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace Lesson4_Homework.Controllers
{
    public class TemperatureConverterController : Controller
    {
        private readonly IValidator<double> _validator;

        public TemperatureConverterController(IValidator<double> validator) 
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IActionResult CelsiumToFahrenheits(double celsiumTemperature)
        {
            IActionResult result;

            var validationResult = _validator.Validate(celsiumTemperature);

            if (validationResult.IsValid)
            {
                var fahrenheits = (celsiumTemperature * (9 / 5)) + 32;
                result = Content($"Result is {fahrenheits} °F");
            }
            else
            {
                result = ProcessBadValidationResult(validationResult);
            }

            return result;
        }

        private BadRequestObjectResult ProcessBadValidationResult(ValidationResult validationResult)
        {
            string errorMessage = string.Empty;

            if (validationResult.ErrorMessages.Any())
            {
                var errors = new StringBuilder();
                validationResult.ErrorMessages.ToList().ForEach(e => errors.AppendLine(e));
                errorMessage = errors.ToString();
            }

            return new BadRequestObjectResult(errorMessage);
        }
    }
}
