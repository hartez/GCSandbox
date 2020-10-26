using System.Threading;
using System.Diagnostics;
using UIKit;

namespace GCSandbox.iOS
{
	public class TestItem : UITextField
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
            Debug.WriteLine($">>>>>> Finalizer, {count} allocated.");
        }

		public void TextChangedHandler(object sender, System.EventArgs e)
		{
			Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}
