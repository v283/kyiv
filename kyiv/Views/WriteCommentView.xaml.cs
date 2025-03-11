using kyiv.ViewModels;
using CommunityToolkit.Mvvm.Input;
using kyiv.Services;

namespace kyiv.Views;

public partial class WriteCommentView : ContentPage
{
	public WriteCommentView(DataService data)
	{
		InitializeComponent();
		BindingContext = new WriteCommentViewModel(data);


    }

}
