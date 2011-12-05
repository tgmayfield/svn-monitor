using System;

namespace SVNMonitor.Settings.Validation
{
	internal class NumberConfigValidator : IConfigValidator
	{
		public NumberConfigValidator(ValidationOperator op, int compareTo)
		{
			Operator = op;
			CompareTo = compareTo;
		}

		private Predicate<int> GetPredicate()
		{
			switch (Operator)
			{
				case ValidationOperator.More:
					return num => (num > CompareTo);

				case ValidationOperator.MoreOrEqual:
					return num => (num >= CompareTo);

				case ValidationOperator.Less:
					return num => (num < CompareTo);

				case ValidationOperator.LessOrEqual:
					return num => (num <= CompareTo);
			}
			throw new NotSupportedException();
		}

		public object Validate(object value, out bool isValid)
		{
			int retValue = (int)value;
			Predicate<int> predicate = GetPredicate();
			isValid = predicate(retValue);
			return retValue;
		}

		public int CompareTo { get; private set; }

		public ValidationOperator Operator { get; private set; }
	}
}