using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Models
{
   
	public partial class DictionaryWordsModel :  ObservableObject, IComparable<DictionaryWordsModel>
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string image;
        [ObservableProperty]
        private string word;
        [ObservableProperty]
        private string translation;

        public int CompareTo(DictionaryWordsModel other)
        {
            if (other == null)
                return 1;

            return string.Compare(word, other.word, StringComparison.OrdinalIgnoreCase);
        }
    }
}

