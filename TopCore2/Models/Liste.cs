using System.Collections.Generic;

namespace TopCore2.Models
{
    public class Liste
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public int Importance { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
        public int ItemCount => Items.Count;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? Deadline { get; set; }
        public bool IsFavorite { get; set; }
    }
}