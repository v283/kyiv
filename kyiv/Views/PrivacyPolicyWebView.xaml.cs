using System.Net.Http;
using Microsoft.Maui.Controls;

namespace kyiv.Views;

public partial class PrivacyPolicyPage : ContentPage
{
	private const string SupabaseFileUrl = "https://gqowltjejkvnonrcdkwt.supabase.co/storage/v1/object/public/documents//index2.html";

    public PrivacyPolicyPage()
	{
		InitializeComponent();

		//LoadCustomWeb();

        LoadHtmlFromSupabase();

	}

	private async void LoadHtmlFromSupabase()
	{
		try
		{
			using (var httpClient = new HttpClient())
			{
				var htmlContent = await httpClient.GetStringAsync(SupabaseFileUrl);

				var htmlSource = new HtmlWebViewSource { Html = htmlContent };
				PrivacyPolicyWebView.Source = htmlSource;
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("�������", "�� ������� ����������� �������.", "��");
		}
	}

    private async void LoadCustomWeb()
    {
        try
        {
			PrivacyPolicyWebView.Source = new UrlWebViewSource { Url = SupabaseFileUrl };
        }
        catch (Exception ex)
        {
            await DisplayAlert("�������", $"�� ������� ����������� �������. �����: {ex.Message}", "��");
        }
    }
}