using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class Mul
	{
		[TestMethod]
		public void Mul_PositiveOneZero()
		{
			BigNumber num = 1;
			num *= 0;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Mul_PositiveOnePositiveOne()
		{
			BigNumber num = 1;
			num *= 1;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Mul_PositiveOnePositiveTwo()
		{
			BigNumber num = 1;
			num *= 2;
			Assert.AreEqual("2", num.ToString());
		}

		[TestMethod]
		public void Mul_PositiveOneNegativeOne()
		{
			BigNumber num = 1;
			num *= -1;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Mul_PositiveOneNegativeTwo()
		{
			BigNumber num = 1;
			num *= -2;
			Assert.AreEqual("-2", num.ToString());
		}

		[TestMethod]
		public void Mul_NegativeOneZero()
		{
			BigNumber num = -1;
			num *= 0;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Mul_NegativeOneNegativeOne()
		{
			BigNumber num = -1;
			num *= -1;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Mul_NegativeOneNegativeTwo()
		{
			BigNumber num = -1;
			num *= -2;
			Assert.AreEqual("2", num.ToString());
		}

		[TestMethod]
		public void Mul_NegativeOnePositiveOne()
		{
			BigNumber num = -1;
			num *= 1;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Mul_NegativeOnePositiveTwo()
		{
			BigNumber num = -1;
			num *= 2;
			Assert.AreEqual("-2", num.ToString());
		}

		[TestMethod]
		public void Mul_LongMaxValueLongMaxValue()
		{
			BigNumber num = long.MaxValue;
			num *= long.MaxValue;
			Assert.AreEqual("85070591730234615847396907784232501249", num.ToString());
		}
	}
}