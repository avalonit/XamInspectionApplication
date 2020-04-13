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

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace KobApplication.Droid.Renderers
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Element is MyEntry && Control != null)
            {
                if (Control == null)
                    return;
                MyEntry myEntry = this.Element as MyEntry;
                if (myEntry.BackgroundColor != Color.Transparent)
                {
                    this.Control.SetBackgroundColor(myEntry.CustomBackgroundColor.ToAndroid());
                }
                Control.SetBackgroundResource(Resource.Drawable.RoundedCornerEntry);

				DisplayMetrics metrics = this.Resources.DisplayMetrics;
				var left = (int)((myEntry.CustomPadding.Left) * Resources.DisplayMetrics.Density);
				var top = (int)((myEntry.CustomPadding.Top) * Resources.DisplayMetrics.Density);
				var right = (int)((myEntry.CustomPadding.Right) * Resources.DisplayMetrics.Density);
				var bottom = (int)((myEntry.CustomPadding.Bottom) * Resources.DisplayMetrics.Density);

                Control.SetPadding(left, top, right, bottom);
            }
        }
    }
}