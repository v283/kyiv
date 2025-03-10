using kyiv.Services;

namespace kyiv;

public partial class App : Application
{

    private readonly DataService _dataService;

    public App(IDataService dataService)
    {
        _dataService = (DataService)dataService;
        InitializeComponent();
        MainPage = new AppShell();

        InitializeAccountAsync();

    }

    private async void InitializeAccountAsync()
    {
        bool isRestored = await _dataService.RestoreSessionAsync();

        if (_dataService.SupabaseClient.Auth.CurrentUser != null)
        {
            // load progress;
        }

    }
}

