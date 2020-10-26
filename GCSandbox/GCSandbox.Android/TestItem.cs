using Android.Widget;
using Android.Content;
using System.Threading;

namespace GCSandbox.Droid
{
	public class TestItem : TextView
	{
		public static int s_count;

		public TestItem(Context context) : base(context)
		{
			var count = Interlocked.Increment(ref s_count);
			//System.Diagnostics.Debug.WriteLine($">>>>>> Constructor, {count} allocated.");
		}

		public void TextChangedHandler(object sender, Android.Text.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the TextChanged event.");
		}

		~TestItem()
		{
			var count = Interlocked.Decrement(ref s_count);
			//System.Diagnostics.Debug.WriteLine($">>>>>> Finalizer, {count} allocated.");
		}
	}
}