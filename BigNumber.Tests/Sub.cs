using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class Sub
	{
		[TestMethod]
		public void Sub_PositiveTenZero()
		{
			BigNumber num = 10;
			num -= 0;
			Assert.AreEqual("10", num.ToString());
		}

		[TestMethod]
		public void Sub_NegativeTenZero()
		{
			BigNumber num = -10;
			num -= 0;
			Assert.AreEqual("-10", num.ToString());
		}

		[TestMethod]
		public void Sub_PositiveElevenPositiveTen()
		{
			BigNumber num = 11;
			num -= 10;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Sub_PositiveTenPositiveTen()
		{
			BigNumber num = 10;
			num -= 10;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Sub_PositiveTenPositiveEleven()
		{
			BigNumber num = 10;
			num -= 11;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Sub_PositiveTenNegativeEleven()
		{
			BigNumber num = 10;
			num -= -11;
			Assert.AreEqual("21", num.ToString());
		}

		[TestMethod]
		public void Sub_PositiveElevenNegativeTen()
		{
			BigNumber num = 11;
			num -= -10;
			Assert.AreEqual("21", num.ToString());
		}

		[TestMethod]
		public void Sub_NegativeTenPositiveEleven()
		{
			BigNumber num = -10;
			num -= 11;
			Assert.AreEqual("-21", num.ToString());
		}

		[TestMethod]
		public void Sub_NegativeElevenPositiveTen()
		{
			BigNumber num = -11;
			num -= 10;
			Assert.AreEqual("-21", num.ToString());
		}

		[TestMethod]
		public void Sub_NegativeTenNegativeEleven()
		{
			BigNumber num = -10;
			num -= -11;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Sub_NegativeElevenNegativeTen()
		{
			BigNumber num = -11;
			num -= -10;
			Assert.AreEqual("-1", num.ToString());
		}
	}
}