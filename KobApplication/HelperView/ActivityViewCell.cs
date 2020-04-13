using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KobApp.HelperView
{

    public class ActivityViewCell : ViewCell
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
               new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.05, GridUnitType.Star) },
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

        Label lblDate = new Label
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

        Label lblAmount = new Label
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

        Label lblFee = new Label
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

        Label lblOC = new Label
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

        public ActivityViewCell()
        {
            MainGrid.Children.Add(lblName, 0, 0);
            MainGrid.Children.Add(lblDate, 1, 0);
            MainGrid.Children.Add(lblCode, 2, 0);
            MainGrid.Children.Add(lblAmount, 3, 0);
            MainGrid.Children.Add(lblFee, 4, 0);
            MainGrid.Children.Add(lblOC, 5, 0);

            View = MainGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ActivityModel activityModel = (ActivityModel)this.BindingContext;

            if (activityModel != null)
            {
                lblName.Text = activityModel.DATA_FIELD_1;
                lblDate.Text = string.Format("{0:dd/MM/yy}", activityModel.DATA_FIELD_2);
                lblCode.Text = activityModel.DATA_FIELD_3;
				lblAmount.Text = string.Format("{0:0.00}",activityModel.DATA_FIELD_4);
				lblFee.Text =  string.Format("{0:0.00}",activityModel.DATA_FIELD_5);
                lblOC.Text = activityModel.DATA_FIELD_6.ToString();
            }
        }
    }
}
