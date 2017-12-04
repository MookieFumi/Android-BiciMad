using Android.Support.V4.View;
using Android.Views;

namespace bicimad.Infrastructure.Transformers
{
    internal class ScaleTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        private float MinScale = 0.5f; // Minimum scale value.

        public void TransformPage(View view, float position)
        {
            if (position < -1 || position > 1)
            {
                view.Alpha = 0; // The view is offscreen.
            }
            else
            {
                view.Alpha = 1;

                // Scale the view.
                var scale = 1 - System.Math.Abs(position) * (1 - MinScale);
                view.ScaleX = scale;
                view.ScaleY = scale;

                // Set the X Translation to keep the views close together.
                var xMargin = view.Width * (1 - scale) / 2;

                if (position < 0)
                {
                    view.TranslationX = xMargin / 2;
                }
                else
                {
                    view.TranslationX = -xMargin / 2;
                }
            }
        }
    }
}