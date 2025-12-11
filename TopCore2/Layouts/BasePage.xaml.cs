using TopCore2.Layouts;

namespace TopCore2.Layouts;

public partial class BasePage : ContentPage
{
    public BasePage()
    {
        InitializeComponent();
    }

    // Methode um den Inhalt zu setzen (von den Kind-Seiten aufgerufen)
    public void SetContent(View view)
    {
        ContentHost.Content = view;
    }

    // Methode um den groﬂen Titel oben rechts zu setzen
    public void SetTitle(string title)
    {
        TitleLabel.Text = title;
    }

    // --- Navigation Events ---

    private async void OnListenTapped(object sender, EventArgs e)
    {
        // Verhindert Flackern, wenn man schon auf der Seite ist
        if (this.GetType() != typeof(ListenPage))
            await Navigation.PushAsync(new ListenPage());
    }

    private async void OnWieTapped(object sender, EventArgs e)
    {
        if (this.GetType() != typeof(WiePage))
            await Navigation.PushAsync(new WiePage());
    }

    private async void OnNeueListeTapped(object sender, EventArgs e)
    {
        if (this.GetType() != typeof(NeueListePage))
            await Navigation.PushAsync(new NeueListePage());
    }

    private async void OnUeberUnsTapped(object sender, EventArgs e)
    {
        if (this.GetType() != typeof(UeberUnsPage))
            await Navigation.PushAsync(new UeberUnsPage());
    }
}