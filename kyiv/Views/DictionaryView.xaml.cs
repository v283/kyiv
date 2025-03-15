
using kyiv.Models;
using CommunityToolkit.Mvvm.Input;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class DictionaryView : ContentPage
{
    DictionaryViewModel vm = new DictionaryViewModel();
    public DictionaryView()
	{
		InitializeComponent();
        BindingContext = vm;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.LoadUI();
    }
}
