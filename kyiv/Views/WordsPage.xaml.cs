using System.Windows.Input;
using ChatGPT;
using ChatGPT.Services;
using kyiv.Services;


using kyiv.Models;

namespace kyiv.Views;

public partial class WordsPage : ContentPage
{
    private string userLanguage = "";
    OpenAIService gpt;
    DictionaryModel folder;

    List<DictionaryWordsModel> wordsList = new();
    List<DictionaryWordsModel> wordsListFiltered = new();
    public WordsPage(DictionaryModel folder)
	{
		InitializeComponent();
        this.folder = folder;
        wordColection.ItemsSource = DbProvider.GetDictionaryWords(folder.DictRef);
        PageTitle.Text = folder.FolderName;
        gpt = new OpenAIService();
        wordsList = DbProvider.GetDictionaryWords(folder.DictRef);
        ShareDictionary.Update += LoadUI;
        BindingContext = this;

       

    }

    private void LoadUI(object sender , EventArgs args) {
        wordsListFiltered = new List<DictionaryWordsModel>(wordsList);
        wordColection.ItemsSource = wordsListFiltered;
       
    }

    private void OnReadWrite(object sender, TappedEventArgs args)
	{
		string temp = readWrite.Source.ToString().Replace("File: ","");
		if (temp == "book_read.png")
        {
			readWrite.Source = "pen_write.png";
			newWord.IsVisible = true;

        }
		else
		{
			readWrite.Source = "book_read.png";
            newWord.IsVisible = false;

        }

    }

    private void OnToolBarTapped(object sender, TappedEventArgs args)
    {
        if (toolBar.IsVisible == true) { toolBar.IsVisible = false; }
        else { toolBar.IsVisible = true; }
        
    }

    private async void OnImageLink(object sender, TappedEventArgs e)
    {
        var result = "";// await this.DisplayTextPromptAsync("Image URL", "Enter a link to the photo ");
        if (result.Contains("http"))
        {
            uploadedImage.Source = ImageSource.FromUri(new Uri(result));
        }

    }

    private async void SelectLanguage(object sender, TappedEventArgs e)
    {
        userLanguage = "English";// await this.DisplayRadioButtonPromptAsync("Select your language",

           // new[] { "English", "Français", "Deutsch", "Español", "中文", "日本語", "Italiano", "Português", "Українська", "Polski", "Nederlands", "Svenska", "Norsk", "Dansk", "Suomi", "Ελληνικά", "Türkçe", "한국어", "Tiếng Việt" });
    }

    public ICommand OnEntryCompleted => new Command( async() =>
    {
        if (userLanguage != "")
        {
            defEntry.Text = await gpt.AskQuestion($"Translate to {userLanguage} this word: {termEntry.Text}");
        }


    });

    private void OnAddWord(object sender, TappedEventArgs e)
    {
        DictionaryWordsModel word = new DictionaryWordsModel() {Image = uploadedImage.Source?.ToString().Replace("Uri: ", ""), Translation = defEntry.Text, Word = termEntry.Text };
        DbProvider.AddDictionaryWords(word, folder.DictRef);

        LoadUI(sender, e);
        defEntry.Text = "";
        termEntry.Text = "";
    }

    private void OnDeleteSwipeWordInvoked(object sender, EventArgs e)
    {
        var views = (SwipeItem)sender;
        var word = (DictionaryWordsModel)(((SwipeView)(views.Parent.Parent)).BindingContext);

        DbProvider.RemoveDictionaryWords(word, folder.DictRef);

        LoadUI(sender, e);
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


    private async void OnDeleteTable(object sender, TappedEventArgs e)
    {
        var result = await DisplayAlert("Delete", "This dictionary will be permanently deleted and cannot be recovered.", "Yes","No");
        if (result)
        {
            DbProvider.DeleteWordsTable(folder.DictRef, folder.Id);
        }

    }

    private void OnShareTable(object sender, TappedEventArgs e)
    {
        ShareDictionary.ShareWordsFile(DbProvider.GetDictionaryWords(folder.DictRef));
    }
    private void OnImportTable(object sender, TappedEventArgs e)
    {
        ShareDictionary.ImportWordsFile(folder.DictRef);
    }

    private async void OnStudySet(object sender, EventArgs e)
    {
        if (((List<DictionaryWordsModel>)wordColection.ItemsSource).Count > 0)
        {
            if (studySetBtn.Text != "Cancel")
            {
                studySetBtn.Text = "Cancel";
                studySet.IsVisible = true;
            }
            else
            {
                studySetBtn.Text = "Study this set";
                studySet.IsVisible = false;
            }
        }

    }
    int i = 0;
    private void OnSort(object sender, TappedEventArgs e)
    {
        if (i == 0)
        {
            wordsList.Sort();
            wordColection.ItemsSource = wordsList;
            i = 1;
        }
        else
        {
            wordColection.ItemsSource = DbProvider.GetDictionaryWords(folder.DictRef);
            i = 0;
        }

    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        //var searchTerm = e.NewTextValue;
        //wordsListFiltered = wordsList.Find(searchTerm);
        //wordColection.ItemsSource = wordsListFiltered;
    }


    private async void OnWritingPage(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new WritingPage(folder.DictRef));
    }
    private async void OnMissingLetterPage(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new MissingLetterPage(folder.DictRef));
    }
    private async void OnTrueFalsePage(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new TrueFalsePage(folder.DictRef));
    }
}
