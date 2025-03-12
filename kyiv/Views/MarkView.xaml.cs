using kyiv.Services;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class MarkView : ContentPage
{
    private readonly DataService _dataService;
    MarkViewModel vm;
    public MarkView(IDataService dataService)
    {
        _dataService = (DataService)dataService;
        InitializeComponent();
        vm = new MarkViewModel(_dataService);
        BindingContext = vm;

	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.LoadCommentsAsync();
    }
}
