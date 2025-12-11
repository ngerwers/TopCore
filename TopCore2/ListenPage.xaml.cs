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

                var editIcon = new Image { Source = "edit_icon.png", HeightRequest = 20, WidthRequest = 20, HorizontalOptions = LayoutOptions.End };
                cardGrid.Children.Add(editIcon);
                Grid.SetRowSpan(editIcon, 2);

                return new Border { Content = cardGrid, BackgroundColor = Color.FromArgb("#1A1A1A"), StrokeThickness = 1, Stroke = Color.FromArgb("#0055FF"), StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) } };
            }),
            AddTemplate = new DataTemplate(() =>
            {
                var addButton = new Button { Text = "+", FontSize = 30, CornerRadius = 10, BackgroundColor = Color.FromArgb("#1A1A1A"), BorderColor = Color.FromArgb("#0055FF"), BorderWidth = 1 };
                addButton.Clicked += OnAddNewListClicked;
                return addButton;
            })}
        };

        mainLayout.Children.Add(listCollectionView);

        SetContent(mainLayout);
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