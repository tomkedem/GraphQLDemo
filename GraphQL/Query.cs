using System.Text.Json;
using System.Text.Json.Serialization;

public class Query
{
    //public List<Book> Books => ReadBooks();
    public List<Magazine> Magazines => ReadMagazines();
    public List<IReadingMaterials> ReadingMaterials => GetReadingMaterials();
    public List<IThings> Things => GetThings();

    public List<Book> Books(string nameContains = "")
    {
        string fileName = "Database/books.json";
        string jsonString = File.ReadAllText(fileName);
        var books =  JsonSerializer.Deserialize<List<Book>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new JsonStringEnumConverter() } })!;
        return books.Where(b => b.Name.IndexOf(nameContains) >=0).ToList();
    }

    private List<Magazine> ReadMagazines()
    {
        string fileName = "Database/magazines.json";
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<List<Magazine>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new JsonStringEnumConverter() } })!;
    }

    private List<IReadingMaterials> GetReadingMaterials()
    {
        var materials = Books().Cast<IReadingMaterials>().ToList();
        materials.AddRange(ReadMagazines().Cast<IReadingMaterials>().ToList());
        return materials;
    }

    private List<IThings> GetThings()
    {
        var things = Books().Cast<IThings>().ToList();
        things.AddRange(ReadMagazines().Cast<IThings>().ToList());
        return things;
    }
}