using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Services;
using kyiv.Models;
using kyiv.Views;
using Plugin.MauiMTAdmob;
using kyiv.Views.Templates;

namespace kyiv.ViewModels
{
	public partial class DictionaryViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<DictionaryModel> foldersLayout;


        public DictionaryViewModel()
        {
            LoadUI();
        }

        public void LoadUI()
        {
            FoldersLayout = DbProvider.GetDictionaryFolders("DictFolders");

        }


        [RelayCommand]
        private async Task AddFolder()
        {
            string result = (string)(await Shell.Current.CurrentPage.ShowPopupAsync(new PopupAddFolder()));
            Random random = new Random();
            if (result != "" && result != null)
            {
                DateOnly now = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                DictionaryModel dictionary = new DictionaryModel();
                dictionary.FolderImage = "folder.png";
                dictionary.FolderName = result;
                dictionary.DictRef = "folder" + now.DayNumber + random.Next(1, 1000);
                DbProvider.AddDictionaryFolder(dictionary);

                DbProvider.CreateDictionaryWordsTable(dictionary.DictRef);

                LoadUI();

                CrossMauiMTAdmob.Current.ShowInterstitial();
            }


        }


        [RelayCommand]
        private async Task FolderSelect(DictionaryModel folder)
        { 
            try
            {
                await Shell.Current.Navigation.PushAsync(new WordsPage(folder));
            }
            catch (Exception ex) {    LoadUI();   }
        }
    }
}

