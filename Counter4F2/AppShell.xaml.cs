using Counter4F2.Views;

namespace Counter4F2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.CounterPage), typeof(Views.CounterPage));
            Routing.RegisterRoute(nameof(AddCounterPage), typeof(AddCounterPage));
        }
    }
}
