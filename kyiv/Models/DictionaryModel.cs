using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
	public partial class DictionaryModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string folderImage;
        [ObservableProperty]
        private string folderName;
        [ObservableProperty]
        private string dictRef;
    }
}

