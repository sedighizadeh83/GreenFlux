using System;
using System.Collections.Generic;
using System.Linq;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service.Validators
{
    public class ValidationResult
    {
		private readonly List<ValidationFailure> _errors;

		public virtual bool IsValid => Errors.Count == 0;

		public List<ValidationFailure> Errors => _errors;

		public ValidationResult()
		{
			_errors = new List<ValidationFailure>();
		}

		public ValidationResult(IEnumerable<ValidationFailure> failures)
		{
			_errors = failures.Where(failure => failure != null).ToList();
		}

		internal ValidationResult(List<ValidationFailure> errors)
		{
			_errors = errors;
		}

		public override string ToString()
		{
			return ToString(Environment.NewLine);
		}

		public string ToString(string separator)
		{
			return string.Join(separator, _errors.Select(failure => failure.ErrorMessage));
		}
	}
}
