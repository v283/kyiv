using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace kyiv.Views.Templates;

public partial class DatePickerPopup : Popup
{
    public DateTime SelectedDate { get; private set; }

    public ICommand CloseCommand { get; }
    public ICommand SaveCommand { get; }

    public DatePickerPopup()
    {
        InitializeComponent();
        SaveCommand = new Command(() => Close(datePicker.Date));
        CloseCommand = new Command(() => Close(null));
        BindingContext = this;
    }
}
