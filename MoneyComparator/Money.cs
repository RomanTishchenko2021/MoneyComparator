using System;

namespace MoneyComparator
{
    public class Money
    {
        public enum Currency { Dollar, Ruble, Euro };

        private Currency currency;

        public double Denomination { get; }

        public Money(double denomination, Currency currency)
        {
            if (denomination < 0) throw new ArgumentException("Denomination should be positive double-precision number.",
                nameof(denomination));

            this.Denomination = denomination;
            this.currency = currency;
        }

        public static double HowMuchIsItInDollars(Money money)
        {
            double value = 0;
            int currency = (int)money.currency;
            switch (currency)
            {
                case 0:
                    value = money.Denomination;
                    break;
                case 1:
                    value = money.Denomination * 0.013;
                    break;
                case 2:
                    value = money.Denomination * 1.18;
                    break;
            }
            return value;
        }

        public static Money operator -(Money left, Money right) =>
            left > right ?
                new Money(HowMuchIsItInDollars(left) - HowMuchIsItInDollars(right), Currency.Dollar)
                : new Money(HowMuchIsItInDollars(right) - HowMuchIsItInDollars(left), Currency.Dollar);

        public static Money operator +(Money left, Money right) =>
            new Money(HowMuchIsItInDollars(left) + HowMuchIsItInDollars(right), Currency.Dollar);

        public static bool operator >(Money left, Money right) =>
            HowMuchIsItInDollars(left) > HowMuchIsItInDollars(right);

        public static bool operator <(Money left, Money right) =>
            HowMuchIsItInDollars(left) < HowMuchIsItInDollars(right);
    }
}
