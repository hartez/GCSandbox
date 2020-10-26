using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml.Controls;

namespace GCSandbox.UWP
{
	public class TestItem : TextBox 
	{
		public static int s_count;

		public TestItem() 
		{
			var count = Interlocked.Increment(ref s_count);
			//Debug.WriteLine($">>>>>> Constructor, {count} allocated.");
		}

		~TestItem() 
		{
			var count = Interlocked.Decrement(ref s_count);
			//Debug.WriteLine($">>>>>> Finalizer, {count} allocated.");
		}

		public void TextChangedHandler(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}
