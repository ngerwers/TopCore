using System.Collections.ObjectModel;
using TopCore2.Layouts;

namespace TopCore2;

public partial class MainPage : BasePage
{
    private readonly ObservableCollection<string> _listItems = new();
    private Entry? _newListItemEntry;

    public MainPage()
    {
        InitializeComponent();

        SetTitle("Hauptseite");

        BuildContent();
    }

    private void BuildContent()
    {
        var mainLayout = new VerticalStackLayout { Spacing = 15 };

        var inputLayout = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            ColumnSpacing = 10
        };

        _newListItemEntry = new Entry
        {
            Placeholder = "Neue Liste eingeben",
            TextColor = Colors.White,
            PlaceholderColor = Colors.LightGray
        };
        inputLayout.Children.Add(_newListItemEntry);
        Grid.SetColumn(_newListItemEntry, 0);

        var addButton = new Button
        {
            Text = "Hinzufügen"
        };
        addButton.Clicked += OnAddItemClicked;
        inputLayout.Children.Add(addButton);
        Grid.SetColumn(addButton, 1);

        mainLayout.Children.Add(inputLayout);

        var listCollectionView = new CollectionView
        {
            ItemsSource = _listItems,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { TextColor = Colors.White, FontSize = 18 };
                label.SetBinding(Label.TextProperty, ".");
                var border = new Border { Content = label, BackgroundColor = Color.FromArgb("#1C1C1E"), Padding = new Thickness(10) };
                return border;
            })
        };
        mainLayout.Children.Add(listCollectionView);

        SetContent(mainLayout);
    }

    private void OnAddItemClicked(object? sender, EventArgs e)
    {
        if (_newListItemEntry != null && !string.IsNullOrWhiteSpace(_newListItemEntry.Text))
        {
            _listItems.Add(_newListItemEntry.Text);
            _newListItemEntry.Text = string.Empty;
        }
    }
}