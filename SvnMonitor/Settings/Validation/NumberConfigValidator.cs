using System;

namespace SVNMonitor.Settings.Validation
{
internal class NumberConfigValidator : IConfigValidator
{
	public int CompareTo
	{
		get;
		private set;
	}

	public ValidationOperator Operator
	{
		get;
		private set;
	}

	public NumberConfigValidator(ValidationOperator op, int compareTo)
	{
		this.Operator = op;
		this.CompareTo = compareTo;
	}

	private Predicate<int> GetPredicate()
	{
		Predicate<int> predicate1 = null;
		Predicate<int> predicate2 = null;
		Predicate<int> predicate3 = null;
		Predicate<int> predicate4 = null;
		switch (this.Operator)
		{
			case ValidationOperator.More:
			{
				if (predicate1 == null)
				{
					predicate1 = new Predicate<int>(this.<GetPredicate>b__0);
				}
				return predicate1;
			}
			case ValidationOperator.MoreOrEqual:
			{
				if (predicate2 == null)
				{
					predicate2 = new Predicate<int>(this.<GetPredicate>b__1);
				}
				return predicate2;
			}
			case ValidationOperator.Less:
			{
				if (predicate3 == null)
				{
					predicate3 = new Predicate<int>(this.<GetPredicate>b__2);
				}
				return predicate3;
			}
			case ValidationOperator.LessOrEqual:
			{
				if (predicate4 == null)
				{
					predicate4 = new Predicate<int>(this.<GetPredicate>b__3);
				}
				return predicate4;
			}
		}
		throw new NotSupportedException();
	}

	public object Validate(object value, out bool isValid)
	{
		int retValue = (int)value;
		Predicate<int> predicate = this.GetPredicate();
		isValid = predicate(retValue);
		return retValue;
	}
}
}