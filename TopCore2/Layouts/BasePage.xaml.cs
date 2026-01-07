using TopCore2.Layouts;

namespace TopCore2.Layouts;

public partial class BasePage : ContentPage
{
    public BasePage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Ermöglicht das Setzen des Hauptinhalts der Seite von abgeleiteten Klassen.
    /// </summary>
    public View? MainContent
    {
        get => ContentHost.Content;
        set => ContentHost.Content = value;
    }

    public void SetTitle(string title)
    {
        TitleLabel.Text = title;
    }

    // --- Navigation Events ---

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
