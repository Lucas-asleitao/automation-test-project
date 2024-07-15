namespace LibraryManagerConsole.Models
{
    public class BookSearchResult(List<Book> books, string message, bool success)
    {
        public List<Book> Books { get; set; } = books;
        public string Message { get; set; } = message;
        public bool Success { get; set; } = success;
    }
}