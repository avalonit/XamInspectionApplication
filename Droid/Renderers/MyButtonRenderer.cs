using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using KobApplication.Controls;
using KobApplication.Droid.Renderers;
using Android.Util;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyButtonRenderer))]
namespace KobApplication.Droid.Renderers
{
    public class MyButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);


            if (this.Element is MyButton && Control != null)
            {
                MyButton myButton = this.Element as MyButton;
                this.Control.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;

				DisplayMetrics metrics = this.Resources.DisplayMetrics;
				var left = (int)((myButton.CustomPadding.Left) * Resources.DisplayMetrics.Density);
				var top = (int)((myButton.CustomPadding.Top) * Resources.DisplayMetrics.Density);
				var right = (int)((myButton.CustomPadding.Right) * Resources.DisplayMetrics.Density);
				var bottom = (int)((myButton.CustomPadding.Bottom) * Resources.DisplayMetrics.Density);

				Control.SetPadding(left, top, right, bottom);
            }
        }

    }
}