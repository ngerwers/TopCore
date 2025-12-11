using TopCore2.Layouts;

namespace TopCore2;

public partial class NeueListePage : BasePage
{
    public NeueListePage()
    {
        InitializeComponent();
        SetTitle("Neue Liste");
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
            Text = "Erstelle eine neue Liste",
            TextColor = Colors.White,
            FontSize = 24
        });

        SetContent(content);
    }
}