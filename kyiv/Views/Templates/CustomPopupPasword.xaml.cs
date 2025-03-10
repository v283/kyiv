using CommunityToolkit.Maui.Views;

namespace kyiv.Views.Templates;

public partial class CustomPopupPasword : Popup
{
    public CustomPopupPasword()
    {
        InitializeComponent();
        CloseCommand = new Command(() => Close(null));
        ChangePasswordCommand = new Command(OnSubmit);
        BindingContext = this;
    }

    public Command CloseCommand { get; }
    public Command ChangePasswordCommand { get; }

    private void OnSubmit()
    {
        string oldPassword = OldPasswordEntry.Text;
        string newPassword = NewPasswordEntry.Text;

        if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
        {
            Close(new Tuple<string, string>(oldPassword, newPassword));
        }
    }
}
