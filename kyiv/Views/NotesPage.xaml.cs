using Microsoft.Maui.Controls;
using kyiv.Services;
using kyiv.Models;
using SQLite;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class NotesPage : ContentPage
{
	private LocalDataService _databaseService;

	public ObservableCollection<NoteModel> Notes { get; set; } = new ObservableCollection<NoteModel>();
	NotesViewModel vm;

    public NotesPage()
	{
		InitializeComponent();

		_databaseService = new LocalDataService();
		vm = new();
		BindingContext = vm;
        vm.LoadNotes();
    }

    protected override void OnAppearing()
    {
        //vm.LoadNotes();
        base.OnAppearing();
        
    }

 //   private void LoadNotes()
	//{
	//	Notes.Clear();

 //       var notes = _databaseService.GetNotes();

	//	foreach(var note in notes)
	//	{
	//		Notes.Add(note);
	//	}

	//	//NotesListView.ItemsSource = notes;

	//}

	//private async void OnCreateNoteClicked(object sender, EventArgs e)
	//{
	//	var newNote = new NoteModel { Name = "���� �������", Text = "" };

	//	_databaseService.AddNote(newNote);

	//	Notes.Add(newNote);

	//	await Navigation.PushAsync(new NoteDetailPage(newNote));
 //   }

	//private async void OnNoteTapped(object sender, SelectedItemChangedEventArgs e)
	//{
	//	if(e.SelectedItem is NoteModel selectedNote)
	//	{
	//		await Navigation.PushAsync(new NoteDetailPage(selectedNote));

	//		NotesListView.SelectedItem = null;
	//	}
	//}

}