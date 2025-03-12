using ChatGPT.Views;

namespace kyiv;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("chatgpt", typeof(ConversationView));
    }

    protected override async void OnNavigating(ShellNavigatingEventArgs args)
    {
        if (args.Current != null && args.Current.ToString().Contains("avocado://"))
        {
            string route = args.Current.ToString().Replace("avocado://mobile/", "");
            await Shell.Current.GoToAsync(route);
        }
    }

}

