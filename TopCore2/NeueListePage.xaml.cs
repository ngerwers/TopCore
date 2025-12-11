using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using TopCore2.Layouts;
using TopCore2.Models;

namespace TopCore2;

public partial class NeueListePage : BasePage
{
    private ObservableCollection<ListItem> _listItems = new();


    public NeueListePage()
    {
        InitializeComponent();
        SetTitle("Neue Liste");
        BuildContent();
    }

    private void BuildContent()
    {
        var mainLayout = new VerticalStackLayout { Spacing = 20 };

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
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    },
                    ColumnSpacing = 10
                };

                var itemEntry = new Entry { TextColor = Colors.White, BackgroundColor = Colors.Transparent };
                itemEntry.SetBinding(Entry.TextProperty, "Text");
                grid.Children.Add(itemEntry);
                Grid.SetColumn(itemEntry, 0);

                var deleteButton = new Button { Text = "X", TextColor = Colors.Red };
                deleteButton.Clicked += (s, e) =>
                {
                    var item = (ListItem)deleteButton.BindingContext;
                    _listItems.Remove(item);
                };
                grid.Children.Add(deleteButton);
                Grid.SetColumn(deleteButton, 1);
                
                return new Border { Content = grid, BackgroundColor = Color.FromArgb("#1A1A1A"), StrokeThickness = 1, Stroke = Color.FromArgb("#0055FF"), StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) }, Padding = new Thickness(10) };
            })
        };
        mainLayout.Children.Add(listCollectionView);
        
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
    


    private void OnAddItemClicked(object? sender, EventArgs e)
    {
        _listItems.Add(new ListItem { Text = "" });
    }
    
    private async void OnSaveListClicked(object? sender, EventArgs e)
    {
        // TODO: Implement save logic
        await Shell.Current.GoToAsync("..");
    }
}