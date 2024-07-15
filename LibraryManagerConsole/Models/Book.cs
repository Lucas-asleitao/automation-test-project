namespace LibraryManagerConsole.Models
{
	public class Book
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public int ReleaseYear { get; set; }

		public int Edition { get; set; }

		public int BookAge
		{
			get
			{
				return DateTime.Now.Year - ReleaseYear;
			}
		}

		public override string ToString()
		{
			return $"Title: {Title}, Author: {Author}, ReleaseYear: {ReleaseYear}, Edition: {Edition}, BookAge: {BookAge}";
		}
	}
}