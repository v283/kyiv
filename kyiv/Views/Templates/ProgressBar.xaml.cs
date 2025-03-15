using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Maui.Graphics;

using System.Drawing;

namespace kyiv.Views.Templates;

public partial class ProgressBar : ContentView
{
    public ObservableCollection<DayData> Days { get; set; }
    public ProgressBar()
    {
        InitializeComponent();

        Days = new ObservableCollection<DayData>
        {
            new DayData("Пн", 20),
            new DayData("Вт", 30),
            new DayData("Ср", 40),
            new DayData("Чт", 80),
            new DayData("Пт", 0),
            new DayData("Сб", 0),
            new DayData("Нд", 0),
        };

        BindingContext = this;
    }

}

public class DayData : INotifyPropertyChanged
{
    public string Name { get; set; }
    private int _level;

    public int Level
    {
        get => _level;
        set
        {
            if (_level != value)
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(ChangedColor)); // ��������� �������
            }
        }
    }

    public Microsoft.Maui.Graphics.Color ChangedColor => GetColor(Level);

    public event PropertyChangedEventHandler PropertyChanged;

    public DayData(string name, int level)
    {
        Name = name;
        Level = level;
    }

    private Microsoft.Maui.Graphics.Color GetColor(int percent)
    {
        return percent switch
        {
            < 25 => Microsoft.Maui.Graphics.Color.Parse("#C3375A"),
            < 50 => Microsoft.Maui.Graphics.Color.Parse("#EB6548"),
            < 75 => Microsoft.Maui.Graphics.Color.Parse("#FF9E7B"),
            _ => Microsoft.Maui.Graphics.Color.Parse("#6B9A6E")
        };
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

