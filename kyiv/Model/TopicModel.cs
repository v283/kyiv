using CommunityToolkit.Mvvm.ComponentModel;

namespace kyiv.Model
{
    public partial class TopicModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string topicName;
        [ObservableProperty]
        private string isDoneImage;
        [ObservableProperty]
        private string tableTopicName;

    }
}
