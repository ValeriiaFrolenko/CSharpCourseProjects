using CinemaManager.Common.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaManager.ViewModels.Forms
{
    public partial class SessionFormViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyPropertyChangedFor(nameof(MovieNameError))]
        private string _movieName = string.Empty;

        [ObservableProperty]
        private FilmGenre _genre = FilmGenre.Action;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyPropertyChangedFor(nameof(YearError))]
        private string _yearText = string.Empty;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _time = TimeSpan.FromHours(12);

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyPropertyChangedFor(nameof(DurationError))]
        private string _durationText = string.Empty;

        public IReadOnlyList<FilmGenre> FilmGenres { get; } =
            Enum.GetValues<FilmGenre>().ToList();

        public string? MovieNameError =>
            string.IsNullOrWhiteSpace(MovieName) ? "Movie name is required." : null;

        public string? YearError =>
            !int.TryParse(YearText, out int y) || y < 1900 || y > 2100
                ? "Enter a year between 1900 and 2100." : null;

        public string? DurationError =>
            !int.TryParse(DurationText, out int d) || d <= 0
                ? "Must be a positive number of minutes." : null;

        private bool IsValid =>
            MovieNameError is null && YearError is null && DurationError is null;

        public int ParsedYear => int.TryParse(YearText, out int y) ? y : 0;
        public int ParsedDuration => int.TryParse(DurationText, out int d) ? d : 0;
        public DateTime ParsedStartTime => Date.Date + Time;

        public Func<Task>? SubmitHandler { get; set; }
        public Action? CancelHandler { get; set; }

        [RelayCommand(CanExecute = nameof(IsValid))]
        private Task SubmitAsync() => SubmitHandler?.Invoke() ?? Task.CompletedTask;

        [RelayCommand]
        private void Cancel() => CancelHandler?.Invoke();

        public void Populate(string movieName, FilmGenre genre, int year,
            DateTime startTime, int durationMinutes)
        {
            MovieName = movieName;
            Genre = genre;
            YearText = year.ToString();
            Date = startTime.Date;
            Time = startTime.TimeOfDay;
            DurationText = durationMinutes.ToString();
        }

        public void Reset()
        {
            MovieName = string.Empty;
            Genre = FilmGenre.Action;
            YearText = DateTime.Today.Year.ToString();
            Date = DateTime.Today;
            Time = TimeSpan.FromHours(12);
            DurationText = string.Empty;
        }
    }
}