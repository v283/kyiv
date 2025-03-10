using ChatGPT.Views;
using System.Windows.Input;
using kyiv.View;

namespace kyiv;

public partial class AppShell : Shell
{
    //public static ViewModels.ShellViewModel ShellViewModelCommands { get; private set; }
    public ICommand GotoWebSiteCommand { get; }

    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("chatgpt", typeof(ConversationView));
        Routing.RegisterRoute("MainPage", typeof(kyiv.View.MainPage));
        CurrentItem = new ShellContent { Content = new MainPage() };
        GotoWebSiteCommand = new Command<string>(async (url) => await GoToSite(url));

        BindingContext = this;

    }

    private void OnCloseFlyout(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = false;
    }


    private async Task GoToSite(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return;
        }

        try
        {
            var options = new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Colors.Violet,
                PreferredControlColor = Colors.SandyBrown
            };
            await Browser.Default.OpenAsync(url, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening browser: {ex.Message}");
        }

    }
}

