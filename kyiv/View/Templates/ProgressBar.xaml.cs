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
        new DayData("Ïí", 20),
        new DayData("Âò", 30),
        new DayData("Ñð", 40),
        new DayData("×ò", 80),
        new DayData("Ïò", 0),
        new DayData("Ñá", 0),
        new DayData("Íä", 0),
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