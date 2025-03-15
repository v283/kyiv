using CommunityToolkit.Maui.Core.Primitives;
using kyiv.Models;
using kyiv.Services;


namespace kyiv.Views;

public partial class TrueFalsePage : ContentPage
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

    public TrueFalsePage(string table)
    {
      
    

		InitializeComponent();
        wordsList = DbProvider.GetDictionaryWords(table);
        totalCount = wordsList.Count;
        Progress = 0;

    }

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
            word.Text = wordsList[Progress].Word;

            wordImage.Source = wordsList[Progress].Image;
            falseBtn.BackgroundColor = Color.FromArgb("#512BD4");
            trueBtn.BackgroundColor = Color.FromArgb("#512BD4");
            Random rand = new Random();
            int step = rand.Next(-1, 1);
            int wordIndex = Progress + step;
            if (wordIndex < 0)
            {
                wordIndex = 0;
            }
            else if (wordIndex >= totalCount)
            {
                wordIndex = totalCount - 1;
            }
            translate.Text = wordsList[wordIndex].Translation;



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

    public void OnTrue(object sender, EventArgs e)
    {
        if (trueBtn.BackgroundColor.ToString() == falseBtn.BackgroundColor.ToString())
        {
            if (translate.Text == wordsList[Progress].Translation)
            {
                correctDone++;
                trueBtn.BackgroundColor = Color.FromArgb("#2eaa63");//green
                wordsList[Progress].Word = wordsList[Progress].Word + "[{?";
            }
            else
            {
                trueBtn.BackgroundColor = Color.FromArgb("#e44e3f");//red
            }
        }

    }



    public void OnFalse(object sender, EventArgs e)
    {
        if (trueBtn.BackgroundColor.ToString() == falseBtn.BackgroundColor.ToString())
        {
            if (translate.Text != wordsList[Progress].Translation)
            {
                correctDone++;
                falseBtn.BackgroundColor = Color.FromArgb("#2eaa63");//green
                wordsList[Progress].Word = wordsList[Progress].Word + "[{?";
            }
            else
            {
                falseBtn.BackgroundColor = Color.FromArgb("#e44e3f");//red
            }
        }

    }

    public void OnNext(object sender, EventArgs e)
    {
        Progress += 1;
    }
    public void OnCheck(object sender, EventArgs e)
    {
        Grid obj = (Grid)sender;
        Label label = (Label)obj.Children[0];

        if (label.Text.Contains("[{?"))
        {
            label.Text = label.Text.Replace("[{?", "");
            obj.BackgroundColor = Color.FromArgb("#2eaa63");//green
        }
    }
}
