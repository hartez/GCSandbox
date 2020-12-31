using Windows.UI.Xaml.Controls;

namespace GCSandbox.UWP
{
	public class TestItem : TextBox 
	{
		private readonly string _testType;

		public TestItem(string testType) 
		{
			TestHelpers.NotifyAllocation(testType);
			_testType = testType;
		}

		~TestItem() 
		{
			TestHelpers.NotifyFinalization(_testType);
		}

		public void TextChangedHandler(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($">>>>>> This is the handler for the event.");
		}
	}
}
