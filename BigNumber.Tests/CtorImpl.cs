using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class CtorImpl
	{
		[TestMethod]
		public void Ctor_Impl_Zero()
		{
			BigNumber num = 0;
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_PositiveOne()
		{
			BigNumber num = 1;
			Assert.AreEqual("1", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_PositiveTen()
		{
			BigNumber num = 10;
			Assert.AreEqual("10", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_PositiveFifteen()
		{
			BigNumber num = 15;
			Assert.AreEqual("15", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_NegativeOne()
		{
			BigNumber num = -1;
			Assert.AreEqual("-1", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_NegativeTen()
		{
			BigNumber num = -10;
			Assert.AreEqual("-10", num.ToString());
		}

		[TestMethod]
		public void Ctor_Impl_NegativeFifteen()
		{
			BigNumber num = -15;
			Assert.AreEqual("-15", num.ToString());
		}
	}
}