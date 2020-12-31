using Android.Widget;
using Android.Content;

namespace GCSandbox.Droid
{
	public class TestItem : TextView
	{
		private readonly string _testType;

		public TestItem(Context context, string testType) : base(context)
		{
			TestHelpers.NotifyAllocation(testType);
			_testType = testType;
		}

		public void TextChangedHandler(object sender, Android.Text.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the TextChanged event.");
		}

		~TestItem()
		{
			TestHelpers.NotifyFinalization(_testType);
		}
	}
}