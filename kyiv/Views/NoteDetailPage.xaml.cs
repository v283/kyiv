using kyiv.Models;
using kyiv.Services;

namespace kyiv.Views;

public partial class NoteDetailPage : ContentPage
{
	private Note _note;
	private LocalDataService _databaseService;

	public NoteDetailPage(Note note)
	{
		InitializeComponent();

		_note = note;
		_databaseService = new LocalDataService();

		BindingContext = _note;
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();

		_databaseService.UpdateNote(_note);
	}
}