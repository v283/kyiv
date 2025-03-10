using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.Views.Templates;

public partial class MarkMessageItemTemplate : Grid
{

    public MarkMessageItemTemplate()
    {
        InitializeComponent();
        BindingContext = this;
        OpenInGoogleMapsCommand = new Command<string>(async (location) => await OpenInGoogleMaps(location));
    }
    public ICommand OpenInGoogleMapsCommand { get; }


    private async Task OpenInGoogleMaps(string location)
    {
        if (string.IsNullOrWhiteSpace(location)) return;

        string encodedAddress = Uri.EscapeDataString(location);
        string url = $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";

        await Launcher.OpenAsync(url);
    }

}