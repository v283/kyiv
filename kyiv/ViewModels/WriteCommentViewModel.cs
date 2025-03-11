using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Services;

namespace kyiv.ViewModels
{
    public partial class WriteCommentViewModel : ObservableObject
    {
        [ObservableProperty]
        private string commentText, topic;

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
        private async Task SendMark()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                await Application.Current.MainPage.DisplayAlert("Помилка", "Коментар не може бути порожнім!", "OK");
                return;
            }

            if (_dataService == null)
            {
                await Application.Current.MainPage.DisplayAlert("Помилка", "DataService не ініціалізовано!", "OK");
                return;
            }

            try
            {
                bool success = await _dataService.AddMarkAsync(CommentText, Topic);

                if (success)
                {
                    CommentText = string.Empty;
                    Topic = string.Empty;
                    MessagingCenter.Send(this, "RefreshComments"); // Відправити повідомлення
                    await Shell.Current.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Помилка", "Не вдалося додати коментар.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Помилка", $"Сталася помилка при додаванні коментаря: {ex.Message}", "OK");
            }
        }
    }
}
