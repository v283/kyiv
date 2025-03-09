using CommunityToolkit.Mvvm.Input;
using kyiv.ViewModels;

namespace kyiv;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}


}


