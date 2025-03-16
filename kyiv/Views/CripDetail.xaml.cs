
namespace kyiv.Views;

public partial class CripDetail : ContentPage
{
	public CripDetail(string source)
    {
		InitializeComponent();
        webV.Source = source;
    }
}
