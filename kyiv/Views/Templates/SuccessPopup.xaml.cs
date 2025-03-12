using CommunityToolkit.Maui.Views;

namespace kyiv.Views.Templates;

public partial class SuccessPopup : Popup
{
    public SuccessPopup()
    {
        InitializeComponent();

        // ������ �������� ���������� �� Popup
        this.Content.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () => await ClosePopupAsync())
        });
    }

    // ����� ��� �������� Popup
    private async Task ClosePopupAsync()
    {
        await this.CloseAsync(); // ������� Popup
    }
}