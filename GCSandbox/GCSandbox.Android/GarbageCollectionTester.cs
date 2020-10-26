using Android.Content;
using Android.Views;
using System.Threading;
using static Android.Views.View;

namespace GCSandbox.Droid
{
	public class GarbageCollectionTester : IGarbageCollectionTester
	{
		private readonly Context _context;

		public GarbageCollectionTester(Context context)
		{
			_context = context;
		}

		public void RunTest()
		{
			TestStuff.RunTest(GenerateTestObjects, () => TestItem.s_count);
		}

		void GenerateTestObjects() 
		{
			CreateItemOnly();
			WithLocalEventHandler();
			WithListener();
			SelfEventHandler();
			MutualEventHandlers();
		}

		void CreateItemOnly() 
		{
			// Creating an instance of a .NET subclass of a native type
			new TestItem(_context);
		}

		void WithLocalEventHandler() 
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem(_context);

			// And setting up a local handler for a native event
			// This means that the TestItem has a reference to this (GarbageCollectionTester)
			x.TextChanged += LocalHandler;
		}
		
		void WithListener()
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem(_context);

			// Set up a listener
			x.AddOnLayoutChangeListener(new LayoutChangeListener());
		}

		void SelfEventHandler() 
		{
			// Creating an instance of a .NET subclass of a native type
			var x = new TestItem(_context);

			// The class has its own event handler
			// So it's holding a reference to itself
			x.TextChanged += x.TextChangedHandler;
		}

		void MutualEventHandlers() 
		{
			// Creating two instances of a .NET subclass of a native type
			var x = new TestItem(_context);
			var y = new TestItem(_context);

			// Set each as the other's event handler
			// So each object will hold a reference to the other
			x.TextChanged += y.TextChangedHandler;
			y.TextChanged += x.TextChangedHandler;
		}

		class LayoutChangeListener : Java.Lang.Object, IOnLayoutChangeListener
		{
			public void OnLayoutChange(View v, int left, int top, int right, int bottom, int oldLeft, int oldTop, int oldRight, int oldBottom)
			{
				System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for OnLayoutChange.");
			}

			public static int s_count;

			public LayoutChangeListener() 
			{
				var count = Interlocked.Increment(ref s_count);
				System.Diagnostics.Debug.WriteLine($">>>>>> LayoutChangeListener Constructor, {count} allocated.");
			}

			~LayoutChangeListener()
			{
				var count = Interlocked.Decrement(ref s_count);
				System.Diagnostics.Debug.WriteLine($">>>>>> LayoutChangeListener Finalizer, {count} allocated.");
			}
		}

		void LocalHandler(object sender, Android.Text.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}