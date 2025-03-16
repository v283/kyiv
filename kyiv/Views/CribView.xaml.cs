using kyiv.ViewModels;
namespace kyiv.Views;

public partial class CribView : ContentPage
{
	public CribView()
	{
		InitializeComponent();

		BindingContext = new CribViewModel();
	

    }
}
