using CinemaManager.Storage;

namespace CinemaManager
{
    public partial class App : Application
    {
        private readonly IStorageContext _storageContext;

        public App(IStorageContext storageContext)
        {
            _storageContext = storageContext;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await _storageContext.InitializeAsync();
        }
    }
}