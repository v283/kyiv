//using AndroidX.CardView.Widget;
using Microsoft.Maui;

namespace kyiv.View.Templates;

public partial class PageTitleView : ContentView
{

    public static readonly BindableProperty TitleBackgroundColorProperty = BindableProperty.Create(nameof(TitleBackgroundColor), typeof(Brush), typeof(PageTitleView), Brush.Transparent);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PageTitleView), Color.FromArgb("#CADFFF"));
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(PageTitleView), string.Empty);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(PageTitleView), 25.0);

    public Brush TitleBackgroundColor
    {
        get => (Brush)GetValue(TitleBackgroundColorProperty);
        set => SetValue(TitleBackgroundColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public PageTitleView()
	{
		//InitializeComponent();
	}

    private async void OpenFlyoutAsync(object sender, TappedEventArgs args)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}
