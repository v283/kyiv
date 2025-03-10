using kyiv.Services;
using ChatGPT.Services;
using ChatGPT.ViewModels;
using ChatGPT.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using AndroidX.CardView.Widget;
using kyiv.Views;

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

		builder.Services.AddTransient<ConversationViewModel>();
        builder.Services.AddSingleton<ConversationView>(); ;

        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();

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

