using LibraryManagerConsole.LibraryManager;
using LibraryManagerConsole.Models;
using NUnit.Framework;

namespace LibraryManagerConsoleTests
{
    [TestFixture]
    public class LibraryManagerTests
    {
        private LibraryManager _libraryManager;

        [SetUp]
        public void Setup()
        {
            _libraryManager = new LibraryManager();
        }

        [Test]
        [Description("Books can be added to the library")]
        public void LibraryManagerTests_TestCase_001()
        {
            // Arrange
            var book = new BookModelBuilder()
                .WithTitle("The Lion, the Witch and the Wardrobe")
                .WithAuthor("C.S. Lewis")
                .WithReleaseYear(1950)
                .WithEdition(1)
                .Build();

            // Act
            var result = _libraryManager.AddBook(book);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo($"{book}: Added successfully."));
        }

        [Test]
        [Description("It is possible to add multiple books")]
        public void LibraryManagerTests_TestCase_002()
        {
            var newBook = new BookModelBuilder()
                .WithTitle("Moby Dick")
                .WithAuthor("Herman Melville")
                .WithReleaseYear(1851)
                .WithEdition(1)
                .Build();
            var firstAttemptResult = _libraryManager.AddBook(newBook);
            Assert.That(firstAttemptResult.Success, Is.True);

            var newBook2 = new BookModelBuilder()
                .WithTitle("The Catcher in the Rye")
                .WithAuthor("J. D. Salinger")
                .WithReleaseYear(1951)
                .WithEdition(1)
                .Build();

            var secondAttemptResult = _libraryManager.AddBook(newBook2);
            Assert.That(secondAttemptResult.Success, Is.True);

            List<Book> books = _libraryManager.GetBooks();
            Assert.That(books.Any(book => book.Title == newBook.Title));
            Assert.That(books.Any(book => book.Title == newBook2.Title));
        }

        [Test]
        [Description("Add a description here")]
        public void LibraryManagerTests_TestCase_003()
        {
            // Arrange
            const string bookTitle = "The Great Gatsby";

            BookModelBuilder modelBuilder = new BookModelBuilder()
                .WithTitle(bookTitle)
                .WithAuthor("F. Scott Fitzgerald")
                .WithReleaseYear(1925);

            Book firstEdition = modelBuilder
                .WithEdition(1)
                .Build();

            Book secondEdition = modelBuilder
                .WithEdition(2)
                .Build();

            // Act
            var addFirstEditionResult = _libraryManager.AddBook(firstEdition);
            var addSecondEditionResult = _libraryManager.AddBook(secondEdition);

            List<Book> books = _libraryManager.GetBooks();

            // Assert
            Assert.That(addFirstEditionResult.Success, Is.True);
            Assert.That(addSecondEditionResult.Success, Is.True);

            Assert.That(books.Count(book => book.Title == bookTitle), Is.EqualTo(2));
            Assert.That(books.Count(book => book.Title == bookTitle && book.Edition == 1), Is.EqualTo(1));
            Assert.That(books.Count(book => book.Title == bookTitle && book.Edition == 2), Is.EqualTo(1));
        }

        [Test]
        [Description("The age of the book is calculated correctly")]
        public void LibraryManagerTests_TestCase_004()
        {
            // Arrange
            var newBook = new BookModelBuilder()
                .WithTitle("Norwegian Wood")
                .WithAuthor("Haruki Murakami")
                .WithReleaseYear(1987)
                .WithEdition(1)
                .Build();

            // Act
            var result = _libraryManager.AddBook(newBook);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo($"{newBook} added successfully"));

            List<Book> books = _libraryManager.GetBooks();
            var addedBook = books.First(book => book.Title == newBook.Title);

            Assert.That(addedBook.BookAge, Is.EqualTo(35));
        }

        [Test]
        [Description("Books can be removed from the library")]
        public void LibraryManagerTests_TestCase_005()
        {
            // Arrange
            var newBook = new BookModelBuilder()
                .WithTitle("The Picture of Dorian Gray")
                .WithAuthor("Oscar Wilde")
                .WithReleaseYear(1890)
                .WithEdition(1)
                .Build();

            var addResult = _libraryManager.AddBook(newBook);

            // Act
            var removeResult = _libraryManager.RemoveBook(newBook.Id);

            // Assert
            Assert.That(addResult.Success, Is.True);
            Assert.That(addResult.Message, Is.EqualTo($"{newBook} added successfully"));

            Assert.That(removeResult.Success, Is.True);
            Assert.That(removeResult.Message, Is.EqualTo($"Book with id {newBook.Id} removed successfully"));
        }
    }
}