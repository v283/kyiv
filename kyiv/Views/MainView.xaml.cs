using CommunityToolkit.Mvvm.Input;
using kyiv.ViewModels;
using System.Diagnostics;

namespace kyiv.Views;

public partial class MainView : ContentPage
{

    public MainView()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
    private double _startX; // Початкова позиція свайпу
    private double _startY;
    private double _lastTotalX; // Останнє значення TotalX
    private double _lastTotalY; // Останнє значення TotalY

    private async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Debug.WriteLine($"OnPanUpdated: StatusType = {e.StatusType}, TotalX = {e.TotalX}, TotalY = {e.TotalY}");

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _startX = e.TotalX;
                _startY = e.TotalY;
                Debug.WriteLine("Свайп розпочато.");
                break;

            case GestureStatus.Running:
                _lastTotalX = e.TotalX;
                _lastTotalY = e.TotalY;
                break;

            case GestureStatus.Completed:
                double finalDiffX = _lastTotalX - _startX;
                double finalDiffY = _lastTotalY - _startY;

                if (Math.Abs(finalDiffX) > Math.Abs(finalDiffY) && Math.Abs(finalDiffX) > 50) // Перевірка горизонтального руху
                {
                    if (finalDiffX < -50)
                    {
                        Debug.WriteLine("Виявлено свайп вліво.");
                        await Shell.Current.GoToAsync("//recipeview");

                    }
                    else if (finalDiffX > 50)
                    {
                        Debug.WriteLine("Виявлено свайп вправо.");
                    }
                }
                else
                {
                    Debug.WriteLine("Свайп занадто короткий або не горизонтальний, ігнорується.");
                }
                break;

            case GestureStatus.Canceled:
                Debug.WriteLine("Свайп скасовано.");
                break;
        }
    }

}


