using System;
using System.Reactive.Linq;
using kyiv.Models;
using kyiv.Views;
using kyiv.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using kyiv.ViewModels;
public partial class MarkViewModel : ObservableObject
{
    private readonly DataService _dataService;

    [ObservableProperty]
    private ObservableCollection<MarkModel> comments;

    public MarkViewModel(IDataService dataService)
    {
        _dataService = (DataService)dataService;
        Comments = new ObservableCollection<MarkModel>(); // Ініціалізація колекції
        Initialyze();
        // Підписатися на повідомлення
        MessagingCenter.Subscribe<WriteCommentViewModel>(this, "RefreshComments", async (sender) =>
        {
            await LoadCommentsAsync();
        });
    }

    private async void Initialyze()
    {
        await LoadCommentsAsync();
    }

    public async Task LoadCommentsAsync()
    {
        try
        {
            // Отримати дані, відсортовані за датою (від новіших до старіших)
            var data = (await _dataService.SupabaseClient
                .From<MarkModel>()
                .Order("writen_at", Supabase.Postgrest.Constants.Ordering.Descending) // Сортування за спаданням
                .Get())
                .Models;

            Comments.Clear(); // Очистити поточні дані
            foreach (var item in data)
            {
                Comments.Add(item); // Додати нові дані
            }
            Debug.WriteLine("Коментарі успішно оновлено.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Помилка при оновленні коментарів: {ex.Message}");
        }
    }


    [RelayCommand]
    private async void WriteComment()
    {
        await Shell.Current.Navigation.PushModalAsync(new WriteCommentView(_dataService));
    }
}