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
            ViewSessionsByHall = 3
        }

        private static SessionStorageService sessionStorageService = new();
        private static HallStorageService hallStorageService = new();

        static void Main(string[] args)
        {
            Console.WriteLine($"Total halls: {hallStorageService.GetHallsCount()}");
            Console.WriteLine($"Total sessions: {sessionStorageService.GetSessionsCount()}");
            Console.WriteLine();

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
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
        }

        static void HandleMenuOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.ViewAllHalls:
                    ViewAllHallsInteractive();
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
                default:
                    Console.WriteLine("Invalid option. Please try again.\n");
                    break;
            }
        }

        static void ViewAllHallsInteractive()
        {
            // Load minimal data for the list to improve performance
            var hallsList = hallStorageService.GetHallsNameList();

            if (hallsList.Count == 0)
            {
                Console.WriteLine("No halls found.");
                Console.WriteLine();
                return;
            }

            while (true)
            {
                for (int i = 0; i < hallsList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hallsList[i].Name}");
                }

                Console.Write("Enter hall number to view details (or press Enter to return): ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine();
                    return;
                }

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > hallsList.Count)
                {
                    Console.WriteLine("Invalid hall number.");
                    Console.WriteLine();
                    continue;
                }

                var selectedHallId = hallsList[choice - 1].Id;
                var hall = hallStorageService.GetHallById(selectedHallId);

                if (hall == null)
                {
                    Console.WriteLine("Hall not found.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine(hall);

                if (hall.Sessions.Count > 0)
                {
                    Console.WriteLine("Sessions:");
                    foreach (var session in hall.Sessions)
                    {
                        Console.WriteLine(session);
                    }
                }
                else
                {
                    Console.WriteLine("No sessions available.");
                }

                Console.WriteLine();
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