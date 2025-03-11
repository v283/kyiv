using Android.Database;
using kyiv.Models;
using Microsoft.IdentityModel.Tokens;
using Supabase;

using static Supabase.Postgrest.Constants;

namespace kyiv.Services
{
    public class DataService : IDataService
    {
        // log
        // pasw 1234567
        private readonly Client _supabaseClient;


        public Client SupabaseClient { get => _supabaseClient; }

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
            var options = new Supabase.Gotrue.SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "name", name } // Add the name as metadata
                }
            };
            var response = await _supabaseClient.Auth.SignUp(email, password, options);

            // check wheather user confirmed sign up than
           // UpdateUserDataAsync(name, email, "", new(), "svg_user.svg");
            return false;
        }
        public async Task SignOutAsync()
        {
            await _supabaseClient.Auth.SignOut();
            SecureStorage.RemoveAll();
        }
        public async Task<bool> AddMarkAsync(string commentText)
        {
            try
            {
                // Отримуємо дані користувача
                var userData = await GetUserData(); // Використовуємо await, щоб отримати результат
                Guid uuid = userData.UserId; // Беремо UserId з отриманих даних користувача

                // Перевірка на порожній коментар
                if (string.IsNullOrWhiteSpace(commentText))
                {
                    Console.WriteLine("Помилка: коментар не може бути порожнім!");
                    return false;
                }

                // Створення нового коментаря
                MarkModel newMark = new()
                {
                    Id = Guid.NewGuid(), // Генеруємо унікальний ID
                    Text = commentText,  // Текст коментаря
                    UserId = uuid,       // UUID користувача
                    WritenAt = DateTime.UtcNow // Час написання
                };

                // Відправка нового коментаря до бази даних
                var response = await _supabaseClient.From<MarkModel>().Insert(newMark);

                // Перевірка відповіді та підтвердження успіху
                if (response != null && response.Models.Count > 0)
                {
                    Console.WriteLine("Коментар успішно додано до бази даних.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок
                Console.WriteLine($"Помилка при додаванні коментаря: {ex.Message}");
            }

            return false;
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