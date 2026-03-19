using CinemaManager.Storage;
using CinemaManager.UIModels;

namespace CinemaManager.Pages;

[QueryProperty(nameof(HallId), "id")]
public partial class HallDetailsPage : ContentPage
{
    private readonly IHallStorageService _hallStorageService;
    private Guid _hallId;

    public HallUIModel? Hall { get; set; }

    public string HallId
    {
        set
        {
            if (Guid.TryParse(value, out _hallId))
            {
                LoadHallDetails();
            }
        }
    }

    public HallDetailsPage(IHallStorageService hallStorageService)
    {
        InitializeComponent();
        _hallStorageService = hallStorageService;
        BindingContext = this;
    }

    private void LoadHallDetails()
    {
        Hall = _hallStorageService.GetHallById(_hallId);
        OnPropertyChanged(nameof(Hall));
    }

    private async void OnSessionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SessionUIModel session)
        {
            await Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}?id={session.Id}");

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}