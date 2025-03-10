using kyiv.Services;
using CommunityToolkit.Maui.Views;
using kyiv.Views.Templates;
using Supabase.Interfaces;
using kyiv.Models;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class UserSettingsView : ContentPage
{
	readonly DataService _dataService;

	public UserSettingsView(IDataService dataService, UserDataModel userData)
	{
		InitializeComponent();
		_dataService = (DataService)dataService;
        BindingContext = new UserSettingsViewModel(dataService, userData);

    }


}
