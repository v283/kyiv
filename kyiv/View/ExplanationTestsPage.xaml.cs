//using kyiv.Data;
using kyiv.Model;
using kyiv.Controls;
using System.Windows.Input;

using CommunityToolkit.Maui.Behaviors;


namespace kyiv.View;

public partial class ExplanationTestsPage : ContentPage
{
    private string level;
    private TopicModel topic;

    private string isKnowVisible;
    private string IsKnowVisible
    {
        get => isKnowVisible;
        set
        {
            if (value == "done_image.png")
            {
                knowBtn.IsVisible = false;
                notKnowBtn.IsVisible = true;

            }
            else
            {
                knowBtn.IsVisible = true;
                notKnowBtn.IsVisible = false;
            }
            topic.IsDoneImage = value;
            //DbProvider.userDatabase.Execute($"UPDATE {level} SET IsDoneImage = '{value}'  WHERE Id = {topic.Id} ");
            isKnowVisible = value;
        }
    }


    private List<TestModel> testList;
    private List<TestModel> TestList
    {
        get => testList;
        set { testList = value; }
    }
    private string[] userAnswers;

    private int progressProperty;
    private int ProgressProperty
    {
        get { return progressProperty; }
        set
        {
            testProgressBar.Done = value;
            progressProperty = value;
        }
    }

    public ExplanationTestsPage(TopicModel topic, string level)
    {
        InitializeComponent();

        // exolanation tab
        explanationWebView.Source = topic.TableTopicName;
        explTitle.Text = topic.TopicName;
        this.level = level;
        this.topic = topic;
        IsKnowVisible = topic.IsDoneImage;
        //back butthon shell
        BindingContext = this;

        InvalidateMeasure();
        var behavior = new IconTintColorBehavior();




        //explTitle.Behaviors.Add(behavior);
    }


