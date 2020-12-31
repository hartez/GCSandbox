using System;
using Xamarin.Forms;

namespace GCSandbox
{
	public static class TestHelpers
	{
		public static int Iterations = 100;

		public static void RunTests(Action createTestObjects) 
		{
			// Pre collect so our data is less messy
			Collect();

			var used = GC.GetTotalMemory(false);
			Log($"Before Starting, {used} bytes allocated.");

			Log($"Creating test items, {Iterations} iterations.");

			for (int n = 0; n < Iterations; n++)
			{
				createTestObjects();
			}

			var previousUsed = used;
			used = GC.GetTotalMemory(false);
			Log($"Before GC, {used} bytes allocated ({used - previousUsed} delta).");

			Collect();

			previousUsed = used;
			used = GC.GetTotalMemory(false);
			Log($"After GC, {used} bytes allocated ({used - previousUsed} delta).");
		}

		static void Collect() 
		{
			for (int n = 0; n < 3; n++)
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}

		public static void Log(string message) 
		{
			MessagingCenter.Send(Messages.Dispatch, Messages.Log, message);
		}

		public static void NotifyAllocation(string testType) 
		{
			MessagingCenter.Send(Messages.Dispatch, Messages.Allocated, testType);
		}

		public static void NotifyFinalization(string testType)
		{
			MessagingCenter.Send(Messages.Dispatch, Messages.Finalized, testType);
		}

		public static void NotifyDisposal(string testType)
		{
			MessagingCenter.Send(Messages.Dispatch, Messages.Disposal, testType);
		}
	}
}