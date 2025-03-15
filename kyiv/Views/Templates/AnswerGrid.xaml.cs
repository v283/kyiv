
using Microsoft.Maui.Controls;

namespace kyiv.Views.Templates;

public partial class AnswerGrid : ContentView
{
    public delegate void ResultHandler();
    public static readonly BindableProperty NotifyProperty = BindableProperty.Create(nameof(Notify), typeof(ResultHandler), typeof(AnswerGrid));
    public event EventHandler Notify;

    public static readonly BindableProperty AnswAProperty = BindableProperty.Create(nameof(AnswA), typeof(string), typeof(AnswerGrid));
    public static readonly BindableProperty AnswBProperty = BindableProperty.Create(nameof(AnswB), typeof(string), typeof(AnswerGrid));
    public static readonly BindableProperty AnswCProperty = BindableProperty.Create(nameof(AnswC), typeof(string), typeof(AnswerGrid));
    public static readonly BindableProperty AnswDProperty = BindableProperty.Create(nameof(AnswD), typeof(string), typeof(AnswerGrid));

    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(AnswerGridSelectionMode), typeof(AnswerGrid));
    public static readonly BindableProperty UserAnswerProperty = BindableProperty.Create(nameof(UserAnswer), typeof(string), typeof(AnswerGrid));
    public static readonly BindableProperty CorrectAnswerProperty = BindableProperty.Create(nameof(CorrectAnswer), typeof(string), typeof(AnswerGrid));


    public string AnswA
    {
        get => (string)GetValue(AnswAProperty);
        set
        {
            if (value == null || value == "" || value == " ") { fAnswA.IsVisible = false; }
            else { fAnswA.IsVisible = true; }
            SetValue(AnswAProperty, value);
        }
    }
    public string AnswB
    {
        get => (string)GetValue(AnswBProperty);
        set
        {
            if (value == null || value == "" || value == " ") { fAnswB.IsVisible = false; }
            else { fAnswB.IsVisible = true; }
            SetValue(AnswBProperty, value);
        }
    }
    public string AnswC
    {
        get => (string)GetValue(AnswCProperty);
        set
        {
            if (value == null || value == "" || value == " ") { fAnswC.IsVisible = false; }
            else { fAnswC.IsVisible = true; }
            SetValue(AnswCProperty, value);
        }
    }
    public string AnswD
    {
        get => (string)GetValue(AnswDProperty);
        set
        {
            if (value == null || value == "" || value == " ") { fAnswD.IsVisible = false; }
            else { fAnswD.IsVisible = true; }
            SetValue(AnswDProperty, value);
        }
    }

    public AnswerGridSelectionMode SelectionMode
    {
        get => (AnswerGridSelectionMode)GetValue(SelectionModeProperty);
        set
        {
            SetValue(SelectionModeProperty, value);
            if (value == AnswerGridSelectionMode.Multiple) { answerBtn.IsVisible = true; }
            else { answerBtn.IsVisible = false; }
        }
    }
    public string UserAnswer
    {
        get => (string)GetValue(UserAnswerProperty);
        set
        {
            SetValue(UserAnswerProperty, value);

            if (value == null || value == "" || value == " ")
            {
                NoAnswer();
            }
            else
            {
                PutUserAnswers();
                CheckAnswers();
            }

        }
    }
    public string CorrectAnswer
    {
        get => (string)GetValue(CorrectAnswerProperty);
        set { SetValue(CorrectAnswerProperty, value); }
    }

    public AnswerGrid()
    {
        InitializeComponent();
    }

    private void NoAnswer()
    {
        List<IView> views = answGrid.Children.ToList();
        for (int i = 0; i < views.Count - 1; i++)
        {
            Border b = (Border)views[i];
            Label l = b.Content as Label;
            l.TextColor = Color.FromArgb("#ffffff ");//black
            b.Stroke = Color.FromArgb("#d0ccdc"); //grey

        }
    }

    private void PutUserAnswers()
    {
        List<IView> views = answGrid.Children.ToList();

        for (int i = 0; i < views.Count - 1; i++)
        {
            Border b = (Border)views[i];
            Label l = b.Content as Label;
            if (l.Text != null && l.Text != "" && l.Text != " ")
            {
                if (UserAnswer.Contains(l.Text[0]))
                {
                    b.Stroke = Color.FromArgb("#e89d28"); //yellow;
                    break;
                }
            }
            l.TextColor = Color.FromArgb("#ffffff ");//black
            b.Stroke = Color.FromArgb("#d0ccdc"); //grey
        }
    }

    private void CheckAnswers()
    {
        List<IView> views = answGrid.Children.ToList();

        for (int i = 0; i < views.Count - 1; i++)
        {
            Border b = (Border)views[i];
            Label l = b.Content as Label;
            if (l.Text != null && l.Text != "" && l.Text != " ")
            {
                string selectionColor = "";
                if (((Border)l.Parent).Stroke is SolidColorBrush colorBrush)
                {
                    selectionColor = colorBrush.Color.ToArgbHex().ToLower();
                }

                if (CorrectAnswer.ToUpper().Contains(l.Text[0]))
                {
                    l.TextColor = Color.FromArgb("#2eaa63");//green
                }
                if (selectionColor == "#e89d28")
                {
                    if (!CorrectAnswer.ToUpper().Contains(l.Text[0]))
                    {
                        l.TextColor = Color.FromArgb("#e44e3f");//red
                    }
                }
            }

        }
    }

    private void OnAnswerGesture(object sender, TappedEventArgs args)
    {
        Label label = (Label)sender;
        if (SelectionMode == AnswerGridSelectionMode.Solo && (UserAnswer == null || UserAnswer == "" || UserAnswer == " "))
        {
            UserAnswer += label.Text[0];
            Notify?.Invoke(this, args);
        }
        else if (SelectionMode == AnswerGridSelectionMode.Multiple && (UserAnswer == null || UserAnswer == "" || UserAnswer == " "))
        {

            string selectionColor = "";
            if (((Border)label.Parent).Stroke is SolidColorBrush colorBrush) { selectionColor = colorBrush.Color.ToArgbHex().ToLower(); }   

            if (selectionColor == "#e89d28") 
            {
                ((Border)label.Parent).Stroke = Color.FromArgb("#d0ccdc"); //grey
            }
            else
            {
                ((Border)label.Parent).Stroke = Color.FromArgb("#e89d28"); //yellow
            }
        }
    }

    private void OnAnswerBtn(object sender, TappedEventArgs args)
    {
        string temp = "";
        List<IView> views = answGrid.Children.ToList();
        for (int i = 0; i < views.Count - 1; i++)
        {
            Border b = (Border)views[i];
            Label l = b.Content as Label;
            if (l.Text != null && l.Text != "" && l.Text != " ")
            {
                string selectionColor = "";
                if (((Border)l.Parent).Stroke is SolidColorBrush colorBrush) { selectionColor = colorBrush.Color.ToArgbHex().ToLower(); }

                if (selectionColor == "#e89d28")
                {
                    temp += l.Text[0];
                }
            }
        }
        UserAnswer = temp;
        Notify?.Invoke(this, args);
    }
}