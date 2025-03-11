using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Model
{
    public partial class ProgressModel : ObservableObject
    {
        [ObservableProperty]
        private string level ;
        [ObservableProperty]
        private double totalAmount ;
        [ObservableProperty]
        private double doneAmount ;
        [ObservableProperty]
        private string topicProgress ;

    }
}
