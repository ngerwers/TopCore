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
            Padding = 30
        };

        content.Children.Add(new Label
        {
            Text = "Informationen über uns",
            TextColor = Colors.White,
            FontSize = 24
        });

        SetContent(content);
    }
}