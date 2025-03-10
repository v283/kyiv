using System.Collections.ObjectModel;

namespace kyiv.View.Templates;

public partial class ProgressBar : ContentView
{
	public ObservableCollection<DayData> Days { get; set; }
    public ProgressBar()
    {
        InitializeComponent();

        Days = new ObservableCollection<DayData>
    {
        new DayData("��", 20),
        new DayData("��", 30),
        new DayData("��", 40),
        new DayData("��", 80),
        new DayData("��", 0),
        new DayData("��", 0),
        new DayData("��", 0),
    };

        BindingContext = this;
    }

}
public class DayData
{
	public string Name { get; set; }
	public int Level { get; set; }

	public DayData(string name, int level)
    {
        Name = name;
        Level = level;
    }
}