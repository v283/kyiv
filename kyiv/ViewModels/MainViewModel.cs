using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Views.Templates;
using Plugin.MauiMTAdmob;

namespace kyiv.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {

        }

        [RelayCommand]
        private async Task ToGpt()
        {
            await Shell.Current.GoToAsync($"chatgpt?question=hello");

            //var emailCkeck = await Shell.Current.CurrentPage.ShowPopupAsync(new EmailCkeckPopup());
        }

        [RelayCommand]
        private async Task AddMob()
        {
            #if ANDROID
            CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
            #elif IOS
            CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/4411468910");
            #endif


            if (CrossMauiMTAdmob.Current.IsInterstitialLoaded())
            {
                CrossMauiMTAdmob.Current.ShowInterstitial();
            }
        }
    }
}

