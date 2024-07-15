namespace LibraryManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
				For the purposes of this mock application we'll instantiate the LibraryManager class here each time the program is run
				This will create a new set of books each time the program is run and will not persist between runs
				In a real application we would want to persist the books to a database or file so they can be loaded each time the program is run
				However for the scope of this test we do not need the added books to persist between runs, only when running unit tests
			*/
            var libraryManager = new LibraryManager.LibraryManager();

            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            switch (args[0])
            {
                case "add":
                    if (args.Length != 5)
                    {
                        Console.WriteLine("usage: dotnet run add <title> <author> <releaseYear> <edition>");
                        return;
                    }

                    var book = new Models.BookModelBuilder()
                        .WithTitle(args[1])
                        .WithAuthor(args[2])
                        .WithReleaseYear(int.Parse(args[3]))
                        .WithEdition(int.Parse(args[4]))
                        .Build();

                    var addResult = libraryManager.AddBook(book);

                    Console.WriteLine(addResult.Message);
                    break;
                case "remove":
                    if (args.Length != 2)
                    {
                        Console.WriteLine("usage: dotnet run remove <id>");
                        return;
                    }

                    var removeResult = libraryManager.RemoveBook(int.Parse(args[1]));

                    Console.WriteLine(removeResult.Message);
                    break;
                case "list":
                    foreach (var result in libraryManager.GetBooks())
                    {
                        Console.WriteLine(result.ToString());
                    }
                    break;
                case "search":
                    if (args.Length != 3)
                    {
                        Console.WriteLine("usage: dotnet run search title <title>");
                        Console.WriteLine("usage: dotnet run search author <author>");
                        return;
                    }

                    var searchResult = args[1] switch
                    {
                        "title" => libraryManager.SearchByTitle(args[2]),
                        "author" => libraryManager.SearchByAuthor(args[2]),
                        _ => throw new ArgumentException("Invalid search type")
                    };

                    if (searchResult.Success)
                    {
                        foreach (var result in searchResult.Books)
                        {
                            Console.WriteLine(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine(searchResult.Message);
                    }
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage: dotnet run <command> [<args>]");
            Console.WriteLine("commands: add, remove, list, search");
            Console.WriteLine("add: dotnet run add <title> <author> <releaseYear> <edition>");
            Console.WriteLine("remove: dotnet run remove <id>");
            Console.WriteLine("search: dotnet run search title <title>");
            Console.WriteLine("search: dotnet run search author <author>");
        }
    }
}