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

    private double _startX; // ��������� ������� ������
    private double _startY;
    private double _lastTotalX; // ������ �������� TotalX
    private double _lastTotalY; // ������ �������� TotalY

    private async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Debug.WriteLine($"OnPanUpdated: StatusType = {e.StatusType}, TotalX = {e.TotalX}, TotalY = {e.TotalY}");

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _startX = e.TotalX; // �����'������� ��������� ������� ������
                _startY = e.TotalY;
                Debug.WriteLine("����� ���������.");
                break;

            case GestureStatus.Running:
                _lastTotalX = e.TotalX; // ��������� ������ �������� TotalX
                _lastTotalY = e.TotalY; // ��������� ������ �������� TotalY
                double diffY = _lastTotalY - _startY; // г����� �� ��������
                double diffX = _lastTotalX - _startX; // г����� �� ����������

                if (Math.Abs(diffY) > Math.Abs(diffX)) // ������������ ��� (�������������)
                {
                    // ���� �������, ������� ����� ��� ����������� ���������
                    Debug.WriteLine("������������ ����� ����������.");
                }
                break;

            case GestureStatus.Completed:
                // ������������� �������� �������� _lastTotalX �� _lastTotalY
                double finalDiffX = _lastTotalX - _startX; // г����� �� ����������
                double finalDiffY = _lastTotalY - _startY; // г����� �� ��������

                // ���������, �� ��� ��������� ��������������
                if (Math.Abs(finalDiffX) > Math.Abs(finalDiffY)) // �������������� ���
                {
                    if (Math.Abs(finalDiffX) < 50) // ���������� ������ ������
                    {
                        Debug.WriteLine("����� ������� ��������, ����������.");
                        return;
                    }

                    if (finalDiffX < -50) // ����� ����
                    {
                        Debug.WriteLine("�������� ����� ����.");
                        await Shell.Current.GoToAsync("//catalogview"); // ������� �� ������� AccountView
                    }
                    else if (finalDiffX > 50) // ����� ������
                    {
                        Debug.WriteLine("�������� ����� ������.");
                        await Shell.Current.GoToAsync("//mainview"); // ������� �� ������� RecipeView
                    }
                }
                Debug.WriteLine("����� ���������.");
                break;

            case GestureStatus.Canceled:
                Debug.WriteLine("����� ���������.");
                break;

            default:
                Debug.WriteLine($"�������� ������ ������: {e.StatusType}");
                break;
        }
    }
}