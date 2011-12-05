namespace SVNMonitor.Settings.Validation
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class NumberConfigValidator : IConfigValidator
    {
        public NumberConfigValidator(ValidationOperator op, int compareTo)
        {
            this.Operator = op;
            this.CompareTo = compareTo;
        }

        private Predicate<int> GetPredicate()
        {
            switch (this.Operator)
            {
                case ValidationOperator.More:
                    return num => (num > this.CompareTo);

                case ValidationOperator.MoreOrEqual:
                    return num => (num >= this.CompareTo);

                case ValidationOperator.Less:
                    return num => (num < this.CompareTo);

                case ValidationOperator.LessOrEqual:
                    return num => (num <= this.CompareTo);
            }
            throw new NotSupportedException();
        }

        public object Validate(object value, out bool isValid)
        {
            int retValue = (int) value;
            Predicate<int> predicate = this.GetPredicate();
            isValid = predicate(retValue);
            return retValue;
        }

        public int CompareTo { get; private set; }

        public ValidationOperator Operator { get; private set; }
    }
}

