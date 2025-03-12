
using kyiv.Models;
using kyiv.Services;
using kyiv.ViewModels;
using kyiv.Views.Templates;
using CommunityToolkit.Maui.Views;
using Supabase.Interfaces;

namespace kyiv.Views;

public partial class AccountView : ContentPage
{
    private readonly DataService _dataService;

    private AccountViewModel viewModel;
    public AccountView(IDataService dataService)
	{
        _dataService = (DataService)dataService;
        InitializeComponent();
        viewModel = new(dataService);
        BindingContext = viewModel;
       
	}
	private async void OnLogin(object sender, EventArgs args)
	{
        var popupResult = await Shell.Current.CurrentPage.ShowPopupAsync(new LoginPopup(_dataService));

        if (popupResult is bool result && result)
        {
            userGrid.IsVisible = true;
            loginBtn.IsVisible = false;
            viewModel.Initialize();
        }
        OnAppearing();
    }

        await Navigation.PushAsync(new NotesPage());
    }
>>>>>>>>> Temporary merge branch 2

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_dataService.SupabaseClient.Auth.CurrentSession != null)
        {
            userGrid.IsVisible = true;
            loginBtn.IsVisible = false;

        }
        else
        {
            userGrid.IsVisible = false;
            loginBtn.IsVisible = true;
        }

    }

    private async void OnNotesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }



    private async void OnUserSettings(object sender, TappedEventArgs args)
    {
        await Navigation.PushAsync(new UserSettingsView(_dataService, viewModel.BindUserDataModel));
    }
    private async void OnSettings(object sender, TappedEventArgs args)
    {
        //       await Navigation.PushAsync(new SettingsView());
    }

    private async void OnPrivacyPolicyTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrivacyPolicyPage());

    }

    private async void OnZCU(object sender, TappedEventArgs args)
    {
        try
        {
            Uri uri = new Uri("https://savelife.in.ua/");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
        }

    }

    private async void OnTelegram(object sender, TappedEventArgs args)
    {
        try
        {
            Uri uri = new Uri("https://t.me/avocado_officialy");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
        }
    }

    private async void OnDonate(object sender, TappedEventArgs args)
    {
        try
        {
            Uri uri = new Uri("https://send.monobank.ua/jar/2e9YVZztyP");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
        }
    }

}
//=======
//using kyiv.Services;
//using kyiv.ViewModels;
//using kyiv.Views.Templates;
//using CommunityToolkit.Maui.Views;
//using Supabase.Interfaces;

//namespace kyiv.Views;

//public partial class AccountView : ContentPage
//{
//    private readonly DataService _dataService;

//    private AccountViewModel viewModel;
//    public AccountView(IDataService dataService)
//	{
//        _dataService = (DataService)dataService;
//        InitializeComponent();
//        viewModel = new(dataService);
//        BindingContext = viewModel;

//	}
//	private async void OnLogin(object sender, EventArgs args)
//	{
//        var popupResult = await Shell.Current.CurrentPage.ShowPopupAsync(new LoginPopup(_dataService));

//        if (popupResult is bool result && result)
//        {
//            userGrid.IsVisible = true;
//            loginBtn.IsVisible = false;
//            viewModel.Initialize();
//        }

//    }

//    private async void OnNotesTapped(object sender, EventArgs e)
//    {
//        await Navigation.PushAsync(new NotesPage());
//    }

//    protected override async void OnAppearing()
//    {
//        base.OnAppearing();

//        if (_dataService.SupabaseClient.Auth.CurrentSession != null)
//        {
//            userGrid.IsVisible = true;
//            loginBtn.IsVisible = false;

//        }
//        else
//        {
//            userGrid.IsVisible = false;
//            loginBtn.IsVisible = true;
//        }

//    }


//    private async void OnUserSettings(object sender, TappedEventArgs args)
//    {
//        await Navigation.PushAsync(new UserSettingsView(_dataService, viewModel.BindUserDataModel));
//    }
//    private async void OnSettings(object sender, TappedEventArgs args)
//    {
// //       await Navigation.PushAsync(new SettingsView());
//    }

//    private async void OnPrivacyPolicyTapped(object sender, EventArgs e)
//    {
//        await Navigation.PushAsync(new PrivacyPolicyPage());

//    }

//    private async void OnZCU(object sender, TappedEventArgs args)
//    {
//        try
//        {
//            Uri uri = new Uri("https://savelife.in.ua/");
//            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
//        }
//        catch (Exception ex)
//        {
//        }

//    }

//    private async void OnTelegram(object sender, TappedEventArgs args)
//    {
//        try
//        {
//            Uri uri = new Uri("https://t.me/avocado_officialy");
//            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
//        }
//        catch (Exception ex)
//        {
//        }
//    }

//    private async void OnDonate(object sender, TappedEventArgs args)
//    {
//        try
//        {
//            Uri uri = new Uri("https://send.monobank.ua/jar/2e9YVZztyP");
//            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
//        }
//        catch (Exception ex)
//        {
//        }
//    }

//}

