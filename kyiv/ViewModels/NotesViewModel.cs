using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Models;
using kyiv.Services;
using kyiv.Views;

namespace kyiv.ViewModels
{
    public partial class NotesViewModel : ObservableObject
    {
        private LocalDataService _databaseService;

        [ObservableProperty]
        public List<NoteModel> notes;


        public NotesViewModel()
        {
            _databaseService = new LocalDataService();
            LoadNotes();
        }

        public void LoadNotes()
        {
            Notes = _databaseService.GetNoteModels();
        }


        [RelayCommand]
        private async Task CreateNote()
        {
            var newNote = new NoteModel { Name = "���� �������", Text = "" };

            _databaseService.AddNoteModel(newNote);

            Notes.Add(newNote);

            await Shell.Current.Navigation.PushAsync(new NoteDetailPage(newNote));
        }

        [RelayCommand]
        private async Task NoteTapped(NoteModel selected)
        {
            await Shell.Current.Navigation.PushAsync(new NoteDetailPage(selected));

        }
    }
}
