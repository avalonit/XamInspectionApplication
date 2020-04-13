using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KobApp.HelperView
{

    public class InspectionInspectorsViewCell : ViewCell
    {
        Grid MainGrid = new Grid
        {
            ColumnSpacing = 1,
            RowSpacing = 0,
            Padding = new Thickness(1),
            BackgroundColor = Color.FromHex(App.Black),
            RowDefinitions = new RowDefinitionCollection() {
                new RowDefinition () { Height = new GridLength (60, GridUnitType.Star) },
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
               new ColumnDefinition () { Width = new GridLength (0.70, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
            }
        };

        Label lblName = new Label
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

        Label lblFrom = new Label
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

		Label lblTo = new Label
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

		public InspectionInspectorsViewCell()
        {
            MainGrid.Children.Add(lblName, 0, 0);
            MainGrid.Children.Add(lblFrom, 1, 0);
            MainGrid.Children.Add(lblTo, 2, 0);

            View = MainGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            InspectionInspectorsModel model = (InspectionInspectorsModel)this.BindingContext;

            if (model != null)
            {
				lblName.Text = model.InspectorName;
				if(  model.Inspection_start_time!=null )
					lblFrom.Text = string.Format("{0:HH:mm}", model.Inspection_start_time);
				if (model.Inspection_end_time != null)
					lblTo.Text = string.Format("{0:HH:mm}", model.Inspection_end_time);
            }
        }
    }
}
