using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class Add
	{
		[TestMethod]
		public void Add_PositiveTenZero()
		{
			BigNumber num = 10;
			num += 0;
			Assert.AreEqual("10", num.ToString());
		}

		[TestMethod]
		public void Add_NegativeTenZero()
		{
			BigNumber num = -10;
			num += 0;
			Assert.AreEqual("-10", num.ToString());
		}

		[TestMethod]
		public void Add_PositiveElevenPositiveTen()
		{
			BigNumber num = 11;
			num += 10;
			Assert.AreEqual("21", num.ToString());
		}

		[TestMethod]
		public void Add_PositiveTenPositiveTen()
		{
			BigNumber num = 10;
			num += 10;
			Assert.AreEqual("20", num.ToString());
		}

		[TestMethod]
		public void Add_PositiveTenPositiveEleven()
		{
			BigNumber num = 10;
			num += 11;
			Assert.AreEqual("21", num.ToString());
		}

		[TestMethod]
		public void Add_PositiveTenNegativeEleven()
		{
			BigNumber num = 10;
			num += -11;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Add_PositiveElevenNegativeTen()
		{
			BigNumber num = 11;
			num += -10;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Add_NegativeTenPositiveEleven()
		{
			BigNumber num = -10;
			num += 11;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Add_NegativeElevenPositiveTen()
		{
			BigNumber num = -11;
			num += 10;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Add_NegativeTenNegativeEleven()
		{
			BigNumber num = -10;
			num += -11;
			Assert.AreEqual("-21", num.ToString());
		}

		[TestMethod]
		public void Add_NegativeElevenNegativeTen()
		{
			BigNumber num = -11;
			num += -10;
			Assert.AreEqual("-21", num.ToString());
		}
	}
}