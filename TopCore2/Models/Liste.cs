using System.Collections.Generic;

namespace TopCore2.Models
{
    public class Liste
    {
        public string? Title { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
        public int ItemCount => Items.Count;
    }
}