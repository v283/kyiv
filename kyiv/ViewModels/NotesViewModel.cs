using System;
using System.Collections.ObjectModel;
using System.Data;
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

        private ObservableCollection<NoteModel> _notes;

        public ObservableCollection<NoteModel> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }


        public NotesViewModel()
        {
            _databaseService = new LocalDataService();
            Notes = new ObservableCollection<NoteModel>();
            LoadNotes();

            MessagingCenter.Subscribe<NoteDetailPage, NoteModel>(this, "NoteDeleted", (sender, deletedNote) =>
            {
                var noteToRemove = Notes.FirstOrDefault(n => n.Id == deletedNote.Id);
                if (noteToRemove != null)
                {
                    Notes.Remove(noteToRemove);
                }
            });
        }

        public void LoadNotes()
        {
            var notesFromDb = _databaseService.GetNoteModels();
            Notes.Clear();
            foreach (var note in notesFromDb)
            {
                Notes.Add(note);
            }
        }




        [RelayCommand]
        private async Task CreateNote()
        {
            var newNote = new NoteModel { Name = "Нова нотатка", Text = "", Date = "" };

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

