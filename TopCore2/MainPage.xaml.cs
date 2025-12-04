using TopCore2.Layouts;

namespace TopCore2;

public partial class MainPage : BasePage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        // Setze den Titel
        SetTitle("Hauptseite");

        // Erstelle den Inhalt
        BuildContent();
    }

    private void BuildContent()
    {
        var layout = new VerticalStackLayout
        {
            Padding = new Thickness(30, 0),
            Spacing = 25
        };

        // Image
        layout.Children.Add(new Image
        {
            Source = "dotnet_bot.png",
            HeightRequest = 185,
            Aspect = Aspect.AspectFit
        });

        // Hello Label
        layout.Children.Add(new Label
        {
            Text = "Hello, World!",
            TextColor = Colors.White,
            FontSize = 32,
            FontAttributes = FontAttributes.Bold
        });

        // Welcome Label
        layout.Children.Add(new Label
        {
            Text = "Willkommen zu deiner App mit BasePage Layout!",
            TextColor = Colors.LightGray,
            FontSize = 18
        });

        // Button
        var button = new Button
        {
            Text = "Click me",
            HorizontalOptions = LayoutOptions.Fill
        };
        button.Clicked += OnCounterClicked;
        layout.Children.Add(button);

        // Setze den Content
        SetContent(new ScrollView { Content = layout });
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;
        var button = (Button)sender;

        if (count == 1)
            button.Text = $"Clicked {count} time";
        else
            button.Text = $"Clicked {count} times";
    }
}