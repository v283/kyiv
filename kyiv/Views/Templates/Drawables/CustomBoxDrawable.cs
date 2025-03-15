using System;
namespace kyiv.Views.Templates.Drawables
{

    public class CustomBoxDrawable : BindableObject, IDrawable
    {
        public readonly static BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CustomBoxDrawable));
        public readonly static BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CustomBoxDrawable));
        public readonly static BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(CustomBoxDrawable));
        public readonly static BindableProperty BoxBackgroundColorProperty = BindableProperty.Create(nameof(BoxBackgroundColor), typeof(Color), typeof(CustomBoxDrawable));
        public readonly static BindableProperty LineLengthProperty = BindableProperty.Create(nameof(LineLength), typeof(float), typeof(CustomBoxDrawable));
        public readonly static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(CustomBoxDrawable));
        public readonly static BindableProperty IsDoneTextProperty = BindableProperty.Create(nameof(IsDoneText), typeof(string), typeof(CustomBoxDrawable));


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
            set
            {
                SetValue(BoxBackgroundColorProperty, value);
            }
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

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = BoxBackgroundColor;
            canvas.StrokeColor = StrokeColor;
            canvas.StrokeSize = Thickness;

            float centerX = dirtyRect.Width / 2;
            float centerY = dirtyRect.Height / 2;
            float size = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2; // Size of the square
            float halfSize = size / 2;


            // Draw rounded square
            canvas.FillRoundedRectangle(
                centerX - halfSize,
                centerY - halfSize,
                size,
                size,
                CornerRadius
            );
            canvas.DrawRoundedRectangle(
    centerX - halfSize,
    centerY - halfSize,
    size,
    size,
    CornerRadius
);

            // Draw line from the center to the bottom
            //canvas.DrawLine(centerX, centerY + dirtyRect.Height/4, centerX, dirtyRect.Bottom);


            canvas.DrawLine(centerX, centerY + dirtyRect.Height / 4, centerX, centerY + dirtyRect.Height / 4 + LineLength);


            canvas.DrawLine(centerX, centerY - dirtyRect.Height / 4, centerX, centerY + dirtyRect.Height / 4 - LineLength);
        }

    }
}

