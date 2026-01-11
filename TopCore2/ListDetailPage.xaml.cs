using TopCore2.Layouts;
using TopCore2.Models;

namespace TopCore2;

[QueryProperty(nameof(List), "List")]
public partial class ListDetailPage : BasePage
{
    private Liste _list = new();
    public Liste List
    {
        get => _list;
        set
        {
            _list = value;
            OnPropertyChanged();
            if (value != null)
            {
                BuildContent();
            }
        }
    }

    public ListDetailPage()
    {
        InitializeComponent();
    }

    private void BuildContent()
    {
        if (List.Title != null)
        {
            SetTitle(List.Title);
        }

        var mainLayout = new VerticalStackLayout { Spacing = 20 };

        var listCollectionView = new CollectionView
        {
            ItemsSource = List.Items,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { TextColor = Colors.White, FontSize = 16 };
                label.SetBinding(Label.TextProperty, "Text");
                return new Border
                {
                    Content = label,
                    BackgroundColor = Color.FromArgb("#1A1A1A"),
                    StrokeThickness = 1,
                    Stroke = Color.FromArgb("#0055FF"),
                    StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(10) },
                    Padding = new Thickness(10)
                };
            })
        };
        mainLayout.Children.Add(listCollectionView);
        
        var scrollView = new ScrollView
        {
            Content = mainLayout
        };

        SetContent(scrollView);
    }
}
