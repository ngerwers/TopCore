using TopCore2.Models;

namespace TopCore2.Selectors;

public class ListenDataTemplateSelector : DataTemplateSelector
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
