using System;

namespace BigNumber.Runner
{
	class Program
	{
		static void Main(string[] args)
		{
			BigNumber ctr = long.MaxValue; //9,223,372,036,854,775,807

			ctr *= long.MaxValue;

			//BigNumber ctr = long.MaxValue;
			//for (int i = 0; i < 100; i++)
			//	ctr *= long.MaxValue;

			//for (int i = 0; i < 100; i++)
			//	ctr /= long.MaxValue;

			Console.WriteLine(ctr);
			Console.ReadLine();
		}
	}
}