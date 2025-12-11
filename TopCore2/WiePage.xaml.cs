using TopCore2.Layouts;

namespace TopCore2;

public partial class WiePage : BasePage
{
    public WiePage()
    {
        InitializeComponent();
        SetTitle("Wie?");
        BuildContent();
    }

    private void BuildContent()
    {
        var content = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = 30
        };

        content.Children.Add(new Label
        {
            Text = "Wie funktioniert das?",
            TextColor = Colors.White,
            FontSize = 24
        });

        SetContent(content);
    }
}