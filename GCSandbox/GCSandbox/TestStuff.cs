using System;

namespace GCSandbox
{
	public static class TestStuff
	{
		public static int ItemsToCreate = 100;

		public static void RunTest(Action createTestObjects, Func<int> getAllocatedCount) 
		{
			for (int n = 0; n < ItemsToCreate; n++)
			{
				createTestObjects();
			}

			var used = GC.GetTotalMemory(false);
			System.Diagnostics.Debug.WriteLine($">>>>>> Before GC, {used} bytes allocated.");

			for (int n = 0; n < 3; n++) 
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			var allocated = getAllocatedCount();
			System.Diagnostics.Debug.WriteLine($">>>>>> After GC, {allocated} objects are live.");

			used = GC.GetTotalMemory(false);
			System.Diagnostics.Debug.WriteLine($">>>>>> After GC, {used} bytes allocated.");
		}
	}
}