using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace It_AcademyHomework.Repository.Common
{
    public class RepositoryOperationResult
    {
        public bool IsSucceed { get; }

        public IReadOnlyCollection<string> ErrorMessages { get; }

        public RepositoryOperationResult(bool isSucceed)
        {
            IsSucceed = isSucceed;
            ErrorMessages = new ReadOnlyCollection<string>(new List<string>());
        }

        public RepositoryOperationResult(bool isSucceed, IList<string> errorMessages)
        {
            IsSucceed = isSucceed;
            ErrorMessages = new ReadOnlyCollection<string>(errorMessages);
        }
    }
}
