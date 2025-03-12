using CommunityToolkit.Maui.Views;

namespace kyiv.Views.Templates;

public partial class SuccessPopup : Popup
{
    public SuccessPopup()
    {
        InitializeComponent();

        // Додати обробник натискання на Popup
        this.Content.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () => await ClosePopupAsync())
        });
    }

    // Метод для закриття Popup
    private async Task ClosePopupAsync()
    {
        await this.CloseAsync(); // Закрити Popup
    }
}