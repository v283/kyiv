//using kyiv.Data;
using kyiv.ViewModels;
namespace kyiv.View;

public partial class ExercisePage : ContentPage
{
	public ExercisePage()
	{
        InitializeComponent();
        BindingContext = new ExerciseViewModel();
    }
}