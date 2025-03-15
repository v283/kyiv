
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
        _databaseService.UpdateNoteModel(_note);

        await Navigation.PopAsync();
    }

    private async void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is NoteModel note)
        {
            bool confirm = await DisplayAlert("Видалення нотатків", "Ви впевнені, що хочете видалити нотатку?", "Так", "Ні");

            if (confirm)
            {
                _databaseService.DeleteNoteModel(note.Id);

                await Navigation.PopAsync();
            }
        }

    }

    protected override void OnDisappearing()
    {

        _databaseService.UpdateNoteModel(_note);
        Thread.Sleep(500);
        base.OnDisappearing();

    }

}
