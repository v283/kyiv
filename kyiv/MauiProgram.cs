using kyiv.Services;
using ChatGPT.Services;
using ChatGPT.ViewModels;
using ChatGPT.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using kyiv.Views;
using kyiv.ViewModels;
using kyiv.Views.Templates;

namespace kyiv;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.Services.AddSingleton<IOpenAIService, OpenAIService>();

        //// Add ViewModels
        builder.Services.AddTransient<MarkViewModel>();

        builder.Services.AddSingleton<LoginPopup>();
        builder.Services.AddSingleton<SignupPopup>();
        //// Add Views


        builder.Services.AddSingleton<MainView>();

        builder.Services.AddTransient<MarkView>();
        builder.Services.AddSingleton<AccountView>();

        builder.Services.AddSingleton<UserSettingsView>();


        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();
        builder.Services.AddTransient<ConversationViewModel>();
        builder.Services.AddSingleton<ConversationView>();


        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();



        // deep link
        builder.Services.AddSingleton<AppShell>();


        var url = Constants.ApiKeys.SUPABASE_URL;
        var key = Constants.ApiKeys.SUPABASE_KEY;

           builder.Services.AddSingleton(provider => new Supabase.Client(url, key, new Supabase.SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true,
        }));


        //// Add ViewModels

        //// Add Views



        builder.Services.AddSingleton<AccountView>();

        builder.Services.AddSingleton<UserSettingsView>();

        builder.Services.AddSingleton<IDataService, DataService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

//=======
//using Microsoft.Extensions.Logging;
//using kyiv.Views;
//using kyiv.ViewModels;
//using kyiv.Views.Templates;

//namespace kyiv;

//public static class MauiProgram
//{
//	public static MauiApp CreateMauiApp()
//	{
//		var builder = MauiApp.CreateBuilder();
//		builder
//			.UseMauiApp<App>()
//			.UseMauiCommunityToolkit()
//            .ConfigureFonts(fonts =>
//			{
//				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//			})
//			.Services.AddSingleton<IOpenAIService, OpenAIService>();

//        //// Add ViewModels
//        builder.Services.AddTransient<MarkViewModel>();

//        builder.Services.AddSingleton<LoginPopup>();
//        builder.Services.AddSingleton<SignupPopup>();
//        //// Add Views
//        builder.Services.AddSingleton<DataService>();
//        builder.Services.AddSingleton<MarkViewModel>();
//        builder.Services.AddTransient<WriteCommentViewModel>();




//        builder.Services.AddSingleton<MainView>();

//        builder.Services.AddTransient<MarkView>();
//        builder.Services.AddSingleton<AccountView>();

//        builder.Services.AddSingleton<UserSettingsView>();


//        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();
//        builder.Services.AddTransient<ConversationViewModel>();
//        builder.Services.AddSingleton<ConversationView>();


//        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();

//        var url = Constants.ApiKeys.SUPABASE_URL;
//        var key = Constants.ApiKeys.SUPABASE_KEY;

//           builder.Services.AddSingleton(provider => new Supabase.Client(url, key, new Supabase.SupabaseOptions
//        {
//            AutoRefreshToken = true,
//            AutoConnectRealtime = true,
//        }));


//        //// Add ViewModels

//        //// Add Views

//        builder.Services.AddSingleton<WriteCommentViewModel>();
//        builder.Services.AddSingleton<WriteCommentView>();


//        builder.Services.AddSingleton<AccountView>();

//        builder.Services.AddSingleton<UserSettingsView>();

//        builder.Services.AddSingleton<IDataService, DataService>();

//#if DEBUG
//        builder.Logging.AddDebug();
//#endif

//		return builder.Build();
//	}
//}

//>>>>>>> Sikario


//=======
//using Microsoft.Extensions.Logging;
//using AndroidX.CardView.Widget;
//using kyiv.Views;
//using kyiv.ViewModels;
//using kyiv.Views.Templates;

//namespace kyiv;

//public static class MauiProgram
//{
//	public static MauiApp CreateMauiApp()
//	{
//		var builder = MauiApp.CreateBuilder();
//		builder
//			.UseMauiApp<App>()
//			.UseMauiCommunityToolkit()
//            .ConfigureFonts(fonts =>
//			{
//				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//			})
//			.Services.AddSingleton<IOpenAIService, OpenAIService>();

//        //// Add ViewModels
//        builder.Services.AddTransient<MarkViewModel>();

//        builder.Services.AddSingleton<LoginPopup>();
//        builder.Services.AddSingleton<SignupPopup>();
//        //// Add Views


//        builder.Services.AddSingleton<MainView>();

//        builder.Services.AddTransient<MarkView>();
//        builder.Services.AddSingleton<AccountView>();

//        builder.Services.AddSingleton<UserSettingsView>();


//        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();
//        builder.Services.AddTransient<ConversationViewModel>();
//        builder.Services.AddSingleton<ConversationView>();


//        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();

//        var url = Constants.ApiKeys.SUPABASE_URL;
//        var key = Constants.ApiKeys.SUPABASE_KEY;

//           builder.Services.AddSingleton(provider => new Supabase.Client(url, key, new Supabase.SupabaseOptions
//        {
//            AutoRefreshToken = true,
//            AutoConnectRealtime = true,
//        }));


//        //// Add ViewModels

//        //// Add Views



//        builder.Services.AddSingleton<AccountView>();

//        builder.Services.AddSingleton<UserSettingsView>();

//        builder.Services.AddSingleton<IDataService, DataService>();

//#if DEBUG
//        builder.Logging.AddDebug();
//#endif

//		return builder.Build();
//	}
//}

//>>>>>>> vannos-slid
