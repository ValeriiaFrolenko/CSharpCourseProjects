using CinemaManager.Common.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaManager.ViewModels.Forms
{
    public partial class HallFormViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyPropertyChangedFor(nameof(NameError))]
        private string _name = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyPropertyChangedFor(nameof(SeatsError))]
        private string _seatsText = string.Empty;

        [ObservableProperty]
        private CinemaHallType _hallType = CinemaHallType.Standard2D;

        public IReadOnlyList<CinemaHallType> HallTypes { get; } =
            Enum.GetValues<CinemaHallType>().ToList();

        public string? NameError =>
            string.IsNullOrWhiteSpace(Name) ? "Hall name is required." : null;

        public string? SeatsError =>
            !int.TryParse(SeatsText, out int s) || s <= 0
                ? "Must be a positive integer." : null;

        private bool IsValid => NameError is null && SeatsError is null;

        public int ParsedSeats => int.TryParse(SeatsText, out int s) ? s : 0;

        public Func<Task>? SubmitHandler { get; set; }
        public Action? CancelHandler { get; set; }

        [RelayCommand(CanExecute = nameof(IsValid))]
        private Task SubmitAsync() => SubmitHandler?.Invoke() ?? Task.CompletedTask;

        [RelayCommand]
        private void Cancel() => CancelHandler?.Invoke();

        public void Populate(string name, int seats, CinemaHallType type)
        {
            Name = name;
            SeatsText = seats.ToString();
            HallType = type;
        }

        public void Reset()
        {
            Name = string.Empty;
            SeatsText = string.Empty;
            HallType = CinemaHallType.Standard2D;
        }
    }
}