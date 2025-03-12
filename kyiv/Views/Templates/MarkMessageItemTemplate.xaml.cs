using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.Views.Templates;

public partial class MarkMessageItemTemplate : ContentView
{

    public MarkMessageItemTemplate()
    {
        InitializeComponent();
        BindingContext = this;
    }
 }