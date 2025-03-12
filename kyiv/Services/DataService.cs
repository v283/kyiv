using kyiv.Models;
using Supabase;
using Supabase.Gotrue;
using CommunityToolkit.Maui.Views;
using kyiv.Views.Templates;
using static Supabase.Gotrue.Constants;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

<<<<<<<<< Temporary merge branch 1
        public async Task<bool> AddMarkAsync(string commentText, string topic)
        {
            try
            {
                var userData = await GetUserData();
                if (userData == null || userData.UserId == Guid.Empty)
                {
                    Debug.WriteLine("Помилка: не вдалося отримати дані користувача.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(commentText) || string.IsNullOrWhiteSpace(topic))
                {
                    Debug.WriteLine("Помилка: коментар або тема не можуть бути порожніми!");
                    return false;
                }

                // Отримати поточний час і округлити його до хвилин
                var currentTime = DateTime.UtcNow;
                var roundedTime = new DateTime(
                    currentTime.Year,
                    currentTime.Month,
                    currentTime.Day,
                    currentTime.Hour,
                    currentTime.Minute,
                    0 // Секунди встановлюємо в 0
                );
                MarkModel newMark = new()
                {
                    Id = Guid.NewGuid(),
                    Text = commentText,
                    UserId = userData.UserId,
                    Name = userData.Name, // Заповнюємо ім'я користувача
                    WritenAt = roundedTime, // Використовуємо округлений час
                    Topic = topic // Додаємо тему
                };

                var response = await _supabaseClient.From<MarkModel>().Insert(newMark);

                if (response != null && response.Models.Count > 0)
                {
                    Debug.WriteLine("Коментар успішно додано до бази даних.");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Помилка: не вдалося додати коментар до бази даних.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка при додаванні коментаря: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                return false;
            }
        }
=========

>>>>>>>>> Temporary merge branch 2
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
        public async Task SignOutAsync()
        public async Task<bool> AddMarkAsync(string commentText, string topic)
        {
            try
            {
                var userData = await GetUserData();
                if (userData == null || userData.UserId == Guid.Empty)
                {
                    Debug.WriteLine("Помилка: не вдалося отримати дані користувача.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(commentText) || string.IsNullOrWhiteSpace(topic))
                {
                    Debug.WriteLine("Помилка: коментар або тема не можуть бути порожніми!");
                    return false;
                }

                // Отримати поточний час і округлити його до хвилин
                var currentTime = DateTime.UtcNow;
                var roundedTime = new DateTime(
                    currentTime.Year,
                    currentTime.Month,
                    currentTime.Day,
                    currentTime.Hour,
                    currentTime.Minute,
                    0 // Секунди встановлюємо в 0
                );

                MarkModel newMark = new()
                {
                    Id = Guid.NewGuid(),
                    Text = commentText,
                    UserId = userData.UserId,
                    Name = userData.Name, // Заповнюємо ім'я користувача
                    WritenAt = roundedTime, // Використовуємо округлений час
                    Topic = topic // Додаємо тему
                };

                var response = await _supabaseClient.From<MarkModel>().Insert(newMark);

                if (response != null && response.Models.Count > 0)
                {
                    Debug.WriteLine("Коментар успішно додано до бази даних.");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Помилка: не вдалося додати коментар до бази даних.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка при додаванні коментаря: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                return false;
            }
        }

            await _supabaseClient.Auth.SignOut();
            SecureStorage.RemoveAll();
        }
<<<<<<<<< Temporary merge branch 1
        public async Task<UserDataModel> GetUserData()
        {
            UserDataModel result = new();

            // Перевірка, чи користувач автентифікований
            if (SupabaseClient.Auth.CurrentUser == null)
=========

        public async Task<UserDataModel> GetUserData()
        {
            UserDataModel rezult = new();
>>>>>>>>> Temporary merge branch 2
            {
                Console.WriteLine("Помилка: користувач не автентифікований.");
                return result;
            }
<<<<<<<<< Temporary merge branch 1

            if (Guid.TryParse(SupabaseClient.Auth.CurrentUser.Id, out var userId))
            {
                var response = await _supabaseClient.From<UserDataModel>()
                    .Select("*")
                    .Where(x => x.UserId == userId)
                    .Get();

                return response.Model ?? result; // Повертаємо результат або пустий об'єкт
=========
                    .Select("*").Where(x => x.UserId == userId)
                    .Get();

                return response.Model;
            }

            return rezult;
        }

>>>>>>>>> Temporary merge branch 2
            }

            return result;
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
