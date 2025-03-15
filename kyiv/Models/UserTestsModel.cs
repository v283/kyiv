using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
    public partial class UserTestsModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private int result;
    }
}
