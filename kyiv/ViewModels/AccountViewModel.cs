using System;
using kyiv.Models;
using kyiv.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.ViewModels
{
	public partial class AccountViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        [ObservableProperty]
        private UserDataModel bindUserDataModel;

        public AccountViewModel(IDataService dataService)
        {
            _dataService = (DataService)dataService;
            Initialize();
        }
        public async void Initialize()
        {
            if (_dataService.SupabaseClient.Auth.CurrentSession != null)
            {
                BindUserDataModel = await _dataService.GetUserData();
            }
        }
    }
}

