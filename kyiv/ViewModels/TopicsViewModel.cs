using System;
using System.Text;
using System.Windows.Input;
using ChatGPT;
using ChatGPT.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kyiv.Constants;

//using kyiv.Data;
using kyiv.Model;
using kyiv.View;
using Mscc.GenerativeAI;

namespace kyiv.ViewModels
{
	public partial class TopicsViewModel : ObservableObject
	{
        private static int ttask = 0;
        [ObservableProperty]
        private string levelName;

        [ObservableProperty]
        public List<TopicModel> topics = new List<TopicModel>();

        static readonly HttpClient client = new HttpClient();

        [ObservableProperty]
        private string taskText = "Loading...";

        public TopicsViewModel(string level)
        {
            LevelName = level;
            LoadTaskCommand.Execute(null);
        }

        [RelayCommand]
        public async Task LoadTask()
        {

        }

        //private async void LoadDataUI(string level)
        //{
        //    Topics = await DbProvider.GetTopicsByLevel(level);

        //    foreach (var item in Topics)
        //    {
        //        if (item.IsDoneImage == "not_done_image.png" || item.IsDoneImage == "done_image.png") { }
        //        else
        //        {
        //            Topics.Remove(item);
        //        }
        //    }
        //}
        [RelayCommand]
        private async Task LevelSelected(string level)
        {
            string taskStr = "10";
            if (int.TryParse(taskStr, out int task))
            {
                if (ttask <= task)
                {
                    await Shell.Current.Navigation.PushAsync(new TopicsView(level));
                    ttask++;
                }
                else
                {
                    await Shell.Current.Navigation.PushAsync(new DonePage());
                    ttask = 0;
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

                await Shell.Current.Navigation.PushAsync(new ExplanationTestsPage(obj, LevelName));
            }
        }



        //}

        public ICommand GoBackCommand => new Command(async () =>
        {
            await Shell.Current.Navigation.PopAsync();

        });
    }
}

