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
    private bool _isWriteCommentViewOpen = false; // Прапорець для відстеження стану вікна

    [ObservableProperty]
    private ObservableCollection<MarkModel> comments;
    [ObservableProperty]
    private bool isLoading;  // Прапорець для анімації завантаження

    public bool IsNotLoading => !IsLoading;  // Протилежне значення для видимості списку
    public MarkViewModel(IDataService dataService)
    {
        _dataService = (DataService)dataService;
        Comments = new ObservableCollection<MarkModel>(); // Ініціалізація колекції
        Initialyze();
    }

    private async void Initialyze()
    {
        await LoadCommentsAsync();
    }

    public async Task LoadCommentsAsync()
    {
        try
        {
            IsLoading = true;
            OnPropertyChanged(nameof(IsNotLoading)); // Оновлення прив’язки
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
        finally
        {
            IsLoading = false;
            OnPropertyChanged(nameof(IsNotLoading)); // Оновлення видимості після завантаження
        }
    }

    [RelayCommand]
    private async void WriteComment()
    {
        // Перевірка, чи вікно вже відкрите
        if (_isWriteCommentViewOpen)
        {
            return; // Якщо вікно вже відкрите, нічого не робимо
        }

        _isWriteCommentViewOpen = true; // Позначити, що вікно відкрите

        try
        {
            await Shell.Current.Navigation.PushModalAsync(new WriteCommentView(_dataService));
        }
        finally
        {
            _isWriteCommentViewOpen = false; // Позначити, що вікно закрите
        }
    }
}