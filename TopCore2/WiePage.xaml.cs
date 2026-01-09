using TopCore2.Layouts;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics; // <--- WICHTIG: Hier wohnt die "Color" Klasse
using System.Collections.Generic;

namespace TopCore2;

public partial class WiePage : BasePage
{
    // Wir speichern Referenzen
    private Button _submitButton;
    private string _selectedOption = string.Empty;
    
    // Deine Farbe definieren (Parsen via FromHex)
    private readonly Color _primaryColor = Color.FromHex("#0055FF");

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
            Padding = new Thickness(0, 0, 0, 40)
        };

        // --- Section 1: Anleitung ---
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

        // --- Section 2: FAQ ---
        content.Children.Add(new Label
        {
            Text = "Häufige Fragen (FAQ)",
            TextColor = Colors.White,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold
        });

        content.Children.Add(CreateExpander("Wie lösche ich eine Liste?", "Klicke in der Listenansicht auf das Mülleimer-Symbol. Du musst das Löschen bestätigen."));
        content.Children.Add(CreateExpander("Werden meine Daten gespeichert?", "Ja, alle Listen werden lokal auf deinem Gerät gesichert."));
        content.Children.Add(CreateExpander("Kann ich Listen bearbeiten?", "Ja, klicke einfach auf das Bearbeiten-Symbol (Stift) neben einer Liste."));

        // Separator
        content.Children.Add(new BoxView { Color = Colors.Gray, HeightRequest = 1, Margin = new Thickness(0, 20) });

        // --- Section 3: Auswahl mit echten Radio Buttons ---
        content.Children.Add(new Label
        {
            Text = "War das hilfreich?",
            TextColor = Colors.White,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold
        });

        // Container für die RadioButtons
        var radioGroup = new VerticalStackLayout 
        { 
            Spacing = 10,
            Padding = new Thickness(10, 0)
        };

        // RadioButtons erstellen
        radioGroup.Children.Add(CreateRadioButton("Ja, sehr"));
        radioGroup.Children.Add(CreateRadioButton("Geht so"));
        radioGroup.Children.Add(CreateRadioButton("Nein"));

        content.Children.Add(radioGroup);

        // --- Der Absenden Button ---
        _submitButton = new Button
        {
            Text = "Feedback absenden",
            BackgroundColor = Colors.Gray, // Startfarbe (inaktiv)
            TextColor = Colors.White,
            CornerRadius = 25,
            HeightRequest = 50,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            IsEnabled = false, // Zuerst deaktiviert!
            Opacity = 0.5,     // Optisch ausgegraut
            Margin = new Thickness(0, 20, 0, 0)
        };

        _submitButton.Clicked += OnSubmitClicked;

        content.Children.Add(_submitButton);

        // Alles in ScrollView packen
        var scrollView = new ScrollView { Content = content };
        SetContent(scrollView);
    }

    // --- Helper Methoden ---

    private Expander CreateExpander(string title, string details)
    {
        var expander = new Expander();
        expander.Header = new Label { Text = title, TextColor = Colors.White, FontSize = 18, FontAttributes = FontAttributes.Bold };
        expander.Content = new Label { Text = details, TextColor = Colors.LightGray, Padding = new Thickness(10, 5, 0, 10) };
        return expander;
    }

    private RadioButton CreateRadioButton(string text)
    {
        // HINWEIS: RadioButtons haben in C# keine direkte "Color"-Eigenschaft für den Kreis.
        // Das Aussehen wird meist vom System bestimmt. Wir setzen hier TextColor.
        var rb = new RadioButton
        {
            Content = text,
            TextColor = Colors.White,
            FontSize = 18
        };

        // Event für Zustandsänderung
        rb.CheckedChanged += OnRadioButtonChanged;
        
        return rb;
    }

    // --- Event Logic ---

    private void OnRadioButtonChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // Wenn ausgewählt
        {
            var rb = sender as RadioButton;
            if (rb != null)
            {
                _selectedOption = rb.Content.ToString();

                // Absenden Button aktivieren und einfärben
                _submitButton.IsEnabled = true;
                _submitButton.Opacity = 1.0;
                _submitButton.BackgroundColor = _primaryColor; // Dein Blau (#0055FF)
            }
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_selectedOption))
        {
            await DisplayAlert("Danke!", $"Du hast gewählt: {_selectedOption}", "OK");
        }
    }
}