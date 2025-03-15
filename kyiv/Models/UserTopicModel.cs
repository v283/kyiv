using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
    public partial class UserTopicModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string isDoneImage;

    }
}
