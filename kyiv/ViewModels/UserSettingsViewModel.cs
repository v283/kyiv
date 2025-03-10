using System;
using kyiv.Models;
using kyiv.Services;
using kyiv.Views.Templates;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Supabase.Interfaces;

namespace kyiv.ViewModels
{
	public partial class UserSettingsViewModel:ObservableObject
	{
        private readonly DataService _dataService;

		[ObservableProperty]
        private UserDataModel bindUserDataModel;

        public UserSettingsViewModel(IDataService dataService, UserDataModel userData)
		{
			_dataService = (DataService)dataService;
            BindUserDataModel = userData;
        }

        [RelayCommand]
        private async Task SignOut()
        {
            await _dataService.SignOutAsync();
            await Shell.Current.Navigation.PopAsync();
        }
        [RelayCommand]
        private async Task ChangePassword()
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(new CustomPopupPasword());

            if (result is Tuple<string, string> passwords)
            {
                string oldPass = passwords.Item1;
                string newPass = passwords.Item2;

                var signInPaswCheck = await _dataService.SupabaseClient.Auth.SignIn(_dataService.SupabaseClient.Auth.CurrentUser.Email, oldPass);

                if (signInPaswCheck.User != null)
                {

                    var updateResult = await _dataService.SupabaseClient.Auth.Update(new Supabase.Gotrue.UserAttributes { Password = newPass });

                    if (updateResult.UserMetadata != null)
                    {
                        // Password update succeeded.
                        await Shell.Current.DisplayAlert("Success", "Password updated successfully.", "OK");
                    }
                    else
                    {
                        // Handle errors here.
                        await Shell.Current.DisplayAlert("Error", "Password update failed.", "OK");
                    }
                }



            }
        }
        [RelayCommand]
        private async Task ChangePhone()
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(new CustomPopupEntry(Keyboard.Telephone));

            if (result is string newValue)
            {
                BindUserDataModel.Phone = newValue;
                await _dataService.UpdateUserDataAsync(BindUserDataModel.Name, BindUserDataModel.Email, BindUserDataModel.Phone, BindUserDataModel.Birth);
            }
        }

        [RelayCommand]
        private async Task ChangeEmail()
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(new CustomPopupEntry(Keyboard.Email));
            if (result is string newValue)
            {
                BindUserDataModel.Email = newValue;
                await _dataService.UpdateUserDataAsync(BindUserDataModel.Name, BindUserDataModel.Email, BindUserDataModel.Phone, BindUserDataModel.Birth);
            }
        }

        [RelayCommand]
        private async Task ChangeName()
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(new CustomPopupEntry(Keyboard.Default));

            if (result is string newValue)
            {
                BindUserDataModel.Name = newValue;
                await _dataService.UpdateUserDataAsync(BindUserDataModel.Name, BindUserDataModel.Email, BindUserDataModel.Phone, BindUserDataModel.Birth);
            }

        }

        [RelayCommand]
        private async Task ChangeDateOfBirth()
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(new DatePickerPopup());
            if (result is DateTime newValue)
            {
                BindUserDataModel.Birth = newValue;
                await _dataService.UpdateUserDataAsync(BindUserDataModel.Name, BindUserDataModel.Email, BindUserDataModel.Phone, BindUserDataModel.Birth);
            }
        }

    }
}

