using System;
using System.Reactive.Linq;
using kyiv.Models;
using kyiv.Views;
using kyiv.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.ViewModels
{
	public partial class MarkViewModel:ObservableObject
	{
        private readonly DataService _dataService;

        [ObservableProperty]
		List<MarkModel> comments;

		public MarkViewModel(IDataService dataService)
        {
            _dataService = (DataService)dataService;
            Initialyze();
        }
        private async void Initialyze()
        {
            Comments = (await _dataService.SupabaseClient.From<MarkModel>().Get()).Models;
        }
        [RelayCommand]
        private async void WriteComment()
        {
            await Shell.Current.Navigation.PushModalAsync(new WriteCommentView(_dataService));
        }
        
	}
}

