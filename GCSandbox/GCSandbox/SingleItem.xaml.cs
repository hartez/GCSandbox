using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCSandbox
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SingleItem : ContentPage
	{
		IGarbageCollectionTester _tester = DependencyService.Resolve<IGarbageCollectionTester>();

		public SingleItem()
		{
			InitializeComponent();
		}

		void RunTestClicked(object sender, EventArgs e)
		{
			_tester.RunTest();
		}
	}
}