using CinemaManager.Storage;
using CinemaManager.UIModels;

namespace CinemaManagerConsole
{
    internal class Program
    {
        enum MenuOption
        {
            Exit = 0,
            ViewAllHalls = 1,
            ViewHallByName = 2,
            ViewSessionsByHall = 3,
            ViewSessionsByMovieName = 4,
            ViewAllSessions = 5,
        }

        private static SessionStorageService sessionStorageService = new();
        private static HallStorageService hallStorageService = new();

        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
                var input = Console.ReadLine();

                if (!Enum.TryParse(input, out MenuOption option))
                {
                    Console.WriteLine("Invalid option. Please try again.\n");
                    continue;
                }

                if (option == MenuOption.Exit)
                    return;

                HandleMenuOption(option);
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Cinema Manager Console");
            Console.WriteLine("1. View All Halls");
            Console.WriteLine("2. View Hall By Name");
            Console.WriteLine("3. View Sessions By Hall");
            Console.WriteLine("4. View Sessions By Movie Name");
            Console.WriteLine("5. View All Sessions");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
        }

        static void HandleMenuOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.ViewAllHalls:
                    DisplayList(hallStorageService.GetAllHalls(), "No halls found.");
                    break;
                case MenuOption.ViewHallByName:
                    Console.Write("Enter hall name: ");
                    DisplaySingleItem(
                        hallStorageService.GetHallByName(Console.ReadLine()!),
                        "Hall not found."
                    );
                    break;
                case MenuOption.ViewSessionsByHall:
                    Console.Write("Enter hall name: ");
                    DisplayList(
                        sessionStorageService.GetSessionsByHallName(Console.ReadLine()!),
                        "No sessions found."
                    );
                    break;
                case MenuOption.ViewSessionsByMovieName:
                    Console.Write("Enter movie name: ");
                    DisplayList(
                        sessionStorageService.GetSessionsByMovieName(Console.ReadLine()!),
                        "No sessions found."
                    );
                    break;
                case MenuOption.ViewAllSessions:
                    DisplayList(sessionStorageService.GetSessions(), "No sessions found.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.\n");
                    break;
            }
        }

        static void DisplayList<T>(List<T> items, string emptyMessage)
        {
            if (items.Count > 0)
                items.ForEach(item => Console.WriteLine(item));
            else
                Console.WriteLine(emptyMessage);
            Console.WriteLine();
        }

        static void DisplaySingleItem<T>(T? item, string notFoundMessage) where T : class
        {
            if (item != null)
                Console.WriteLine(item);
            else
                Console.WriteLine(notFoundMessage);
            Console.WriteLine();
        }
    }
}