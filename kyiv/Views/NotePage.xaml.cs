using Microsoft.Maui.Controls;
using kyiv.Models;
using SQLite;

namespace kyiv.Views;

public partial class NotePage : ContentPage
{
	public NotePage()
	{
		InitializeComponent();

		LoadSavedNotes();
	}

	private void LoadSavedNotes()
	{
		//var localDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		//var dbPath = Path.Combine(localDataPath, "Notes.db");
		if (Preferences.ContainsKey("SavedNotes"))
		{
			NotesEditor.Text = Preferences.Get("SavedNotes", string.Empty);
		}
	}

	private void OnSaveNotesClicked(object sender, EventArgs e)
	{
		Preferences.Set("SavedNotes", NotesEditor.Text);

		DisplayAlert("Успіх", "Нотатку збережено!", "ОК");

  //      var localDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
  //      var dbPath = Path.Combine(localDataPath, "Notes.db");

		//var db = new SQLiteConnection(dbPath);

		//db.Insert(new Note { Text = "Моя нотатка" });

		//var notes = db.Table<Note>().ToList();
    }
}