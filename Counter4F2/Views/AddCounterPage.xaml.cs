using Counter4F2.Models;
using System.Diagnostics.Metrics;

namespace Counter4F2.Views;

public partial class AddCounterPage : ContentPage
{
    private AllCountersModel _counters;


    public AddCounterPage()
	{
		InitializeComponent();

        _counters = new AllCountersModel();
        _counters.LoadAllCounters();
	}

    


    private async void AddCounterBtn_Click(object sender, EventArgs e)
    {
        Color color = Color.FromRgb(byte.Parse(RedEditor.Text), byte.Parse(GreenEditor.Text), byte.Parse(BlueEditor.Text));

        string counterName = "Untitled counter";

        int defaultCounter = 0;

        if (string.IsNullOrEmpty(counterName))
        {
            counterName = CounterNameEditor.Text;
        }

        if (string.IsNullOrEmpty(counterName))
        {
            defaultCounter = int.Parse(DefaultCountEditor.Text);
        }

        await Shell.Current.GoToAsync($"../{nameof(CounterPage)}?{nameof(CounterPage.CounterId)}={_counters.AddCounter(counterName, defaultCounter, color.ToHex())}");
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
        if (string.IsNullOrEmpty(RedEditor.Text))
        {
            GreenEditor.Text = "0";
        }
    }

    private async void BackBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}