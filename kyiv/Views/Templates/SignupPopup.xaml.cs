using kyiv.Services;
using CommunityToolkit.Maui.Views;
using Supabase.Gotrue.Mfa;

namespace kyiv.Views.Templates;

public partial class SignupPopup : Popup
{
    private readonly DataService _dataService;

    public SignupPopup(IDataService dataService)
    {
        _dataService = (DataService)dataService;

        InitializeComponent();
    }
    private void OnClose(object sender, TappedEventArgs args)
    {
        this.Close();
    }
    private async void OnRegister(object sender, EventArgs e)
    {
        bool isSignUp = await _dataService.SignUpAsync(loginEntry.Text, passwordEntry.Text, nameEntry.Text);
        if (isSignUp)
        {
            this.Close();
            // and send to login page login and password and wait when user click login btn
        }

    }

}
