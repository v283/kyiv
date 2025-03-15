namespace kyiv.Views.Templates;

public partial class CustomBox : ContentView
{
	public CustomBox()
	{
		InitializeComponent();
	}

    public readonly static BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomBox));
    public readonly static BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomBox));
    public readonly static BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(CustomBox));
    public readonly static BindableProperty BoxBackgroundColorProperty = BindableProperty.Create(nameof(BoxBackgroundColor), typeof(Color), typeof(CustomBox));
    public readonly static BindableProperty LineLengthProperty = BindableProperty.Create(nameof(LineLength), typeof(float), typeof(CustomBox));
    public readonly static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(CustomBox));
    public readonly static BindableProperty IsDoneTextProperty = BindableProperty.Create(nameof(IsDoneText), typeof(string), typeof(CustomBox));


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

    public Color StrokeColor
    {
        get { return (Color)GetValue(StrokeColorProperty); }
        set { SetValue(StrokeColorProperty, value); }
    }

    public Color BoxBackgroundColor
    {
        get { return (Color)GetValue(BoxBackgroundColorProperty); }
        set { SetValue(BoxBackgroundColorProperty, value); }
    }

    public float LineLength
    {
        get { return (float)GetValue(LineLengthProperty); }
        set { SetValue(LineLengthProperty, value); }
    }

    public float CornerRadius
    {
        get { return (float)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public string IsDoneText
    {
        get { return (string)GetValue(IsDoneTextProperty); }
        set { SetValue(IsDoneTextProperty, value); }
    }


    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == SizeProperty.PropertyName)
        {
            HeightRequest = Size;
            WidthRequest = Size;
        }

        if (propertyName == IsDoneTextProperty.PropertyName)
        {
            if (IsDoneText == "done_image.png")
            {
                SetValue(BoxBackgroundColorProperty, Color.FromHex("#512BD4"));
            }
            else
            {
                SetValue(BoxBackgroundColorProperty, Colors.Transparent);
            }
        }

        this.InvalidateLayout(); //works on android


    }
}
