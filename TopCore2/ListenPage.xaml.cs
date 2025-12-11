using TopCore2.Layouts;

namespace TopCore2;

public partial class ListenPage : BasePage
{
    public ListenPage()
    {
        InitializeComponent();
        SetTitle("Listen");
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
            Text = "Hier kommen deine Listen",
            TextColor = Colors.White,
            FontSize = 24
        });

        SetContent(content);
    }
}