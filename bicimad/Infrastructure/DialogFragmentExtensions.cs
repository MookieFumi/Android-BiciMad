using Android.Graphics;
using Android.Views;

namespace bicimad.Infrastructure
{
    public static class DialogFragmentExtensions
    {
        public static void AdjustSize(this Android.Support.V4.App.DialogFragment dialogFragment,
                                      double widthPercentage = 0.90, double heightPercentage = 0.60)
        {
            Window window = dialogFragment.Dialog.Window;
            Point size = new Point();

            Display display = window.WindowManager.DefaultDisplay;
            display.GetSize(size);

            int width = size.X;
            int height = size.Y;

            window.SetLayout((int)(width * widthPercentage), (int)(height * heightPercentage));
            window.SetGravity(GravityFlags.Center);
        }
    }
}
