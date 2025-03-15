using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
    public partial class TestModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string question;
        [ObservableProperty]
        private string answA;
        [ObservableProperty]
        private string answB;
        [ObservableProperty]
        private string answC;
        [ObservableProperty]
        private string answD;
        [ObservableProperty]
        private string correctAnsw;
        [ObservableProperty]
        private string explanation;


    }
}
