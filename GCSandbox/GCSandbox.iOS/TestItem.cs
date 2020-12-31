using System.Threading;
using UIKit;
using Xamarin.Forms;

namespace GCSandbox.iOS
{
	public class TestItem : UITextField // Change this to a UILabel or UIButton and the GC can clean it up just fine
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

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				TestHelpers.NotifyDisposal(_testType);
			}

			base.Dispose(disposing);
		}

		public void TextChangedHandler(object sender, System.EventArgs e)
		{
		}
	}
}
