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

    public void SetTitle(string title)
    {
        TitleLabel.Text = title;
    }
}