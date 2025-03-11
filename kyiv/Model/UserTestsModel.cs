using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Model
{
    public partial class UserTestsModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private int result;
    }
}
