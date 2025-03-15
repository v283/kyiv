using System.ComponentModel;


namespace kyiv.Views.Templates;

public partial class MyCircleProgressBar : ContentView
{
    // to solve bug
    private static int reloadNumb = 0; // i use this vriable because on ios I can't invalidate layout without changing size of view
    public MyCircleProgressBar()
    {
        InitializeComponent();
	}

    //public readonly static BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(MyCircleProgressBar));
    public readonly static BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(MyCircleProgressBar));
    public readonly static BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(MyCircleProgressBar));
    public readonly static BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(MyCircleProgressBar));
    public readonly static BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(MyCircleProgressBar));
    public readonly static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MyCircleProgressBar));
    public readonly static BindableProperty TotalProperty = BindableProperty.Create(nameof(Total), typeof(int), typeof(MyCircleProgressBar));
    public readonly static BindableProperty DoneProperty = BindableProperty.Create(nameof(Done), typeof(int), typeof(MyCircleProgressBar));
    public readonly static BindableProperty TextStyleProperty = BindableProperty.Create(nameof(TextStyle), typeof(CircleTextStyle), typeof(MyCircleProgressBar));
    public readonly static BindableProperty FontZoomProperty = BindableProperty.Create(nameof(FontZoom), typeof(double), typeof(MyCircleProgressBar));

    //public int Progress
    //{
    //    get { return (int)GetValue(ProgressProperty); }
    //    set{ SetValue(ProgressProperty, value);}
    //}

    public int Size
    {
        get { return (int)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }

    public int Thickness
    {
        get { return (int)GetValue(ThicknessProperty); }
        set { SetValue(ThicknessProperty, value); }
    }

    public Color ProgressColor
    {
        get { return (Color)GetValue(ProgressColorProperty); }
        set { SetValue(ProgressColorProperty, value); }
    }

    public Color ProgressLeftColor
    {
        get { return (Color)GetValue(ProgressLeftColorProperty); }
        set { SetValue(ProgressLeftColorProperty, value); }
    }

    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    public int Total
    {
        get { return (int)GetValue(TotalProperty); }
        set { SetValue(TotalProperty, value); }
    }
    public int Done
    {
        get { return (int)GetValue(DoneProperty); }
        set
        {        
            SetValue(DoneProperty, value);
            ProgressChange();
        }
    }
    public CircleTextStyle TextStyle
    {
        get { return (CircleTextStyle)GetValue(TextStyleProperty); }
        set { SetValue(TextStyleProperty, value); }
    }

    public double FontZoom
    {
        get { return (double)GetValue(FontZoomProperty); }
        set { SetValue(FontZoomProperty, value); }
    }

    // this is needed because on ios InvalidateLayout method doesn't work if i change progrees property in object in another class
    public void ProgressChange()
    {
        
        if (reloadNumb == 0)
        {
            HeightRequest -=0.000001;
            WidthRequest -= 0.000001;
            reloadNumb = 1;
        }
        else
        {
            HeightRequest += 0.000001;
            WidthRequest += 0.000001;
            reloadNumb = 0;
        }


    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == SizeProperty.PropertyName)
        {
            HeightRequest = Size;
            WidthRequest = Size;
        }

        this.InvalidateLayout(); //works on android
    }
}

