using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using TopCore2.Layouts;
using TopCore2.Models;

namespace TopCore2;

public partial class ListenPage : BasePage
{
    private ObservableCollection<object> _listItems = new();

    public ListenPage()
    {
        InitializeComponent();
        SetTitle("Meine Listen");
        BuildContent();
        _listItems.Add(new AddListItem());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadLists();
    }

    private async Task LoadLists()
    {
        var lists = await Services.ListService.GetAllLists();
        _listItems.Clear();
        _listItems.Add(new AddListItem());
        foreach (var list in lists)
        {
            _listItems.Add(list);
        }
    }

    private void BuildContent()
    {
        var mainLayout = new VerticalStackLayout { Spacing = 20 };

        var controlsLayout = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },
            ColumnSpacing = 20
        };

        var sortPicker = new Picker { Title = "Sortieren" };
        sortPicker.ItemsSource = new[] { "Datum", "Name", "Anzahl" };
        controlsLayout.Children.Add(sortPicker);
        Grid.SetColumn(sortPicker, 0);

        var searchEntry = new Entry { Placeholder = "Listen Suchen" };
        var searchEntryBorder = new Border
        {
            Content = searchEntry,
            Stroke = Color.FromArgb("#0055FF"),
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
        };
        controlsLayout.Children.Add(searchEntryBorder);
        Grid.SetColumn(searchEntryBorder, 1);

        mainLayout.Children.Add(controlsLayout);

        var listCollectionView = new CollectionView
        {
            ItemsSource = _listItems,
            ItemTemplate = new DataTemplateSelector { CardTemplate = new DataTemplate(() =>
            {
                var cardGrid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Auto } },
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    Padding = 10
                };
                
                var titleLabel = new Label { TextColor = Colors.White, FontSize = 18, FontAttributes = FontAttributes.Bold };
                titleLabel.SetBinding(Label.TextProperty, "Title");
                cardGrid.Children.Add(titleLabel);
                Grid.SetRow(titleLabel, 0);

                var itemCountLabel = new Label { TextColor = Colors.White, FontSize = 14 };
                itemCountLabel.SetBinding(Label.TextProperty, "ItemCount", stringFormat: "{0} Items");
                cardGrid.Children.Add(itemCountLabel);
                Grid.SetRow(itemCountLabel, 1);

                var deleteButton = new Button { Text = "X", TextColor = Colors.Red };
                deleteButton.Clicked += async (s, e) =>
                {
                    var item = (Liste)deleteButton.BindingContext;
                    await Services.ListService.DeleteList(item);
                    await LoadLists();
                };
                cardGrid.Children.Add(deleteButton);
                Grid.SetColumn(deleteButton, 1);
                Grid.SetRowSpan(deleteButton, 2);

                var border = new Border { Content = cardGrid, BackgroundColor = Color.FromArgb("#1A1A1A"), StrokeThickness = 1, Stroke = Color.FromArgb("#0055FF"), StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) } };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    var item = (Liste)border.BindingContext;
                    await Shell.Current.GoToAsync($"{nameof(ListDetailPage)}", new Dictionary<string, object>
                    {
                        { "List", item }
                    });
                };
                border.GestureRecognizers.Add(tapGestureRecognizer);

                return border;
            }),
            AddTemplate = new DataTemplate(() =>
            {
                var addButton = new Button { Text = "+", FontSize = 30, CornerRadius = 10, BackgroundColor = Color.FromArgb("#1A1A1A"), BorderColor = Color.FromArgb("#0055FF"), BorderWidth = 1 };
                addButton.Clicked += OnAddNewListClicked;
                return addButton;
            })}
        };

        mainLayout.Children.Add(listCollectionView);

        var scrollView = new ScrollView
        {
            Content = mainLayout
        };

        SetContent(scrollView);
    }
    

    
    private async void OnAddNewListClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NeueListePage));
    }

    public class DataTemplateSelector : Microsoft.Maui.Controls.DataTemplateSelector
    {
        public DataTemplate? AddTemplate { get; set; }
        public DataTemplate? CardTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is AddListItem)
            {
                return AddTemplate ?? new DataTemplate();
            }
            return CardTemplate ?? new DataTemplate();
        }
    }
}