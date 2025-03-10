using kyiv.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.Views;

public partial class WriteCommentView : ContentPage
{
	public WriteCommentView()
	{
		InitializeComponent();
		BindingContext = new WriteCommentViewModel();


    }

}
