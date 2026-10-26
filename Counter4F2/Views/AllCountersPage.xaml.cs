using Counter4F2.Models;

namespace Counter4F2.Views;

public partial class AllCountersPage : ContentPage
{
	public AllCountersPage()
	{
		InitializeComponent();

		AllCountersModel allCountersModel = new();

		BindingContext = allCountersModel;
    }

    protected override void OnAppearing()
    {
        if (this.BindingContext is AllCountersModel allCounters)
        {
            allCounters.LoadAllCounters();
            allCounters.SaveAllCounters();
        } 
        base.OnAppearing();
    }

    private void DecraseCountBtn_Clicked(object sender, EventArgs e)
    {
		if (sender is Button ClickedButton)
		{
			if (ClickedButton.BindingContext is CounterModel _counter)
			{
                _counter.ChangeCount(-1);

            }
		}
    }

    private void IncraseCountBtn_Clicked(object sender, EventArgs e)
    {
        if (sender is Button ClickedButton)
        {
            if (ClickedButton.BindingContext is CounterModel _counter)
            {
                _counter.ChangeCount(1);

            }
        }
    }

    private async void CountersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var counter = (Models.CounterModel)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(CounterPage)}?{nameof(CounterPage.CounterId)}={counter.Id}");

            CountersCollevtionView.SelectedItem = null;
        }
    }

    private async void AddCounterBtn_Click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AddCounterPage)}");
    }
}