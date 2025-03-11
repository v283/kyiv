using System;
namespace kyiv.Controls
{
    public static class Help
    {

        public static async void MakeActive(Button button, VisualElement page)
        {
            button.BackgroundColor = Color.FromArgb("#c1e6f8");//active
            button.TextColor = Color.FromArgb("#03b3fd");//active
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

