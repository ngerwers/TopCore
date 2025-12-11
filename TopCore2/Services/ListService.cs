using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TopCore2.Models;

namespace TopCore2.Services
{
    public static class ListService
    {
        private static readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "lists.json");

        public static async Task<List<Liste>> GetAllLists()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Liste>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Liste>>(json) ?? new List<Liste>();
        }

        public static async Task SaveList(Liste newList)
        {
            var lists = await GetAllLists();
            lists.Add(newList);
            var json = JsonSerializer.Serialize(lists);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public static async Task DeleteList(Liste listToRemove)
        {
            var lists = await GetAllLists();
            var list = lists.Find(l => l.Title == listToRemove.Title);
            if (list != null)
            {
                lists.Remove(list);
                var json = JsonSerializer.Serialize(lists);
                await File.WriteAllTextAsync(_filePath, json);
            }
        }
    }
}
