using CommunityToolkit.Mvvm.Input;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class MainView : ContentPage
{

	public MainView()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}


}


