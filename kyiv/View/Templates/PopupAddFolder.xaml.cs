using CommunityToolkit.Maui.Views;
namespace kyiv.View.Templates;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PopupAddFolder : Popup
{
    public string EnteredText => NewValue.Text;
    public PopupAddFolder()
    {
        InitializeComponent();
        CloseCommand = new Command(() => Close(null));
        ChangeCommand = new Command(() => Close(EnteredText));
        BindingContext = this;
    }

    public Command CloseCommand { get; }
    public Command ChangeCommand { get; }

    private void OnSubmit()
    {

    }
}


