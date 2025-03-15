using kyiv.ViewModels;

namespace kyiv.Views;

public partial class TopicsView : ContentPage
{

    public TopicsView( string subj)
	{
		InitializeComponent();
        BindingContext = new TopicsViewModel(subj);

    }


}