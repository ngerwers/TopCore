namespace TopCore2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Direkt ein neues Fenster mit der AppShell zurückgeben
        return new Window(new AppShell());
    }
}