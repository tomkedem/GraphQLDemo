using System.Text.Json;
using System.Text.Json.Serialization;

public class Query
{
    public List<Book> Books => ReadBooks();   
    public List<Magazine> Magazines => ReadMagazines();   
    public List<IReadingMaterials> ReadingMaterials => GetReadingMaterials();   

    private List<Book> ReadBooks()  {
        string fileName = "Database/books.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<List<Book>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters={new JsonStringEnumConverter()} })!;
    }

    private List<Magazine> ReadMagazines()  {
        string fileName = "Database/magazines.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<List<Magazine>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters={new JsonStringEnumConverter()} })!;
    }

     private List<IReadingMaterials> GetReadingMaterials()  {
        var materials = ReadBooks().Cast<IReadingMaterials>().ToList();
        materials.AddRange(ReadMagazines().Cast<IReadingMaterials>().ToList());
        return materials;
     }
}