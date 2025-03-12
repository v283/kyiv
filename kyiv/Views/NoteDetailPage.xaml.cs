using Android.App;
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

	private async void OnSaveChangesClicked(object sender, EventArgs e)
	{
		_databaseService.UpdateNote(_note);

		await Navigation.PopAsync();
	}

    protected override void OnDisappearing()
	{
		base.OnDisappearing();

		//_databaseService.UpdateNote(_note);
	}

}