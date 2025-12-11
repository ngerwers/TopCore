namespace TopCore2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(NeueListePage), typeof(NeueListePage));
            Routing.RegisterRoute(nameof(UeberUnsPage), typeof(UeberUnsPage));
            Routing.RegisterRoute(nameof(WiePage), typeof(WiePage));
        }
    }
}
