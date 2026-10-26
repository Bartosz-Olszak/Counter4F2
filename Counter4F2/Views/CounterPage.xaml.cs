using Counter4F2.Models;
using System.Threading.Tasks;

namespace Counter4F2.Views;

[QueryProperty(nameof(CounterId), nameof(CounterId))]
public partial class CounterPage : ContentPage
{
	public string CounterId
	{
		set { SetupCounter(int.Parse(value)); }
	}

	private async void SetupCounter(int id)
	{
        CounterGrid.BindingContext = AllCountersModel.LoadCounter(id);
        if (CounterGrid.BindingContext == null)
        {
            await Shell.Current.GoToAsync("..");
        }
    }

	public CounterPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (CounterGrid.BindingContext == null)
        {
            await Shell.Current.GoToAsync("..");
        }
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

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        if (CounterGrid.BindingContext is CounterModel counter)
        {
            AllCountersModel.DeleteCounter(counter.Id);
            CounterGrid.BindingContext = null;
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void BackBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void RedEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(RedEditor.Text))
        {
            RedEditor.Text = "0";
        }
    }

    private void GreenEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(GreenEditor.Text))
        {
            GreenEditor.Text = "0";
        }
    }

    private void BlueEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(BlueEditor.Text))
        {
            BlueEditor.Text = "0";
        }
    }

    private void ResetBtn_Clicked(object sender, EventArgs e)
    {
        if (CounterGrid.BindingContext is CounterModel counter)
        {
            counter.ResetCountToDefault();
        }
    }
}