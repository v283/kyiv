using kyiv.Services;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class MarkView : ContentPage
{
    private readonly DataService _dataService;
    public MarkView(IDataService dataService)
    {
        _dataService = (DataService)dataService;
        InitializeComponent();

        BindingContext = new MarkViewModel(_dataService);
	}
}
