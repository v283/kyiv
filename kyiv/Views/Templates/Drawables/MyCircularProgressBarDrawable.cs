using System;

namespace kyiv.Views.Templates.Drawables
{
    public class MyCircularProgressBarDrawable : BindableObject, IDrawable
    {

        //public readonly static  BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(int), typeof(MyCircularProgressBarDrawable));
        public readonly static  BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(MyCircularProgressBarDrawable));
        public readonly static  BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty TotalProperty = BindableProperty.Create(nameof(Total), typeof(int), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty DoneProperty = BindableProperty.Create(nameof(Done), typeof(int), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty TextStyleProperty = BindableProperty.Create(nameof(TextStyle), typeof(CircleTextStyle), typeof(MyCircularProgressBarDrawable));
        public readonly static BindableProperty FontZoomProperty = BindableProperty.Create(nameof(FontZoom), typeof(double), typeof(MyCircularProgressBarDrawable));


        //public int Progress
        //{
        //    get { return (int)GetValue(ProgressProperty); }
        //    set { SetValue(ProgressProperty, value); }
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
            set { SetValue(DoneProperty, value); }
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



        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float effectiveSize = Size - Thickness;
            float x = Thickness / 2;
            float y = Thickness / 2;

            if ((100 * Done / Total) > 100)
            {
                Done = Total;
            }

            if ((100 * Done / Total) < 100)
            {
                float angle = GetAngle(100*Done/Total);

                canvas.StrokeColor = ProgressLeftColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);

                // Draw arc
                canvas.StrokeColor = ProgressColor;
                canvas.StrokeSize = Thickness;
               
                canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
            }
            else
            {
                // Draw circle
                canvas.StrokeColor = ProgressColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);
            }

            // Make the percentage always the same size in relation to the size of the progress bar
            float fontSize = (effectiveSize / 2.9f) * (float)FontZoom ;
            canvas.FontSize = fontSize;
            canvas.FontColor = TextColor;

            // Vertical text align the text, and we need a correction factor of around 1.15 to have it aligned properly
            // Note: The VerticalAlignment.Center property of the DrawString method seems to have no effect
            float verticalPosition = ((Size / 2) - (fontSize / 2)) * 1.15f;

            if (TextStyle == CircleTextStyle.Percent)
            {
                canvas.DrawString($"{100*Done / Total}%", 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center, textFlow: TextFlow.OverflowBounds);
            }
            else if (TextStyle == CircleTextStyle.DoneTotal)
            {
                canvas.DrawString($"{Done}/{Total}", 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center, textFlow: TextFlow.OverflowBounds);

            }
            else
            {
                canvas.DrawString($"{Done}", 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center, textFlow: TextFlow.OverflowBounds);

            }


        }

        private float GetAngle(int progress)
        {
            float factor = 90f / 25f;

            if (progress > 75)
            {
                return -180 - ((progress - 75) * factor);
            }
            else if (progress > 50)
            {
                return -90 - ((progress - 50) * factor);
            }
            else if (progress > 25)
            {
                return 0 - ((progress - 25) * factor);
            }
            else
            {
                return 90 - (progress * factor);
            }
        }

    }
}

