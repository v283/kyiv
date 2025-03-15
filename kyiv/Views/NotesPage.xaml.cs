
using kyiv.Services;
using kyiv.Models;

using kyiv.ViewModels;

namespace kyiv.Views;

public partial class NotesPage : ContentPage
{
    private LocalDataService _databaseService;

    public List<NoteModel> Notes;

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
        base.OnAppearing();

    }

}