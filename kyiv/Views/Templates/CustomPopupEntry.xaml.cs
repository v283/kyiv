using CommunityToolkit.Maui.Views;

namespace kyiv.Views.Templates;

public partial class CustomPopupEntry : Popup
{
    public Keyboard EntryType { get; set; }
    public string PopUpTitle { get; set; }

    public string EnteredText => NewValue.Text;
    public CustomPopupEntry(Keyboard entryType)
    {
        InitializeComponent();
        CloseCommand = new Command(() => Close(null));
        ChangeCommand = new Command(() => Close(EnteredText));
        EntryType = entryType;
        if (entryType == Keyboard.Telephone)
        {
            PopUpTitle = "Змінити номер";
        }
        else if (entryType == Keyboard.Email)
        {
            PopUpTitle = "Змінити email";
        }
        else
        {
            PopUpTitle = "Змінити ПІБ";
        }
        BindingContext = this;
    }

    public Command CloseCommand { get; }
    public Command ChangeCommand { get; }

    private void OnSubmit()
    {

    }
}

