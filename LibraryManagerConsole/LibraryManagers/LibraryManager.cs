using LibraryManagerConsole.Models;
using System.Net;

namespace LibraryManagerConsole.LibraryManagers
{
    public class LibraryManager
    {
        private readonly List<Book> _books = [];

        public LibraryManager() { }

        public BookManagementResult AddBook(Book book)
        {
            if (_books.Any(libraryBook => libraryBook.Title == book.Title &&
                    libraryBook.Author == book.Author &&
                    libraryBook.ReleaseYear == book.ReleaseYear &&
                    libraryBook.Edition == book.Edition))
            {
                return new BookManagementResult($"{book}: Already exists.", false);
            }

            Book bookToAdd = new()
            {
                Id = _books.Count == 0 ? 1 : _books.Max(libraryBook => libraryBook.Id) + 1,
                Title = book.Title,
                Author = book.Author,
                ReleaseYear = book.ReleaseYear,
                Edition = book.Edition
            };

            _books.Add(bookToAdd);

            return new BookManagementResult($"{bookToAdd}: Added successfully.", true);
        }

        public BookManagementResult RemoveBook(int bookId)
        {
            int booksRemoved = _books.RemoveAll(book => book.Id == bookId);

            if (booksRemoved == 0)
            {
                return new BookManagementResult($"Book with id {bookId} not found", false);
            }

            return new BookManagementResult($"Book with id {bookId} removed successfully.", true);
        }

        public BookManagementResult RemoveAllBooks()
        {
            _books.Clear();

            int booksInLibrary = _books.Count;

            if (booksInLibrary > 0)
            {
                return new BookManagementResult($"Not all books were removed successfully", false);
            }

            return new BookManagementResult($"All books have been removed successfully.", true);
        }

        public BookSearchResult SearchByTitle(string title)
        {
            var books = _books.Where(book => book.Title.Contains(title)).ToList();

            if (books.Count == 0)
            {
                return new BookSearchResult(books, $"No books found with title: {title}", false);
            }

            return new BookSearchResult(books, "Books found", true);
        }

        public BookSearchResult SearchByAuthor(string author)
        {
            var books = _books.Where(book => book.Author == author).ToList();

            if (books.Count == 0)
            {
                return new BookSearchResult(books, $"No books found with author: {author}", false);
            }

            return new BookSearchResult(books, "Books found", true);
        }

        public BookSearchResult SearchAllEditionsByBook(Book myBook)
        {
            var books = _books
                .Where(book => book.Title == myBook.Title
                && book.Author == myBook.Author
                && book.ReleaseYear == myBook.ReleaseYear)
                .ToList();

            if (books.Count == 0)
            {
                return new BookSearchResult(books, $"No editions found for book: {myBook.ToString()}", false);
            }

            return new BookSearchResult(books, "Books found", true);
        }

        public List<Book> GetBooks()
        {
            return _books;
        }
    }
}