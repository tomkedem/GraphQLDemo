using System.Text.Json;
using System.Text.Json.Serialization;

public class Mutation  {
    public Book AddBook(BookInput input)  {

        // Read all current books
        string fileName = "Database/books.json";
        string jsonString = File.ReadAllText(fileName);
        var books= JsonSerializer.Deserialize<List<Book>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new JsonStringEnumConverter() } })!;
        
        // Create a new book based on the input
        var rand = new Random();

        var book = new Book();
        book.BookId = rand.Next(1000, 10000);
        book.Name = input.Name;
        book.Genre = input.Genre;
        book.Pages = input.Pages;
        book.Price = input.Price;
        book.PublishDate = input.PublishDate;

        // Add the new book to the books list and save to the file
        books.Add(book);
        var json = JsonSerializer.Serialize(books);
        File.WriteAllText(fileName,json);

        // Return the newly created book
        return book;
    }
}