using CommunityToolkit.Mvvm.Input;
using kyiv.Data;
using kyiv.ViewModels;

namespace kyiv.Views;

public partial class MainView : ContentPage
{

	public MainView()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();

        //btnUkr.Clicked += async (s, e) => Shell.Current.GoToAsync("subjecttopics?subjectName=Ukr");
        //btnMath.Clicked += async (s, e) => Shell.Current.GoToAsync("subjecttopics?subjectName=Math");
        //btnHistory.Clicked += async (s, e) => Shell.Current.GoToAsync("subjecttopics?subjectName=History");
        //btnNMT.Clicked += async (s, e) => Shell.Current.GoToAsync("subjecttopics?subjectName=NMT");



        //StorageWritePermission();
        //StorageReadPermission();


    }

    //private void Connect()
    //{
    //    //DbProvider.ConnctToDb();
    //    //var time = new TimeAccountant();
    //    //Settings.MainFontSize = DbProvider.GetProjectSettings()[0].MainFontSize;
    //    //List<QuoteModel> quotes = DbProvider.GetGuotesFromDbTable("Quotes");
    //    //Random random = new Random();
    //    //quoteLabel.Text = quotes[random.Next(0, quotes.Count)].Quote;
    //}
    //async Task StorageWritePermission()
    //{
    //    var status = PermissionStatus.Unknown;
    //    status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

    //    if (status == PermissionStatus.Granted)
    //    {
    //        Connect();
    //        return;
    //    }

    //    if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
    //    {
    //        await Shell.Current.DisplayAlert("Дозволити зберігати ваші дані тестувань?", "Це потрібно для нормального функціонування додатку", "Так", "Ні");
    //    }

    //    status = await Permissions.RequestAsync<Permissions.StorageWrite>();

    //    DbProvider.FirstRunDb();
    //    Connect();


    //}
    //async Task StorageReadPermission()
    //{
    //    var status = PermissionStatus.Unknown;
    //    status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

    //    if (status == PermissionStatus.Granted)
    //    {
    //        return;
    //    }

    //    if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
    //    {
    //        await Shell.Current.DisplayAlert("Дозволити зчитувати дані тестувань?", "Це потрібно для нормального функціонування додатку", "Так", "Ні");
    //    }

    //    status = await Permissions.RequestAsync<Permissions.StorageRead>();



    //}


}


