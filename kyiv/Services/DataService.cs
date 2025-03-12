
using kyiv.Models;
using Supabase;
using Supabase.Gotrue;
using CommunityToolkit.Maui.Views;
using kyiv.Views.Templates;
using static Supabase.Gotrue.Constants;

namespace kyiv.Services
{
    public class DataService : IDataService
    {
        // log
        // pasw 1234567
        private readonly Supabase.Client _supabaseClient;


        public Supabase.Client SupabaseClient { get => _supabaseClient; }

        public DataService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
            
        }


        public async Task<bool> LoginAsync(string email, string password)
        {
            var session = await _supabaseClient.Auth.SignIn(email, password);

            if (session != null)
            {
                await SecureStorage.Default.SetAsync("authToken", session.AccessToken);
                await SecureStorage.Default.SetAsync("refreshToken", session.RefreshToken);
                return true;
            }

            return false;
        }

        public async Task<bool> RestoreSessionAsync()
        {
            var token = await SecureStorage.Default.GetAsync("authToken");
            var refreshToken = await SecureStorage.Default.GetAsync("refreshToken");

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken))
            {
                var session = await _supabaseClient.Auth.SetSession(token, refreshToken);

                return session != null;

            }

            return false;
        }


        public async Task<bool> SignUpAsync(string email, string password, string name)
        {

            try
            {
                var response = await _supabaseClient.Auth.SignUp(email, password);
                var emailCkeck = await Shell.Current.CurrentPage.ShowPopupAsync(new EmailCkeckPopup());

                if ((response != null) && (emailCkeck is bool result && result))
                {
                    if (Guid.TryParse(response.User.Id, out var userId))
                    {
                        var newUser = new UserDataModel
                        {
                            UserId = userId,
                            Email = email,
                            Phone = "",
                            Name = name,
                            Birth = new(),
                            Image = "svg_user.svg"
                        };

                        await _supabaseClient.From<UserDataModel>().Insert(newUser);
                    }

                }
                return true;

            }
            catch (Exception ex)
            {

            }

            // треба буде іще удаляти токен авторизації якщо видалено акаунт користувача і обробляи ошибку якщо він є а акаунта в супі немає

            return false;
        }


        public async void LoginWithGoogle()
        {
            //var supabase = Util.GetSupabase();

            SignInOptions o = new()
            {
                FlowType = OAuthFlowType.PKCE,
            };

            var signInUrl = await _supabaseClient.Auth.SignIn(Provider.Google, o);

            if (signInUrl == null) throw new Exception();

            WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
        signInUrl.Uri,
        new Uri("kyiv://callback"));
            // manage the auth result
            Supabase.Gotrue.Session? session = _supabaseClient.Auth.CurrentSession;
            String? oAuthToken = session?.AccessToken;
            await Shell.Current.GoToAsync("where");
        }

        public async Task SignOutAsync()
        {
            await _supabaseClient.Auth.SignOut();
            SecureStorage.RemoveAll();
        }

        public async Task<UserDataModel> GetUserData()
        {
            UserDataModel rezult = new();

            if (Guid.TryParse(SupabaseClient.Auth.CurrentUser.Id, out var userId))
            {
                var response = await _supabaseClient.From<UserDataModel>()
                    .Select("*").Where(x => x.UserId == userId)
                    .Get();

                return response.Model;
            }

            return rezult;
        }

        public async Task UpdateUserDataAsync(string name, string email, string phone, DateTime? birth, string image = "")
        {
            try
            {
                if (Guid.TryParse(SupabaseClient.Auth.CurrentUser.Id, out var userId))
                {

                    var response = await _supabaseClient.From<UserDataModel>().
                        Where(x => x.UserId == userId)
                        .Set(x => x.Email, email).Set(x => x.Phone, phone).Set(x => x.Name, name).Set(x => x.Birth, birth).Set(x => x.Image, image)
                        .Update(); ;

                    if (SupabaseClient.Auth.CurrentUser.Email != email)
                    {
                        // change email in auth shema
                    }
                    if (SupabaseClient.Auth.CurrentUser.Phone != phone)
                    {
                        // change phone in auth shema
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user data: {ex.Message}");

            }
        }
    }
}

public static class Util
{
    public static Supabase.Client GetSupabase()
    {
        Supabase.Client? c = IPlatformApplication.Current?.Services.GetService<Supabase.Client>();
        if (c == null)
        {
            var url = kyiv.Constants.ApiKeys.SUPABASE_URL;// "https://<url here>.supabase.co";
            var key = kyiv.Constants.ApiKeys.SUPABASE_KEY;
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
               // SessionHandler = new CustomSessionHandler()
            };
            return new Supabase.Client(url, key, options);
        }
        return c;
    }
}