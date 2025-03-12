using kyiv.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.Views.Templates;

public partial class LoginPopup : Popup
{
    private readonly DataService _dataService;

    public LoginPopup(IDataService dataService)
    {
        _dataService = (DataService)dataService;
		InitializeComponent();

        CloseCommand = new Command(() => Close(false));
        BindingContext = this;
	}



    private async void OnSignUp(object sender, TappedEventArgs args)
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(new SignupPopup(_dataService));
    }


    public async void OnLogin(object sender, EventArgs args)
    {
        try
        {
            var isLogIn = await _dataService.LoginAsync(loginEntry.Text, paswEntry.Text);
            this.Close(true);

        }
        catch (Exception ex)
        {
            Close(false);
        }

    }

    public Command CloseCommand { get; }

    private async void OnSignInWithGoogleAsync(object sender, TappedEventArgs args)
    {

    }

    //public async Task InitializeAsync()
    //{
    //    await _client.InitializeAsync();
    //}

    //public async Task<string> SignInWithGoogleAsync()
    //{
    //    var url = await _client.Auth.SignIn(Supabase.Gotrue.Constants.Provider.Google, new SupabaseOptions
    //    {

    //        // RedirectTo = "com.example.app:/auth" // Match your platform-specific redirect URI
    //    });

    //    // Open the browser for the user to authenticate
    //    await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);

    //    return "Check the redirect URI for login success.";
    //}

    //public async Task<User> GetUserAsync()
    //{
    //    return _client.Auth.CurrentUser;
    //}

}
