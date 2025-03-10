using System;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.ViewModels
{
	public partial class WriteCommentViewModel
	{
        public WriteCommentViewModel()
        {

        }
        [RelayCommand]
        private async void Close()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}

