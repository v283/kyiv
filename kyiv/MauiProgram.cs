using ChatGPT.Services;
using ChatGPT.ViewModels;
using ChatGPT.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

