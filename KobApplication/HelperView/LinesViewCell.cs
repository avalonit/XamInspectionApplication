using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KobApp.HelperView
{

    public class LinesViewCell : ViewCell
    {
        Grid MainGrid = new Grid
        {
            ColumnSpacing = 1,
            RowSpacing = 0,
            Padding = new Thickness(1),
            BackgroundColor = Color.FromHex(App.Black),
            RowDefinitions = new RowDefinitionCollection() {
                new RowDefinition () { Height = new GridLength (30, GridUnitType.Star) },
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
               new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Star) },
            }
        };

        Label lblLine = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.Black,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.WordWrap,
            FontSize = 15,
            BackgroundColor = Color.White,
        };

      
		public LinesViewCell()
        {
            MainGrid.Children.Add(lblLine, 0, 0);

            View = MainGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
			LinesModel model = (LinesModel)this.BindingContext;

            if (model != null)
            {
				lblLine.Text = model.Linea;
            }
        }
    }
}
