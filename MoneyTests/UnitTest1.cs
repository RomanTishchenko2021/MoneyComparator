using NUnit.Framework;
using MoneyComparator;
using System;

namespace MoneyTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldBeDoNotThrowExceptionDuringMoneyCreation()
        {
            Assert.Throws<ArgumentException>(() => new Money(-100, Money.Currency.Dollar));
        }

        [Test]
        public void ShouldBeThrowExceptionDuringMoneyCreation()
        {
            Assert.DoesNotThrow(() => new Money(100, Money.Currency.Dollar));
        }

        [Test]
        public void IsGreaterThanShouldbeFalse()
        {
            var dollars1 = new Money(100, Money.Currency.Dollar);
            var dollars2 = new Money(100, Money.Currency.Dollar);
            Assert.IsFalse(dollars1 > dollars2);

            var dollars3 = new Money(1000, Money.Currency.Dollar);
            Assert.IsFalse(dollars1 > dollars3);

            var rubles1 = new Money(100, Money.Currency.Ruble);
            Assert.IsFalse(rubles1 > dollars1);

            var euros1 = new Money(100, Money.Currency.Euro);
            Assert.IsFalse(dollars1 > euros1);

            Assert.IsFalse(rubles1 > euros1);
        }

        [Test]
        public void IsGreaterThanShouldbeTrue()
        {
            var dollars1 = new Money(100, Money.Currency.Dollar);
            var dollars2 = new Money(1000, Money.Currency.Dollar);
            Assert.IsTrue(dollars2 > dollars1);

            var rubles1 = new Money(100, Money.Currency.Ruble);
            Assert.IsTrue(dollars1 > rubles1);

            var euros1 = new Money(100, Money.Currency.Euro);
            Assert.IsTrue(euros1 > dollars1);

            Assert.IsTrue(euros1 > rubles1);
        }

        [Test]
        public void IsLessThanShouldbeFalse()
        {
            var dollars1 = new Money(100, Money.Currency.Dollar);
            var dollars2 = new Money(100, Money.Currency.Dollar);
            Assert.IsFalse(dollars1 < dollars2);

            var dollars3 = new Money(10, Money.Currency.Dollar);
            Assert.IsFalse(dollars1 < dollars3);

            var rubles1 = new Money(10000, Money.Currency.Ruble);
            Assert.IsFalse(rubles1 < dollars1);

            var euros1 = new Money(10, Money.Currency.Euro);
            Assert.IsFalse(dollars1 < euros1);

            Assert.IsFalse(rubles1 < euros1);
        }

        [Test]
        public void IsLessThanShouldbeTrue()
        {
            var dollars1 = new Money(1000, Money.Currency.Dollar);
            var dollars2 = new Money(100, Money.Currency.Dollar);
            Assert.IsTrue(dollars2 < dollars1);

            var rubles1 = new Money(100000, Money.Currency.Ruble);
            Assert.IsTrue(dollars1 < rubles1);

            var euros1 = new Money(10, Money.Currency.Euro);
            Assert.IsTrue(euros1 < dollars1);

            Assert.IsTrue(euros1 < rubles1);
        }

        [Test]
        public void IsAddValidOperation()
        {
            var dollars1 = new Money(100, Money.Currency.Dollar);
            Assert.AreEqual((dollars1 + dollars1).Denomination, 200);

            var rubles1 = new Money(100, Money.Currency.Ruble);
            Assert.AreEqual((dollars1 + rubles1).Denomination, 101.3);

            var euros1 = new Money(100, Money.Currency.Euro);
            Assert.AreEqual((dollars1 + euros1).Denomination, 218);

            Assert.AreEqual((rubles1 + euros1).Denomination, 119.3);
        }

        [Test]
        public void IsSubtractsValidOperation()
        {
            var dollars1 = new Money(100, Money.Currency.Dollar);
            Assert.AreEqual((dollars1 - dollars1).Denomination, 0);

            var dollars3 = new Money(10, Money.Currency.Dollar);
            Assert.AreEqual((dollars1 - dollars3).Denomination, 90);

            Assert.AreEqual((dollars3 - dollars1).Denomination, 90);

            var rubles1 = new Money(100, Money.Currency.Ruble);
            Assert.AreEqual((rubles1 - rubles1).Denomination, 0);
            Assert.AreEqual((dollars1 - rubles1).Denomination, 100 - 100 * 0.013);

            Assert.AreEqual((rubles1 - dollars1).Denomination, 100 - 100 * 0.013);

            var euros1 = new Money(100, Money.Currency.Euro);
            Assert.AreEqual((euros1 - euros1).Denomination, 0);
            Assert.AreEqual((euros1 - dollars1).Denomination, 100 * 1.18 - 100);

            Assert.AreEqual((dollars1 - euros1).Denomination, 100 * 1.18 - 100);

            Assert.AreEqual((rubles1 - euros1).Denomination, 100 * 1.18 - 100 * 0.013);

            Assert.AreEqual((euros1 - rubles1).Denomination, 100 * 1.18 - 100 * 0.013);
        }
    }
}