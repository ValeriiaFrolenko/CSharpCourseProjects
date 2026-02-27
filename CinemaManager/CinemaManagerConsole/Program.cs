using CinemaManager.Composition;
using CinemaManager.DTOs;
using CinemaManager.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaManagerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            CompositionRoot.Register(services);
            var provider = services.BuildServiceProvider();

            var hallService = provider.GetRequiredService<IHallStorageService>();
            var sessionService = provider.GetRequiredService<ISessionStorageService>();

            DisplayTotals(hallService, sessionService);
            ViewHallsInteractive(hallService);
        }

        static void DisplayTotals(IHallStorageService hallService, ISessionStorageService sessionService)
        {
            Console.WriteLine($"Total halls: {hallService.GetHallsCount()}");
            Console.WriteLine($"Total sessions: {sessionService.GetSessionsCount()}");
            Console.WriteLine();
        }

        static void ViewHallsInteractive(IHallStorageService hallService)
        {
            var hallsList = hallService.GetHallsSummary();

            if (hallsList.Count == 0)
            {
                Console.WriteLine("No halls found.");
                return;
            }

            while (true)
            {
                DisplayHallsList(hallsList);

                var hallId = GetSelectedHallId(hallsList);
                if (hallId == null)
                    return;

                DisplayHallDetails(hallService, hallId.Value);
            }
        }

        static void DisplayHallsList(List<HallListItemDTO> hallsList)
        {
            for (int i = 0; i < hallsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {hallsList[i].Name}");
            }
        }

        static Guid? GetSelectedHallId(List<HallListItemDTO> hallsList)
        {
            Console.Write("Enter hall number to view details (or press Enter to exit): ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > hallsList.Count)
            {
                Console.WriteLine("Invalid hall number.");
                Console.WriteLine();
                return Guid.Empty;
            }

            return hallsList[choice - 1].Id;
        }

        static void DisplayHallDetails(IHallStorageService hallService, Guid hallId)
        {
            if (hallId == Guid.Empty)
                return;

            var hall = hallService.GetHallById(hallId);

            if (hall == null)
            {
                Console.WriteLine("Hall not found.");
                Console.WriteLine();
                return;
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
}