
using kyiv.Models;
using kyiv.Services;

namespace kyiv.Views;

public partial class NoteDetailPage : ContentPage
{
    private NoteModel _note;
    private LocalDataService _databaseService;
    bool isDeleting;

    public NoteDetailPage(NoteModel note)
    {
        InitializeComponent();

        isDeleting = false;
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
            bool confirm = await DisplayAlert("Видалення нотатки", "Ви впевнені що хочете видалити нотатку?", "Так", "Ні");

            if (confirm)
            {
                _databaseService.DeleteNoteModel(note.Id);

                MessagingCenter.Send(this, "NoteDeleted", note);

                isDeleting = true;

                await Navigation.PopAsync();
            }
        }

    }

    protected override void OnDisappearing()
    {
        if (!isDeleting && _note.Name.Equals("Нова нотатка"))
        {
            _note.Date = DateTime.Now.ToString("dd/MM/yyyy");
            _note.Name = _note.Name + " " + _note.Date;
        }

        isDeleting = false;

        _databaseService.UpdateNoteModel(_note);
        Thread.Sleep(500);
        base.OnDisappearing();

    }

}
