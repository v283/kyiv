
using kyiv.Models;
using kyiv.Services;

namespace kyiv.Views;

public partial class WritingPage : ContentPage
{

    List<DictionaryWordsModel> wordsList;
    private int correctDone = 0;
    private int totalCount = 0;
    private int progress;
    public int Progress
    {
        get => progress;
        set
        {
            progress = value;
            progressBar.Progress = (double)value / totalCount;
            UpdateUI();
        }
    }

    public WritingPage(string table)
    {
        InitializeComponent();
        wordsList = DbProvider.GetDictionaryWords(table);
        totalCount = wordsList.Count;
        Progress = 0;    }

    private async void OnPlaySound(object sender, TappedEventArgs e)
    {
        var views = (Image)sender;
        string word = ((Label)(((Grid)views.Parent).Children[0])).Text;

        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

        // Find the locale corresponding to English - United States
        Locale localeUSA = locales.FirstOrDefault(locale =>
            locale.Language == "en" && locale.Country == "US");

        if (localeUSA == null) { return; }

        SpeechOptions options = new SpeechOptions()
        {
            Pitch = 1f,   // 0.0 - 2.0
            Volume = 1f, // 0.0 - 1.0
            Locale = localeUSA

        };

        await TextToSpeech.Default.SpeakAsync(word, options);
    }

    public void UpdateUI()
    {
        if (progress < totalCount)
        {
            translate.Text = wordsList[Progress].Translation;
            wordImage.Source = wordsList[Progress].Image;
            word.Text = "Correct word: " + wordsList[Progress].Word;
            word.IsVisible = false;

            userAnsw.Text = "";
            userAnsw.TextColor = Color.FromArgb("#ffffff ");//black
            nextSkip.Text = "Don'tknow";
        }
        else
        {
            examGrid.IsVisible = false;
            resultStack.IsVisible = true;
            resultProgressBar.Done = correctDone;
            resultProgressBar.Total = totalCount;
            if (((correctDone / totalCount) * 100) < 30) { resultPhrase.Text = "You can do better."; }
            else if ((((correctDone / totalCount) * 100) < 60)) { resultPhrase.Text = "Not bad!"; }
            else if ((((correctDone / totalCount) * 100) < 99)) { resultPhrase.Text = "You're doing great!"; }
            else { resultPhrase.Text = "Excellent!"; }

            wordColection.ItemsSource = wordsList;
        }

    }


    public void OnComplete(object sender, EventArgs e)
    {
        if (nextSkip.Text != "Next")
        {
            nextSkip.Text = "Next";
            if (CheckWords(userAnsw.Text.ToLower(), wordsList[Progress].Word.ToLower()))
            {
                userAnsw.TextColor = Color.FromArgb("#2eaa63");//green
                correctDone += 1;
            }
            else
            {
                userAnsw.TextColor = Color.FromArgb("#e44e3f");//red
                word.IsVisible = true;

                wordsList[Progress].Word = wordsList[Progress].Word + "[{?";
            }
        }      
    }
    public void OnNext(object sender, TappedEventArgs e)
    {
        if (nextSkip.Text == "Next")
        {
            Progress += 1;
        }
        else
        {
            nextSkip.Text = "Next";
            word.IsVisible = true;
            wordsList[Progress].Word = wordsList[Progress].Word + "[{?";
        }

    }

    public void OnCheck(object sender, EventArgs e)
    {
        Grid obj = (Grid)sender;
        Label label = (Label)obj.Children[0];

        if (label.Text.Contains("[{?"))
        {
            label.Text = label.Text.Replace("[{?","");
            obj.BackgroundColor = Color.FromArgb("#e44e3f");//red
        }
    }

    public static bool CheckWords(string word, string correctWord)
    {

        if (word.Length != correctWord.Length)
        {
            return false;
        }


        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] != correctWord[i])
            {
                return false; 
            }
        }

        return true;
    }
}
