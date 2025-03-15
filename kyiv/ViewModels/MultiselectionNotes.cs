using CommunityToolkit.Mvvm.ComponentModel;
using kyiv.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using kyiv.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kyiv.ViewModels
{
    public partial class MultiselectionNotes : ObservableObject
    {
        public ObservableCollection<NoteModel> Notes { get; set; } = new();

        private LocalDataService _databaseService;

        public MultiselectionNotes()
        {

            _databaseService = new LocalDataService();
            Notes = new ObservableCollection<NoteModel>(_databaseService.GetNotes());

        }

        [ObservableProperty]
        private string _selectedNoteNumber;
    }
}
