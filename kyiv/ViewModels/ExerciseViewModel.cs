using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kyiv.View;

namespace kyiv.ViewModels
{
    public partial class ExerciseViewModel
    {
        public ExerciseViewModel()
        {
            Initialize();
        }
        private async void Initialize()
        {
            await StorageWritePermission();
            await StorageReadPermission();
            //await PrivacyPolicy();
            //CheckDatabaseVersion();
        }

        [RelayCommand]
        private async Task LevelSelected(string level)
        {
            await Shell.Current.Navigation.PushAsync(new TopicsView(level));
        }
        [RelayCommand]
        private async Task OpenFlyoutAsync()
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        //private async Task PrivacyPolicy()
        //{
        //    try
        //    {
        //        DbProvider.ConnctToDb();
        //        await DbProvider.GetTestByNameAsync("PresentSimple1");

        //    }
        //    catch (Exception ex)
        //    {
        //        string action = await Shell.Current.DisplayActionSheet("Privacy policy", "Accept", "Deny", "Read");

        //        DbProvider.FirstRunDb();
        //        DbProvider.ConnctToDb();


        //    }

        //}

        private async Task StorageWritePermission()
        {
            var status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
            {
                return;
            }

            if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
            {
                await Shell.Current.DisplayAlert("Needs permission", "For the normal operation of the application.", "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.StorageRead>();

        }

        private async Task StorageReadPermission()
        {
            var status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                return;
            }

            if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
            {
                await Shell.Current.DisplayAlert("Needs permission", "For the normal operation of the application.", "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.StorageRead>();

        }

        //private void CheckDatabaseVersion()
        //{
        //    int dbVersion = 0;
        //    try
        //    {
        //        dbVersion = DbProvider.GetDbVersion();
        //    }
        //    catch (Exception ex)
        //    {
        //        DbProvider.SetDbVersion(version);
        //    }

        //    if (dbVersion == version) { }
        //    else
        //    {
        //        DbProvider.ResetTestsDb();
        //        DbProvider.ConnctToDb();
        //        DbProvider.SetDbVersion(version);
        //    }

        //}
    }
}
