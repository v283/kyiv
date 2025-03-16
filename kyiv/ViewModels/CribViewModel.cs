using System;
using System.Net.Http;
using System.Reactive.Linq;
using kyiv.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace kyiv.ViewModels
{
    public partial class CribViewModel : ObservableObject
    {
        [ObservableProperty]
        private string htmlSource;

        [RelayCommand]
        private async void ChooseCrip(string crip)
        {
            if (crip == "irregularverbs")
            {
                await Shell.Current.Navigation.PushAsync(new CripDetail("IrregularVerbs.html"));

            }
            else if (crip == "theorymeth")
            {

                await Shell.Current.Navigation.PushAsync(new CripDetail("TheoryMath.html"));

            }
            else if (crip == "theoryukr")
            {
                await Shell.Current.Navigation.PushAsync(new CripDetail("TheoryUkr.html"));

            }
            else if (crip == "theoryhist")
            {
                await Shell.Current.Navigation.PushAsync(new CripDetail("KeyDates.html"));

            }

        }
    }
}

