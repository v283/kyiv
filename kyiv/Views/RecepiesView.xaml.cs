using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System.Diagnostics;

namespace kyiv.Views;

public partial class RecepiesView : ContentPage
{
    public RecepiesView()
    {
        InitializeComponent();
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
                _startX = e.TotalX; // Запам'ятовуємо початкову позицію свайпу
                _startY = e.TotalY;
                Debug.WriteLine("Свайп розпочато.");
                break;

            case GestureStatus.Running:
                _lastTotalX = e.TotalX; // Оновлюємо останнє значення TotalX
                _lastTotalY = e.TotalY; // Оновлюємо останнє значення TotalY
                double diffY = _lastTotalY - _startY; // Різниця по вертикалі
                double diffX = _lastTotalX - _startX; // Різниця по горизонталі

                if (Math.Abs(diffY) > Math.Abs(diffX)) // Вертикальний рух (прокручування)
                {
                    // Якщо потрібно, додайте логіку для вертикальної прокрутки
                    Debug.WriteLine("Вертикальний свайп ігнорується.");
                }
                break;

            case GestureStatus.Completed:
                // Використовуємо збережені значення _lastTotalX та _lastTotalY
                double finalDiffX = _lastTotalX - _startX; // Різниця по горизонталі
                double finalDiffY = _lastTotalY - _startY; // Різниця по вертикалі

                // Визначаємо, чи рух переважно горизонтальний
                if (Math.Abs(finalDiffX) > Math.Abs(finalDiffY)) // Горизонтальний рух
                {
                    if (Math.Abs(finalDiffX) < 50) // Ігнорувати короткі свайпи
                    {
                        Debug.WriteLine("Свайп занадто короткий, ігнорується.");
                        return;
                    }

                    if (finalDiffX < -50) // Свайп вліво
                    {
                        Debug.WriteLine("Виявлено свайп вліво.");
                        await Shell.Current.GoToAsync("//catalogview"); // Перехід на сторінку AccountView
                    }
                    else if (finalDiffX > 50) // Свайп вправо
                    {
                        Debug.WriteLine("Виявлено свайп вправо.");
                        await Shell.Current.GoToAsync("//mainview"); // Перехід на сторінку RecipeView
                    }
                }
                Debug.WriteLine("Свайп завершено.");
                break;

            case GestureStatus.Canceled:
                Debug.WriteLine("Свайп скасовано.");
                break;

            default:
                Debug.WriteLine($"Невідомий статус свайпу: {e.StatusType}");
                break;
        }
    }
}