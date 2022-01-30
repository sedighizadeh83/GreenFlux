using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.GlobalErrorHandling.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
        : base("One or more validation errors occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }

    public class ValidationFailure
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public ValidationFailure(string properyName, string errorMessage)
        {
            this.PropertyName = properyName;
            this.ErrorMessage = errorMessage;
        }
    }
}
