using Counter4F2.Models;

namespace Counter4F2
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}