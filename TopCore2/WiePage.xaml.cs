using TopCore2.Layouts;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;

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
            Spacing = 20
        };

        // Section 1: Anleitung
        content.Children.Add(new Label
        {
            Text = "Kurzanleitung",
            TextColor = Colors.White,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold
        });

        content.Children.Add(new Label
        {
            Text = "Willkommen bei TopCore! Mit dieser App kannst du deine Aufgaben priorisieren. Erstelle neue Listen über das Menü, gib ihnen eine Wichtigkeit von 1-10 und behalte den Überblick. Nutze die Sortierfunktion auf der Startseite, um das Wichtigste zuerst zu sehen.",
            TextColor = Colors.LightGray,
            FontSize = 16
        });

        // Separator
        content.Children.Add(new BoxView { Color = Colors.Gray, HeightRequest = 1, Margin = new Thickness(0, 20) });

        // Section 2: FAQ
        content.Children.Add(new Label
        {
            Text = "Häufige Fragen (FAQ)",
            TextColor = Colors.White,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold
        });

        // FAQ 1
        var expander1 = new Expander();
        expander1.Header = new Label { Text = "Wie lösche ich eine Liste?", TextColor = Colors.White, FontSize = 18 };
        expander1.Content = new Label { Text = "Klicke in der Listenansicht auf das Mülleimer-Symbol. Du musst das Löschen bestätigen.", TextColor = Colors.LightGray, Padding = new Thickness(10) };
        content.Children.Add(expander1);

        // FAQ 2
        var expander2 = new Expander();
        expander2.Header = new Label { Text = "Werden meine Daten gespeichert?", TextColor = Colors.White, FontSize = 18 };
        expander2.Content = new Label { Text = "Ja, alle Listen werden lokal auf deinem Gerät gesichert.", TextColor = Colors.LightGray, Padding = new Thickness(10) };
        content.Children.Add(expander2);

        // FAQ 3
        var expander3 = new Expander();
        expander3.Header = new Label { Text = "Kann ich Listen bearbeiten?", TextColor = Colors.White, FontSize = 18 };
        expander3.Content = new Label { Text = "Ja, klicke einfach auf das Bearbeiten-Symbol (Stift) neben einer Liste.", TextColor = Colors.LightGray, Padding = new Thickness(10) };
        content.Children.Add(expander3);

        var scrollView = new ScrollView { Content = content };
        SetContent(scrollView);
    }
}