    //general
    // tap bar in maui can't be used for specific page and it is at the bottom
    // in aditiionat tabview from another develp doesn't wok on ios
    private async void OnExplanationTabCliked(object sender, EventArgs e)
    {
        Help.MakeActive( explanationTabBtn, explanationTab);
        Help.MakeDisable( testsTabBtn, testsTab);
        Help.MakeDisable( examTabBtn, examTab);
    }
    private async void OnTestsTabCliked(object sender, EventArgs e)
    {
        //testsColection.ItemsSource = DbProvider.GetTestsByTopicAsync(topic.TableTopicName.ToString().Replace(".html", "")).Result;
        Help.MakeDisable( explanationTabBtn, explanationTab);
        Help.MakeActive( testsTabBtn, testsTab);
        Help.MakeDisable( examTabBtn, examTab);
    }
    private async void OnExamTabCliked(object sender, EventArgs e)
    {
        if (testList != null)
        {
            Help.MakeDisable( explanationTabBtn, explanationTab);
            Help.MakeDisable( testsTabBtn, testsTab);
            Help.MakeActive( examTabBtn, examTab);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        if (userAnswers != null && userAnswers[0] != null && !resultGrid.IsVisible)
        {
            PromptUser();
            return true;
        }
        else { return false; }

    }
    public ICommand GoBackCommand => new Command(async () =>
    {
        if (userAnswers != null && userAnswers[0] != null && !resultGrid.IsVisible)
        {
            PromptUser();
        }
        else
        {
            await Navigation.PopAsync();
        }
    });
    private async void PromptUser()
    {
        bool answer = await DisplayAlert("Oops", "Do you want to finish test?", "Yes", "No");
        if (answer)
        {
            await Navigation.PopAsync();
        }
    }


    // explanation tap
    private async void OnKnow(object sender, EventArgs e)
    {
        topic.IsDoneImage = IsKnowVisible = "done_image.png";

    }

    private async void OnNotKnow(object sender, EventArgs e) { topic.IsDoneImage = IsKnowVisible = "not_done_image.png"; }



    //tests tap

    private async void OnTestsSelectedChanged(object sender, EventArgs e)
    {

        //TestList = await DbProvider.GetTestByNameAsync(((TestsModel)testsColection.SelectedItem).TableTestName);

        OnExamTabCliked(sender, e);

        ProgressProperty = 0;

        //replace to Total
        testProgressBar.Total = TestList.Count;
        userAnswers = new string[TestList.Count];
        DefaultTestState();
        TestTaskUIUpdate();
    }

    //exam tap
    private async void DefaultTestState()
    {

        explanationGrid.IsVisible = false;
        question.IsVisible = true;
        answGrid.IsVisible = true;
        answEntry.TextColor = Color.FromArgb("#ffffff ");//black
        answEntry.Text = "";
        answEntry.IsVisible = false;

        answerBtn.IsVisible = false;
        resultGrid.IsVisible = false;
    }

    private async void TestTaskUIUpdate()
    {
        question.Text = TestList[ProgressProperty].Question;
        if (TestList[ProgressProperty].CorrectAnsw.Length <= 3)
        {
            answGrid.AnswA = TestList[ProgressProperty].AnswA;
            answGrid.AnswB = TestList[ProgressProperty].AnswB;
            answGrid.AnswC = TestList[ProgressProperty].AnswC;
            answGrid.AnswD = TestList[ProgressProperty].AnswD;
            answGrid.CorrectAnswer = TestList[ProgressProperty].CorrectAnsw;
            answGrid.UserAnswer = userAnswers[ProgressProperty];
            if (TestList[ProgressProperty].CorrectAnsw.Length == 1) { answGrid.SelectionMode = AnswerGridSelectionMode.Solo; }
            else { answGrid.SelectionMode = AnswerGridSelectionMode.Multiple; }

        }
        else
        {
            answEntry.IsVisible = true;
            answerBtn.IsVisible = true;

            if (userAnswers[ProgressProperty] != null)
            {
                answEntry.Text = userAnswers[ProgressProperty];

                if (TestList[ProgressProperty].CorrectAnsw.ToLower() == userAnswers[ProgressProperty].ToLower())
                {
                    answEntry.TextColor = Color.FromArgb("#2eaa63");//green
                }
                else
                {
                    answEntry.TextColor = Color.FromArgb("#e44e3f");//red
                }

                explanationGrid.IsVisible = true;
            }


        }
        explanation.Text = TestList[ProgressProperty].Explanation;


    }

    private void OnNextTask(object sender, TappedEventArgs args)
    {
        if (resultGrid.IsVisible) { }
        else if (ProgressProperty != TestList.Count - 1)
        {
            ProgressProperty++;
            DefaultTestState();
            TestTaskUIUpdate();
        }
        else
        {
            ProgressProperty++;
            DefaultTestState();
            question.IsVisible = false;
            answGrid.IsVisible = false;

            resultGrid.IsVisible = true;

            //replace
            double done = 0;
            double total = 0;
            TestPageLogic.CalculateResult(out done, out total, TestList, userAnswers, topic.TableTopicName.ToString().Replace(".html", ""), ((TestsModel)testsColection.SelectedItem).Id);
            resultProgressBar.Done = (int)done;
            resultProgressBar.Total = (int)total;

            if (((done / total) * 100) < 30) { resultPhrase.Text = "You can do better."; }
            else if ((((done / total) * 100) < 60)) { resultPhrase.Text = "Not bad!"; }
            else if ((((done / total) * 100) < 99)) { resultPhrase.Text = "You're doing great!"; }
            else { resultPhrase.Text = "Excellent!"; }


        }

    }

    private void OnPreviousTask(object sender, TappedEventArgs args)
    {
        if (ProgressProperty != 0)
        {
            ProgressProperty--;
            DefaultTestState();
            TestTaskUIUpdate();
        }

    }

    private async void OnAnswer(object sender, EventArgs args)
    {
        userAnswers[ProgressProperty] = answGrid.UserAnswer;
        explanationGrid.IsVisible = true;
    }

    private async void OnAnswerBtn(object sender, TappedEventArgs args)
    {
        explanationGrid.IsVisible = true;
    }

    public ICommand OnAnswEntryCompleted => new Command(async () =>
    {
        if (userAnswers[ProgressProperty] != null || userAnswers[ProgressProperty] != "" || userAnswers[ProgressProperty] != " ")
        {
            userAnswers[ProgressProperty] = answEntry.Text.Trim();
            if (TestList[ProgressProperty].CorrectAnsw.ToLower() == userAnswers[ProgressProperty].ToLower())
            {
                answEntry.TextColor = Color.FromArgb("#2eaa63");//green
            }
            else
            {
                answEntry.TextColor = Color.FromArgb("#e44e3f");//red
            }
            explanationGrid.IsVisible = true;
        }
    });

    private async void OnChatBot(object sender, TappedEventArgs args)
    {

        string temp = "";
        if (examTab.IsVisible && ProgressProperty != TestList.Count)
        {
            temp = $"Write an explanation for this question: {TestList[ProgressProperty].Question} " +
    $"\n{TestList[ProgressProperty].AnswA} \n{TestList[ProgressProperty].AnswB} \n{TestList[ProgressProperty].AnswC} \n{TestList[ProgressProperty].AnswD}\n" +
    $"Correct answer: {TestList[ProgressProperty].CorrectAnsw}";
        }

        await Shell.Current.GoToAsync($"jackbotpage?question={temp}");
        if (progressProperty % 2 == 0)
        {

        }

    }


}