namespace TopCore2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Verwende NavigationPage für Navigation
        MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.Black,
            BarTextColor = Colors.White
        };
    }
}