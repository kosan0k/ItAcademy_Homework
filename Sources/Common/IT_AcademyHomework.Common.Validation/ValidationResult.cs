using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IT_AcademyHomework.Common.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; }

        public IReadOnlyCollection<string> Messages { get; }

        public ValidationResult(bool isValid, IList<string> messages) 
        {
            IsValid = isValid;
            Messages = new ReadOnlyCollection<string>(messages);
        }
    }
}
