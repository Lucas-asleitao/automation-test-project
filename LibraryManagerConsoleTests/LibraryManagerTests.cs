using LibraryManagerConsole.LibraryManagers;
using LibraryManagerConsole.Models;
using NUnit.Framework;
using System.Net;

namespace LibraryManagerConsoleTests
{
    [TestFixture]
    public class LibraryManagerTests
    {
        private LibraryManager _libraryManagerWithSamples;
        private LibraryManager _emptyLibraryManager;

        [SetUp]
        public void Setup()
        {
            _libraryManagerWithSamples = new LibraryManager();
            AddSampleBooks(_libraryManagerWithSamples);
            
            _emptyLibraryManager = new LibraryManager();
        }

        [TearDown]
        public void TearDown()
        {
            _libraryManagerWithSamples.RemoveAllBooks();
        }

        [Test]
        [Description("A Book can be added correctly to the library")]
        public void LibraryManagerTests_TestCase_001()
        {
            // Arrange
            List<Book> BooksBeforeNewBookIsAdded = _libraryManagerWithSamples.GetBooks();
            int bookCountBeforeAdding = BooksBeforeNewBookIsAdded.Count;
            int lastBookId = BooksBeforeNewBookIsAdded.Max(libraryBook => libraryBook.Id);

            Book newBook = new BookModelBuilder()
                .WithTitle("The Lion, the Witch and the Wardrobe")
                .WithAuthor("C.S. Lewis")
                .WithReleaseYear(1950)
                .WithEdition(1)
                .Build();

            // Act
            BookManagementResult addBookResult = _libraryManagerWithSamples.AddBook(newBook);

            // Assert
            Assert.That(addBookResult.Success, Is.True);

            List<Book> BooksAfterBookIsAdded = _libraryManagerWithSamples.GetBooks();
            Book addedBook = BooksAfterBookIsAdded.Single(book => book.ToString() == newBook.ToString());

            Assert.That(BooksAfterBookIsAdded.Count, Is.EqualTo(bookCountBeforeAdding + 1));

            Assert.That(addedBook.Id, Is.EqualTo(lastBookId + 1));
            Assert.That(addedBook.Title, Is.EqualTo(newBook.Title));
            Assert.That(addedBook.Author, Is.EqualTo(newBook.Author));
            Assert.That(addedBook.ReleaseYear, Is.EqualTo(newBook.ReleaseYear));
            Assert.That(addedBook.Edition, Is.EqualTo(newBook.Edition));
            Assert.That(addedBook.BookAge, Is.EqualTo(newBook.BookAge));

            string expectedMessage = $"{addedBook}: Added successfully.";

            Assert.That(addBookResult.Message.Contains(expectedMessage), Is.True, $"Expected message not found: '{expectedMessage}'");
        }

        [Test]
        [Description("Multiple Books can be added correctly to the library")]
        public void LibraryManagerTests_TestCase_002()
        {
            // Arrange
            List<Book> BooksBeforeNewBookIsAdded = _libraryManagerWithSamples.GetBooks();
            int bookCountBeforeAdding = BooksBeforeNewBookIsAdded.Count;
            int lastBookId = BooksBeforeNewBookIsAdded.Max(libraryBook => libraryBook.Id);

            Book newBookOne = new BookModelBuilder()
                .WithTitle("Moby Dick")
                .WithAuthor("Herman Melville")
                .WithReleaseYear(1851)
                .WithEdition(1)
                .Build();
            var firstAttemptResult = _libraryManager.AddBook(newBook);
            Assert.That(firstAttemptResult.Success, Is.True);

            Book newBookTwo = new BookModelBuilder()
                .WithTitle("The Catcher in the Rye")
                .WithAuthor("J. D. Salinger")
                .WithReleaseYear(1951)
                .WithEdition(1)
                .Build();

            // Act
            BookManagementResult addFirstBookResult = _libraryManagerWithSamples.AddBook(newBookOne);
            BookManagementResult addSecondBookResult = _libraryManagerWithSamples.AddBook(newBookTwo);

            // Assert
            Assert.That(addFirstBookResult.Success, Is.True);
            Assert.That(addSecondBookResult.Success, Is.True);

            List<Book> BooksAfterBookIsAdded = _libraryManagerWithSamples.GetBooks();
            Assert.That(BooksAfterBookIsAdded.Count, Is.EqualTo(bookCountBeforeAdding + 2));

            Book addedBookOne = BooksAfterBookIsAdded.Single(book => book.ToString() == newBookOne.ToString());

            Assert.That(addedBookOne.Id, Is.EqualTo(lastBookId + 1));
            Assert.That(addedBookOne.Title, Is.EqualTo(newBookOne.Title));
            Assert.That(addedBookOne.Author, Is.EqualTo(newBookOne.Author));
            Assert.That(addedBookOne.ReleaseYear, Is.EqualTo(newBookOne.ReleaseYear));
            Assert.That(addedBookOne.Edition, Is.EqualTo(newBookOne.Edition));
            Assert.That(addedBookOne.BookAge, Is.EqualTo(newBookOne.BookAge));

            Book addedBookTwo = BooksAfterBookIsAdded.Single(book => book.ToString() == newBookTwo.ToString());

            Assert.That(addedBookTwo.Id, Is.EqualTo(lastBookId + 2));
            Assert.That(addedBookTwo.Title, Is.EqualTo(newBookTwo.Title));
            Assert.That(addedBookTwo.Author, Is.EqualTo(newBookTwo.Author));
            Assert.That(addedBookTwo.ReleaseYear, Is.EqualTo(newBookTwo.ReleaseYear));
            Assert.That(addedBookTwo.Edition, Is.EqualTo(newBookTwo.Edition));
            Assert.That(addedBookTwo.BookAge, Is.EqualTo(newBookTwo.BookAge));

            string expectedMessageForBookOne = $"{addedBookOne}: Added successfully.";
            string expectedMessageForBookTwo = $"{addedBookTwo}: Added successfully.";

            Assert.That(addFirstBookResult.Message.Contains(expectedMessageForBookOne), Is.True, $"Expected message not found: '{expectedMessageForBookOne}'");
            Assert.That(addSecondBookResult.Message.Contains(expectedMessageForBookTwo), Is.True, $"Expected message not found: '{expectedMessageForBookTwo}'");
        }

