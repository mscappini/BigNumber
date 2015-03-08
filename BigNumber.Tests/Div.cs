using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class Div
	{
		[TestMethod]
		[ExpectedException(typeof(DivideByZeroException), "Division by zero was inappropriately allowed.")]
		public void Div_PositiveOneZero()
		{
			BigNumber num = 1;
			num /= 0;
		}

		[TestMethod]
		public void Div_PositiveOnePositiveOne()
		{
			BigNumber num = 1;
			num /= 1;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Div_PositiveOnePositiveTwo()
		{
			BigNumber num = 1;
			num /= 2;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Div_PositiveOneNegativeOne()
		{
			BigNumber num = 1;
			num /= -1;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Div_PositiveOneNegativeTwo()
		{
			BigNumber num = 1;
			num /= -2;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Div_PositiveOneHundredPositiveTwo()
		{
			BigNumber num = 100;
			num /= 2;
			Assert.AreEqual("50", num.ToString());
		}

		[TestMethod]
		public void Div_PositiveOneHundredNegativeTwo()
		{
			BigNumber num = 100;
			num /= -2;
			Assert.AreEqual("-50", num.ToString());
		}

		[TestMethod]
		[ExpectedException(typeof(DivideByZeroException), "Division by zero was inappropriately allowed.")]
		public void Div_NegativeOneZero()
		{
			BigNumber num = -1;
			num /= 0;
		}

		[TestMethod]
		public void Div_NegativeOneNegativeOne()
		{
			BigNumber num = -1;
			num /= -1;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Div_NegativeOneNegativeTwo()
		{
			BigNumber num = -1;
			num /= -2;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Div_NegativeOnePositiveOne()
		{
			BigNumber num = -1;
			num /= 1;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Div_NegativeOnePositiveTwo()
		{
			BigNumber num = -1;
			num /= 2;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Div_NegativeOneHundredNegativeTwo()
		{
			BigNumber num = -100;
			num /= -2;
			Assert.AreEqual("50", num.ToString());
		}

		[TestMethod]
		public void Div_NegativeOneHundredPositiveTwo()
		{
			BigNumber num = -100;
			num /= 2;
			Assert.AreEqual("-50", num.ToString());
		}

		[TestMethod]
		public void Div_LongMaxValueLongMaxValue()
		{
			BigNumber num = long.MaxValue;
			num /= long.MaxValue;
			Assert.AreEqual("1", num.ToString());
		}
	}
}