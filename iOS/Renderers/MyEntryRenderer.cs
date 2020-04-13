using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using KobApplication.Controls;
using Xamarin.Forms.Platform.iOS;
using KobApplication.iOS.Renderers;
using CoreAnimation;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace KobApplication.iOS.Renderers
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Element is MyEntry && Control != null)
            {
                MyEntry myEntry = this.Element as MyEntry;
                if (myEntry.BackgroundColor != Color.Transparent)
                {
					this.Control.BackgroundColor = myEntry.CustomBackgroundColor.ToUIColor();
                }

				Control.SizeToFit();
				Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;
				Control.BackgroundColor = Color.Transparent.ToUIColor();
				Control.BorderStyle = UITextBorderStyle.RoundedRect;
				if (Control.Layer != null)
				{
					Control.Layer.CornerRadius = 0;
					Control.Layer.BorderColor = Color.Transparent.ToCGColor();
					DrawBorder();
				}

				//this.Control.LeftView = new UIView();

                //Control.SetBackgroundResource(Resource.Drawable.RoundedCornerEntry);
                //this.Control.ContentEdgeInsets = new UIKit.UIEdgeInsets((int)myEntry.CustomPadding.Left, (int)myEntry.CustomPadding.Top, (int)myEntry.CustomPadding.Right, (int)myEntry.CustomPadding.Bottom);
                //Control.SetPadding((int)myEntry.CustomPadding.Left, (int)myEntry.CustomPadding.Top, (int)myEntry.CustomPadding.Right, (int)myEntry.CustomPadding.Bottom);
            }
        }


		void DrawBorder()
		{
			var borderLayer = new CALayer();
			borderLayer.MasksToBounds = true;
			borderLayer.Frame = new CoreGraphics.CGRect(0f, 0f, this.Frame.Width, this.Control.Bounds.Height);
			borderLayer.BorderColor = Color.FromHex("40FFFFFF").ToCGColor();
			borderLayer.BorderWidth = this.Control.Bounds.Width;

			Control.Layer.AddSublayer(borderLayer);
		}
    }
}