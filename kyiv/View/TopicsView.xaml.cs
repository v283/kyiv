using kyiv.ViewModels;

namespace kyiv.View;

public partial class TopicsView : ContentPage
{
    private TopicsViewModel viewModel;

    public TopicsView( string level)
	{
		InitializeComponent();
        viewModel = new TopicsViewModel(level);
        BindingContext = viewModel;
        switch (level)
        {
            case "mainpage":
                Title = "MainPaige";
                break;
            case "pvp":
                Title = "Player Vs Player Tests";
                break;
            case "pve":
                Title = "Player Vs AI Tests";
                break;

            default:
                break;
        }
    }


}