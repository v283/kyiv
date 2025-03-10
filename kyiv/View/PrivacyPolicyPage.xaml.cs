using System.Windows.Input;

namespace kyiv.View;

public partial class PrivacyPolicyPage : ContentPage
{
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public PrivacyPolicyPage()
	{
		//InitializeComponent();
        BindingContext = this;
    }
    
}