using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using TopCore2.Layouts;
using TopCore2.Models;
using TopCore2.Services;

namespace TopCore2;

[QueryProperty(nameof(ListToEdit), "ListToEdit")]
public partial class NeueListePage : BasePage
{
    private readonly ObservableCollection<ListItem> _listItems = new();
    private Entry? _titleEntry;
    private Slider? _prioritySlider;
    private DatePicker? _deadlinePicker;
    private Switch? _favoriteSwitch;
    private Switch? _deadlineSwitch;
    private Liste? _editingList;

    public Liste? ListToEdit
    {
        set
        {
            if (value == null) return;

            _editingList = value;

            if (_titleEntry != null)
                _titleEntry.Text = value.Title;

            if (_prioritySlider != null)
                _prioritySlider.Value = value.Importance;

            if (_deadlineSwitch != null)
            {
                _deadlineSwitch.IsToggled = value.Deadline.HasValue;
            }

            if (_deadlinePicker != null)
            {
                _deadlinePicker.Date = value.Deadline ?? DateTime.Now;
                _deadlinePicker.IsVisible = value.Deadline.HasValue;
            }

            if (_favoriteSwitch != null)
                _favoriteSwitch.IsToggled = value.IsFavorite;

            _listItems.Clear();
            foreach (var item in value.Items)
            {
                _listItems.Add(item);
            }
            SetTitle("Liste Bearbeiten");
        }
    }

    public NeueListePage()
    {
        InitializeComponent();
        SetTitle("Neue Liste");
        BuildContent();
    }

    private void BuildContent()
    {
        var mainLayout = new VerticalStackLayout { Spacing = 20, Padding = new Thickness(20) };

        mainLayout.Children.Add(new Label { Text = "Titel", TextColor = Colors.White, FontSize = 18 });
        _titleEntry = new Entry { Placeholder = "Titel der Liste" };
        var titleEntryBorder = new Border
        {
            Content = _titleEntry,
            Stroke = Color.FromArgb("#0055FF"),
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
        };
        mainLayout.Children.Add(titleEntryBorder);

        mainLayout.Children.Add(new Label { Text = "Listen Wichtigkeit 1 - 10", TextColor = Colors.White, FontSize = 18 });
        _prioritySlider = new Slider { Minimum = 1, Maximum = 10, Value = 5 };
        mainLayout.Children.Add(_prioritySlider);

        var deadlineLayout = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center };
        deadlineLayout.Children.Add(new Label { Text = "Fälligkeitsdatum aktivieren", TextColor = Colors.White, FontSize = 18, VerticalOptions = LayoutOptions.Center });
        _deadlineSwitch = new Switch { IsToggled = false };
        deadlineLayout.Children.Add(_deadlineSwitch);
        mainLayout.Children.Add(deadlineLayout);

        _deadlinePicker = new DatePicker { Date = DateTime.Now, IsVisible = false };
        _deadlinePicker.SetBinding(IsVisibleProperty, new Binding("IsToggled", source: _deadlineSwitch));
        mainLayout.Children.Add(_deadlinePicker);

        var favoriteLayout = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center };
        favoriteLayout.Children.Add(new Label { Text = "Als Favorit markieren", TextColor = Colors.White, FontSize = 18, VerticalOptions = LayoutOptions.Center });
        _favoriteSwitch = new Switch { IsToggled = false };
        favoriteLayout.Children.Add(_favoriteSwitch);
        mainLayout.Children.Add(favoriteLayout);


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
                    if (s is Button { BindingContext: ListItem item })
                    {
                        _listItems.Remove(item);
                    }
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

        var scrollView = new ScrollView
        {
            Content = mainLayout
        };

        SetContent(scrollView);
    }

    private void OnAddItemClicked(object? sender, EventArgs e)
    {
        _listItems.Add(new ListItem { Text = "" });
    }

    private async void OnSaveListClicked(object? sender, EventArgs e)
    {
        if (_titleEntry == null || string.IsNullOrWhiteSpace(_titleEntry.Text))
        {
            await DisplayAlert("Fehler", "Der Titel darf nicht leer sein.", "OK");
            return;
        }

        if (_prioritySlider == null || _deadlinePicker == null || _favoriteSwitch == null || _deadlineSwitch == null)
        {
            return;
        }

        Liste listToSave;
        if (_editingList != null)
        {
            _editingList.Title = _titleEntry.Text;
            _editingList.Importance = (int)_prioritySlider.Value;
            _editingList.Items = new List<ListItem>(_listItems);
            _editingList.Deadline = _deadlineSwitch.IsToggled ? _deadlinePicker.Date : (DateTime?)null;
            _editingList.IsFavorite = _favoriteSwitch.IsToggled;
            listToSave = _editingList;
        }
        else
        {
            listToSave = new Liste
            {
                Title = _titleEntry.Text,
                Importance = (int)_prioritySlider.Value,
                Items = new List<ListItem>(_listItems),
                Deadline = _deadlineSwitch.IsToggled ? _deadlinePicker.Date : (DateTime?)null,
                IsFavorite = _favoriteSwitch.IsToggled
            };
        }

        await ListService.SaveList(listToSave);
        await Shell.Current.GoToAsync("//ListenPage");
    }
}