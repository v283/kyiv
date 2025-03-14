using kyiv.Models;
using kyiv.Services;

namespace kyiv.Views;

public partial class NoteDetailPage : ContentPage
{
	private NoteModel _note;
	private LocalDataService _databaseService;

	public NoteDetailPage(NoteModel note)
	{
		InitializeComponent();

		_note = note;
		_databaseService = new LocalDataService();

		BindingContext = _note;
	}

	private async void OnSaveChangesClicked(object sender, EventArgs e)
	{
		_databaseService.UpdateNote(_note);

		await Navigation.PopAsync();
	}

    private async void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is NoteModel note)
        {
            bool confirm = await DisplayAlert("Видалити нотатку", "Ви впевнені, що хочете видалити нотатку?", "Так", "Ні");

            if (confirm)
            {
                _databaseService.DeleteNote(note.Id);

				await Navigation.PopAsync();
            }
        }

    }

    protected override void OnDisappearing()
	{

        _databaseService.UpdateNote(_note);
        base.OnDisappearing();

	}

}