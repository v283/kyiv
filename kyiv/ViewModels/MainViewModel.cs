using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        }
    }
}

