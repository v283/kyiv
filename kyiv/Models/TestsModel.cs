using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
    public partial class TestsModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string testName;

        [ObservableProperty]
        private int result;

        // Table name is equal as for explanation as for table with tests
        [ObservableProperty]
        private string tableTestName;

    }
}
