using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Views.Templates;

namespace kyiv.ViewModels
{
	public partial class MainViewModel: ObservableObject
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
    }
}

