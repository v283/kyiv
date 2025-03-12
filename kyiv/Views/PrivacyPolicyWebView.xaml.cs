using System.Net.Http;
using Microsoft.Maui.Controls;

namespace kyiv.Views;

public partial class PrivacyPolicyPage : ContentPage
{
	private const string SupabaseFileUrl = "https://gqowltjejkvnonrcdkwt.supabase.co/storage/v1/object/public/documents//index2.html";

    public PrivacyPolicyPage()
	{
		InitializeComponent();

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
			await DisplayAlert("Помилка", "Не вдалося завантажити сторінку.", "ОК");
		}
	}
}