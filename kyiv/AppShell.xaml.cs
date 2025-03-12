using ChatGPT.Views;

namespace kyiv;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("chatgpt", typeof(ConversationView));
    }


}

