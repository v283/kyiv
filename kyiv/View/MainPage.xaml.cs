using CommunityToolkit.Mvvm.Input;
using kyiv;
using kyiv.ViewModels;

namespace kyiv.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ProgressBar progressBar = new ProgressBar();
            BindingContext = new MainViewModel();
            
        }
    }
}



