using TopCore2.Layouts;

namespace TopCore2.Layouts;

public partial class BasePage : ContentPage
{
    public BasePage()
    {
        InitializeComponent();
    }

    public void SetContent(View view)
    {
        ContentHost.Content = view;
    }

    // Methode um den gro�en Titel oben rechts zu setzen
    public void SetTitle(string title)
    {
        TitleLabel.Text = title;
    }

    private async void OnListenTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ListenPage");
    }

    private async void OnWieTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(WiePage));
    }

    private async void OnNeueListeTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NeueListePage));
    }

    private async void OnUeberUnsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(UeberUnsPage));
    }
}