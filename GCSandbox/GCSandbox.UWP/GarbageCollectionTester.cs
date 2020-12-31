using GCSandbox.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(GarbageCollectionTester))]

namespace GCSandbox.UWP
{
	public class GarbageCollectionTester : IGarbageCollectionTester
	{
		public void RunTest()
		{
			TestHelpers.RunTests(GenerateTestObjects);
		}

		void GenerateTestObjects()
		{
			CreateItemOnly();
			WithLocalEventHandler();
			SelfEventHandler();
			MutualEventHandlers();
		}

		void CreateItemOnly()
		{
			// Creating an instance of a .NET subclass of a native type
			new TestItem("CreateOnly");
		}

		void WithLocalEventHandler()
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem("WithLocalEventHandler");

			// And setting up a local handler for a native event
			// This means that the TestItem has a reference to this (GarbageCollectionTester)
			x.TextChanged += LocalHandler;
		}

		void SelfEventHandler()
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem("SelfEventHandler");

			// The class has its own event handler
			// So it's holding a reference to itself
			x.TextChanged += x.TextChangedHandler;
		}

		void MutualEventHandlers()
		{
			// Creating two instances of a .NET subclass of a native type
			var x = new TestItem("MutualEventHandlers");
			var y = new TestItem("MutualEventHandlers");

			// Set each as the other's event handler
			// So each object will hold a reference to the other
			x.TextChanged += y.TextChangedHandler;
			y.TextChanged += x.TextChangedHandler;
		}

		private void LocalHandler(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}
