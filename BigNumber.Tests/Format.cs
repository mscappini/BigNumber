using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumber.Tests
{
	[TestClass]
	public class Format
	{
		[TestMethod]
		public void Format_Default_Empty()
		{
			BigNumber num = new BigNumber();
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Format_Default_Zero()
		{
			BigNumber num = new BigNumber(0);
			Assert.AreEqual("0", num.ToString());
		}

		[TestMethod]
		public void Format_Default_OneHundred()
		{
			BigNumber num = new BigNumber(0100);
			Assert.AreEqual("100", num.ToString());
		}

		[TestMethod]
		public void Format_Number_OneHundred()
		{
			BigNumber num = new BigNumber(100);
			Assert.AreEqual("100", num.ToString("N"));
		}

		[TestMethod]
		public void Format_Number_OneThousand()
		{
			BigNumber num = new BigNumber(1000);
			Assert.AreEqual("1,000", num.ToString("N"));
		}

		[TestMethod]
		public void Format_Number_OneBillion()
		{
			BigNumber num = new BigNumber(1000000000);
			Assert.AreEqual("1,000,000,000", num.ToString("N"));
		}
	}
}