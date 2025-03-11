using UraniumUI;
using UraniumUI.Dialogs.Mopups;
namespace kyiv.Controls;

public partial class NewWord : ContentView
{
    public NewWord()
    {
        InitializeComponent();


    }


    private async void OnImageLink(object sender, TappedEventArgs e)
    {
        //var result = await this.DisplayTextPromptAsync("Image URL", "Enter a link to the photo ", placeholder: "My words");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //var result = await this.DisplayCheckBoxPromptAsync("Title", new[] { "Option 1", "Option 2", "Option 3" });
    }
}
