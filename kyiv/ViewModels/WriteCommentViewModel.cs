using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Services;

namespace kyiv.ViewModels
{
    public partial class WriteCommentViewModel : ObservableObject
    {
        [ObservableProperty]
        private string commentText;

        private readonly DataService _dataService;

        public WriteCommentViewModel(DataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService), "DataService не може бути null!");
        }

        [RelayCommand]
        private async void Close()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        private async void SendMark()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                Console.WriteLine("Коментар не може бути порожнім!");
                return;
            }

            if (_dataService == null)
            {
                Console.WriteLine("Помилка: DataService не ініціалізовано!");
                return;
            }

            try
            {
                // Викликаємо асинхронний метод і чекаємо результат
                bool success = await _dataService.AddMarkAsync(CommentText);

                if (success)
                {
                    CommentText = string.Empty; // Очистити поле коментаря після успішного додавання
                }
                else
                {
                    CommentText = "Не вдалося додати коментар.";
                }
            }
            catch (Exception ex)
            {
                // Обробляємо помилки та виводимо їх
                CommentText = ($"Сталася помилка при додаванні коментаря: {ex.Message}");
            }
        }

    }
}
