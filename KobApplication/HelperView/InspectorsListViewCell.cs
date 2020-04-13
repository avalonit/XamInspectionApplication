using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KobApp.HelperView
{

    public class InspectorsListViewCell : ViewCell
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
               new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.80, GridUnitType.Star) },
            }
        };

        Label lblCode = new Label
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

        Label lblInspectors = new Label
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


        public InspectorsListViewCell()
        {
            MainGrid.Children.Add(lblCode, 0, 0);
            MainGrid.Children.Add(lblInspectors, 1, 0);

            View = MainGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
			InspectorsModel activityModel = (InspectorsModel)this.BindingContext;

            if (activityModel != null)
            {
				lblCode.Text = activityModel.a_CodVer;
				lblInspectors.Text = activityModel.a_descrizione;
            }
        }
    }
}
