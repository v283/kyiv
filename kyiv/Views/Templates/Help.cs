using System;
namespace kyiv.Views.Templates
{
    public static class Help
    {

        public static async void MakeActive(Button button, VisualElement page)
        {
            button.BackgroundColor = Color.FromArgb("#8a2be2");//active
            button.TextColor = Color.FromArgb("#49008c");//active
            page.IsVisible = true;
        }

        public static async void MakeDisable(Button button, VisualElement page)
        {
            button.BackgroundColor = Color.FromArgb("#f8f3f6");//
            button.TextColor = Color.FromArgb("#aeaaad");//
            page.IsVisible = false;
        }
    }
}

