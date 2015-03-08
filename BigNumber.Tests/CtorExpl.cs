using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class CtorExpl
	{
		[TestMethod]
		public void Ctor_Expl_EmptyZero()
		{
			BigNumber num = new BigNumber();
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_Zero()
		{
			BigNumber num = new BigNumber(0);
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_PositiveOne()
		{
			BigNumber num = new BigNumber(1);
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_PositiveTen()
		{
			BigNumber num = new BigNumber(10);
			Assert.AreEqual("10", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_PositiveFifteen()
		{
			BigNumber num = new BigNumber(15);
			Assert.AreEqual("15", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_NegativeOne()
		{
			BigNumber num = new BigNumber(-1);
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_NegativeTen()
		{
			BigNumber num = new BigNumber(-10);
			Assert.AreEqual("-10", num.ToString());
		}

		[TestMethod]
		public void Ctor_Expl_NegativeFifteen()
		{
			BigNumber num = new BigNumber(-15);
			Assert.AreEqual("-15", num.ToString());
		}
	}
}