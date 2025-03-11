using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;

namespace kyiv.Views.Templates;

public partial class RaitingStarTemplate : ContentView
{
 //   List<Image> stars;
	public RaitingStarTemplate()
	{
		InitializeComponent();
        BindingContext = this;
      //  Loaded += (s, e) => stars = starStack.Children.OfType<Image>().ToList();
    }
    public static readonly BindableProperty CountProperty = BindableProperty.Create(
        nameof(Count),
        typeof(int),
        typeof(RaitingStarTemplate),
        propertyChanged: OnCountChanged);

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public static readonly BindableProperty IsTappedProperty = BindableProperty.Create( nameof(IsTapped), typeof(bool), typeof(RaitingStarTemplate),
    propertyChanged: OnCountChanged);

    public bool IsTapped
    {
        get => (bool)GetValue(IsTappedProperty);
        set => SetValue(IsTappedProperty, value);
    }

    private static void OnCountChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RaitingStarTemplate ratingStarTemplate && newValue is int count)
        {
            ratingStarTemplate.UpdateStars(count);
        }
    }

    //private void UpdateStars(int count)
    //{
    //    if (stars == null || stars.Count == 0) return;

    //    int i = 0;
    //    for (; i < count; i++)
    //    {
    //        stars[i].Source = "star.png";
    //    }
    //    for (; i < 5; i++)
    //    {
    //        stars[i].Source = "star_grey.png";
    //    }
    //}

    private void UpdateStars(int count)
    {
        starStack.Clear();
        int i = 0;
        for (; i < count; i++)
        {
            starStack.Add(new Image() { HeightRequest = 22, WidthRequest = 22, Source = "star.png" });

        }
        for (; i < 5; i++)
        {
            starStack.Add(new Image() { HeightRequest = 22, WidthRequest = 22, Source = "star_grey.png"});
        }
    }

    [RelayCommand]
    private void StarTap(object number)
    {
        if (!IsTapped) return;
        Count = Convert.ToInt32(number);

    }
}
