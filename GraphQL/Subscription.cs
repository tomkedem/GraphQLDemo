public class Subscription{
    [Subscribe]
    public Book BookAdded([EventMessage]Book newBook) => newBook;
}