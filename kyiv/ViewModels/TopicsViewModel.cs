using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Services;
using kyiv.Models;
using kyiv.Views;

namespace kyiv.ViewModels
{
	public partial class TopicsViewModel : ObservableObject
	{
        [ObservableProperty]
        private string subjlName;

        [ObservableProperty]
        public List<TopicModel> topics = new List<TopicModel>();

        public TopicsViewModel(string subj)
		{
            subjlName = subj;
            if (subj == "engtable")
            {
                LoadDataUI("beginner");
            }


        }

        private async void LoadDataUI(string level)
        {
            Topics = await DbProvider.GetTopicsByLevel(level);

            foreach (var item in Topics)
            {
                if (item.IsDoneImage == "not_done_image.png" || item.IsDoneImage == "done_image.png") { }
                else
                {
                    Topics.Remove(item);
                }
            }
        }


        [RelayCommand]
        public async void TopicSelected(TopicModel obj)
        {

            if (obj.TableTopicName == "" || obj.TableTopicName == null)
            {
                await Shell.Current.DisplayAlert("Sorry", "This topic isn't available right now.", " Cancel");
            }
            else
            {
                await Shell.Current.Navigation.PushAsync(new ExplanationTestsPage(obj, "beginner"));
            }



        }

        public ICommand GoBackCommand => new Command(async () =>
        {
            await Shell.Current.Navigation.PopAsync();

        });
    }
}

