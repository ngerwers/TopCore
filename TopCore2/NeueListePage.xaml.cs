using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using TopCore2.Layouts;
using TopCore2.Models;

namespace TopCore2;

public partial class NeueListePage : BasePage
{
    private ObservableCollection<ListItem> _listItems = new();
    private Entry? _newItemEntry;

    public NeueListePage()
    {
        InitializeComponent();
        SetTitle("Neue Liste");
        BuildContent();
        LoadData();
    }

    private void BuildContent()
    {
        var mainLayout = new VerticalStackLayout { Spacing = 20, Padding = 30 };

        mainLayout.Children.Add(new Label { Text = "Titel", TextColor = Colors.White, FontSize = 18 });
        var titleEntry = new Entry { Placeholder = "Titel der Liste" };
        var titleEntryBorder = new Border
        {
            Content = titleEntry,
            Stroke = Color.FromArgb("#0055FF"),
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
        };
        mainLayout.Children.Add(titleEntryBorder);

        mainLayout.Children.Add(new Label { Text = "Listen Wichtigkeit 1 - 10", TextColor = Colors.White, FontSize = 18 });
        var prioritySlider = new Slider { Minimum = 1, Maximum = 10, Value = 5 };
        mainLayout.Children.Add(prioritySlider);

        var listCollectionView = new CollectionView
        {
            ItemsSource = _listItems,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { TextColor = Colors.White, FontSize = 16 };
                label.SetBinding(Label.TextProperty, "Text");
                return new Border { Content = label, BackgroundColor = Color.FromArgb("#1A1A1A"), StrokeThickness = 1, Stroke = Color.FromArgb("#0055FF"), StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) }, Padding = new Thickness(10) };
            })
        };
        mainLayout.Children.Add(listCollectionView);

        _newItemEntry = new Entry { Placeholder = "Neues Item" };
        var newItemEntryBorder = new Border
        {
            Content = _newItemEntry,
            Stroke = Color.FromArgb("#0055FF"),
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
        };
        mainLayout.Children.Add(newItemEntryBorder);
        
        var buttonsLayout = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            ColumnSpacing = 20
        };
        
        var addItemButton = new Button { Text = "+ Neues Item" };
        addItemButton.Clicked += OnAddItemClicked;
        buttonsLayout.Children.Add(addItemButton);
        Grid.SetColumn(addItemButton, 0);

        var saveListButton = new Button { Text = "Liste Speichern" };
        saveListButton.Clicked += OnSaveListClicked;
        buttonsLayout.Children.Add(saveListButton);
        Grid.SetColumn(saveListButton, 1);
        
        mainLayout.Children.Add(buttonsLayout);

        SetContent(mainLayout);
    }
    
    private void LoadData()
    {
        _listItems.Add(new ListItem { Text = "Star Wars 2" });
        _listItems.Add(new ListItem { Text = "Home Alone" });
    }

    private void OnAddItemClicked(object? sender, EventArgs e)
    {
        if (_newItemEntry != null && !string.IsNullOrWhiteSpace(_newItemEntry.Text))
        {
            _listItems.Add(new ListItem { Text = _newItemEntry.Text });
            _newItemEntry.Text = string.Empty;
        }
    }
    
    private async void OnSaveListClicked(object? sender, EventArgs e)
    {
        // TODO: Implement save logic
        await Shell.Current.GoToAsync("..");
    }
}