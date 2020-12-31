using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCSandbox
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		readonly IGarbageCollectionTester _tester = DependencyService.Resolve<IGarbageCollectionTester>();
		readonly Dictionary<string, LiveObjectCount> _data = new Dictionary<string, LiveObjectCount>();

		public MainPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<Messages, string>(this, Messages.Allocated, TrackAllocation);
			MessagingCenter.Subscribe<Messages, string>(this, Messages.Finalized, TrackFinalized);
			MessagingCenter.Subscribe<Messages, string>(this, Messages.Disposal, TrackDisposal);
			MessagingCenter.Subscribe<Messages, string>(this, Messages.Log, LogMessage);
		}

		private void TrackDisposal(Messages dispatch, string testType)
		{
			_data[testType].Decrement();
		}

		void TrackFinalized(Messages dispatch, string testType)
		{
			_data[testType].Decrement();
		}

		void TrackAllocation(Messages dispatch, string testType)
		{
			if (_data.TryGetValue(testType, out LiveObjectCount count))
			{
				count.Increment();
			}
			else
			{
				_data[testType] = new LiveObjectCount { TestType = testType, Count = 1 };
			}
		}

		void LogMessage(Messages dispatch, string message)
		{
			Output(message);
		}

		void RunTestClicked(object sender, EventArgs e)
		{
			Log.Children.Clear();

			Output("Starting test ...");
			_tester.RunTest();
			Output("Finished.");

			foreach (var value in _data.Values)
			{
				Output(value.ToString());
			}

			var total = _data.Values.Sum(t => t.Count);

			Output($"Total Live Objects: {total}");
		}

		void Output(string message)
		{
			Log.Children.Add(new Label { Text = message });
		}
	}
}