using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LibraryData
{
    public class BackupHelper
    {
        public void SaveBooksToJson(List<Book> books, string filePath)
        {
            string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public List<Book> LoadBooksFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(json);
            return books;
        }
    }
}
