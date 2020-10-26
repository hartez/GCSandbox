using GCSandbox.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(GarbageCollectionTester))]

namespace GCSandbox.iOS
{
	public class GarbageCollectionTester : IGarbageCollectionTester
    {
        public void RunTest()
        {
            TestStuff.RunTest(GenerateTestObjects, () => TestItem.s_count);
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
			new TestItem();
		}

		void WithLocalEventHandler()
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem();

			// And setting up a local handler for a native event
			// This means that the TestItem has a reference to this (GarbageCollectionTester)
			x.ValueChanged += LocalHandler;
		}

		void SelfEventHandler()
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem();

			// The class has its own event handler
			// So it's holding a reference to itself
			x.ValueChanged += x.TextChangedHandler;
		}

		void MutualEventHandlers()
		{
			// Creating two instances of a .NET subclass of a native type
			var x = new TestItem();
			var y = new TestItem();

			// Set each as the other's event handler
			// So each object will hold a reference to the other
			x.ValueChanged += y.TextChangedHandler;
			y.ValueChanged += x.TextChangedHandler;
		}

		private void LocalHandler(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}
