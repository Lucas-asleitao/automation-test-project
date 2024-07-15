namespace LibraryManagerConsole.Models
{
    public class BookManagementResult(string message, bool success)
    {
        public string Message { get; set; } = message;
        public bool Success { get; set; } = success;
    }
}