        [Test]
        [Description("Multiple Books with different editions can be added correctly to the library")]
        public void LibraryManagerTests_TestCase_003()
        {
            // Arrange
            BookModelBuilder bookModelBuilder = new BookModelBuilder()
                .WithTitle("The Great Gatsby")
            BookModelBuilder modelBuilder = new BookModelBuilder()
                .WithTitle(bookTitle)
                .WithAuthor("F. Scott Fitzgerald")
                .WithReleaseYear(1925);

            Book firstEditionBook = bookModelBuilder
                .WithEdition(1)
                .Build();

            Book secondEditionBook = bookModelBuilder
                .WithEdition(2)
                .Build();

            // Act
            BookManagementResult addFirstEditionResult = _libraryManagerWithSamples.AddBook(firstEditionBook);
            BookManagementResult addSecondEditionResult = _libraryManagerWithSamples.AddBook(secondEditionBook);

            List<Book> books = _libraryManager.GetBooks();

            // Assert
            Assert.That(addFirstEditionResult.Success, Is.True);
            Assert.That(addSecondEditionResult.Success, Is.True);

            List<Book> books = _libraryManagerWithSamples.GetBooks();

            Book addedFirstEditionBook = books.Single(book => book.ToString() == firstEditionBook.ToString());
            Book addedSecondEditionBook = books.Single(book => book.ToString() == secondEditionBook.ToString());

            BookSearchResult allBookEditionsResult = _libraryManagerWithSamples.SearchAllEditionsByBook(addedFirstEditionBook);
            Assert.That(allBookEditionsResult.Books.Count, Is.EqualTo(2));

            Assert.That(books.Count(book => book.ToString() == addedFirstEditionBook.ToString()), Is.EqualTo(1));
            Assert.That(books.Count(book => book.ToString() == addedSecondEditionBook.ToString()), Is.EqualTo(1));

            string expectedMessageForFirstEditionBook = $"{addedFirstEditionBook}: Added successfully.";
            string expectedMessageForSecondEditionBook = $"{addedSecondEditionBook}: Added successfully.";

            Assert.That(addFirstEditionResult.Message.Contains(expectedMessageForFirstEditionBook),
                Is.True, $"Expected message not found: '{expectedMessageForFirstEditionBook}'");
            Assert.That(addSecondEditionResult.Message.Contains(expectedMessageForSecondEditionBook),
               Is.True, $"Expected message not found: '{expectedMessageForSecondEditionBook}'");
        }

        [Test]
        [Description("The age of the book is calculated correctly")]
        public void LibraryManagerTests_TestCase_004()
        {
            // Arrange
            const int releaseYear = 1987;

            Book newBook = new BookModelBuilder()
                .WithTitle("Norwegian Wood")
                .WithAuthor("Haruki Murakami")
                .WithReleaseYear(1987)
                .WithEdition(1)
                .Build();

            // Act
            BookManagementResult result = _libraryManagerWithSamples.AddBook(newBook);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo($"{newBook}: Added successfully."));

            List<Book> books = _libraryManagerWithSamples.GetBooks();
            Book addedBook = books.First(book => book.Title == newBook.Title);

            Assert.That(addedBook.BookAge, Is.EqualTo(DateTime.Now.Year - releaseYear));
        }

        [Test]
        [Description("A Book can be removed from the library")]
        public void LibraryManagerTests_TestCase_005()
        {
            // Arrange
            Book newBook = new BookModelBuilder()
                .WithTitle("The Picture of Dorian Gray")
                .WithAuthor("Oscar Wilde")
                .WithReleaseYear(1890)
                .WithEdition(1)
                .Build();

            BookManagementResult addResult = _libraryManagerWithSamples.AddBook(newBook);

            List<Book> books = _libraryManagerWithSamples.GetBooks();
            Book addedBook = books.First(book => book.Title == newBook.Title);

            // Act
            BookManagementResult removeResult = _libraryManagerWithSamples.RemoveBook(addedBook.Id);

            // Assert
            Assert.That(addResult.Success, Is.True);
            Assert.That(addResult.Message, Is.EqualTo($"{newBook}: Added successfully."));

            Assert.That(removeResult.Success, Is.True);
            Assert.That(removeResult.Message, Is.EqualTo($"Book with id {newBook.Id} removed successfully"));
        }
    }
}