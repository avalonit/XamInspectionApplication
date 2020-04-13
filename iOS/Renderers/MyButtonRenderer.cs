using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using KobApplication.Controls;
using Xamarin.Forms.Platform.iOS;
using KobApplication.iOS.Renderers;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyButtonRenderer))]
namespace KobApplication.iOS.Renderers
{
    public class MyButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);


			if (this.Element is MyButton && Control != null)
			{
				MyButton myButton = this.Element as MyButton;
				this.Control.HorizontalAlignment = UIKit.UIControlContentHorizontalAlignment.Left;
				this.Control.ContentEdgeInsets = new UIKit.UIEdgeInsets((int)myButton.CustomPadding.Top, (int)myButton.CustomPadding.Left, (int)myButton.CustomPadding.Bottom, (int)myButton.CustomPadding.Right);
			}
        }

    }
}