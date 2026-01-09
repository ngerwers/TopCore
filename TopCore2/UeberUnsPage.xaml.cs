using TopCore2.Layouts;

namespace TopCore2;

public partial class UeberUnsPage : BasePage
{
    public UeberUnsPage()
    {
        InitializeComponent();
        SetTitle("Über Uns");
        BuildContent();
    }

    private void BuildContent()
    {
        var content = new VerticalStackLayout
        {
            Spacing = 20,
            VerticalOptions = LayoutOptions.Start
        };

        content.Children.Add(new Image
        {
            Source = "logo_dark.png",
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.Start
        });

        content.Children.Add(new Label
        {
            Text = "App Version: 1.0",
            TextColor = Colors.White,
            FontSize = 14
        });

        content.Children.Add(new Label
        {
            Text = "Entwickelt von: Simon Gerwer",
            TextColor = Colors.White,
            FontSize = 14
        });

        content.Children.Add(new Label
        {
            Text = "TopCore ist dein Tool für besseres Task-Management.",
            TextColor = Colors.White,
            FontSize = 16
        });

        SetContent(content);
    }
}