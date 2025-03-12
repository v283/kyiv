using kyiv.Models;

namespace kyiv.Views.Templates;

public partial class NoteItemTemplate : ContentView
{
	public NoteItemTemplate()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private async void OnNoteTapped(object sender, EventArgs e)
    {
        if (BindingContext is NoteModel note)
        {
            await Navigation.PushAsync(new NoteDetailPage(note));

            //NotesListView.SelectedItem = null;
        }
    }
}