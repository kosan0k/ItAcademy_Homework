using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IT_AcademyHomework.Common.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; }

        public IReadOnlyCollection<string> ErrorMessages { get; }

        public ValidationResult(bool isValid) 
        {
            IsValid = isValid;
            ErrorMessages = new ReadOnlyCollection<string>(new List<string>());
        }

        public ValidationResult(bool isValid ,IList<string> errorMessages) 
        {
            IsValid = isValid;
            ErrorMessages = new ReadOnlyCollection<string>(errorMessages);
        }
    }
}
