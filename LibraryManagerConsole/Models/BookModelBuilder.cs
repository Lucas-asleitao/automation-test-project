namespace LibraryManagerConsole.Models
{
    public class BookModelBuilder
    {
        private string _title;
        private string _author;
        private int _releaseYear;
        private int _edition;

        public BookModelBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public BookModelBuilder WithAuthor(string author)
        {
            _author = author;
            return this;
        }

        public BookModelBuilder WithReleaseYear(int releaseYear)
        {
            _releaseYear = releaseYear;
            return this;
        }

        public BookModelBuilder WithEdition(int edition)
        {
            _edition = edition;
            return this;
        }

        public Book Build()
        {
            return new Book
            {
                Title = _title,
                Author = _author,
                ReleaseYear = _releaseYear,
                Edition = _edition
            };
        }
    }
}