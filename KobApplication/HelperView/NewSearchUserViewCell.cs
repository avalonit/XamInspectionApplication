using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Globalization;

namespace KobApp.HelperView
{
	public class NewSearchUserViewCell : ViewCell
	{		
		Grid MainGrid = new Grid {
			//ColumnSpacing = 1,
			//RowSpacing = 3,
			//Padding = new Thickness (1),
			Padding = new Thickness(0, 5),
			BackgroundColor = Color.Transparent,
			RowDefinitions = new RowDefinitionCollection () {
				new RowDefinition () { Height = GridLength.Auto },
                new RowDefinition () { Height = 1 },
            },
			ColumnDefinitions = new ColumnDefinitionCollection () {
				new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (3, GridUnitType.Star) }
			}
		};

        View lineBottom = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        Label lblName = new Label {
			HorizontalOptions = LayoutOptions.Fill,
			VerticalOptions = LayoutOptions.StartAndExpand,
			TextColor = Color.Yellow,
			HorizontalTextAlignment = TextAlignment.Start,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.TailTruncation,
			FontSize = 12,
			BackgroundColor = Color.Transparent,
            FontAttributes = FontAttributes.Bold,
        };

		Label lblAddress = new Label {
			HorizontalOptions = LayoutOptions.Fill,
			VerticalOptions = LayoutOptions.StartAndExpand,
			TextColor = Color.Wheat,
			HorizontalTextAlignment = TextAlignment.Start,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.TailTruncation,
			FontSize = 10,
			BackgroundColor = Color.Transparent,
            FontAttributes = FontAttributes.Bold,
        };

		StackLayout stackInfor = new StackLayout
        {
            BackgroundColor = Color.Transparent,
			Orientation = StackOrientation.Vertical,
			Spacing = 3,
            //Padding = new Thickness(1, 0, 1, 0)
        };

        Label lblDateOfBirth = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.Transparent,
		};


		Image imgPhoto = new Image {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			Aspect = Aspect.AspectFit,
			BackgroundColor = Color.Transparent,
			HeightRequest = 30,
            WidthRequest = 30,
            Margin = new Thickness(5)
		};

		public NewSearchUserViewCell()
		{
            MainGrid.Children.Add(imgPhoto, 0, 0);
            stackInfor.Children.Add(lblName);
            stackInfor.Children.Add(lblAddress);
            MainGrid.Children.Add(stackInfor, 1, 0);
			MainGrid.Children.Add (lblDateOfBirth, 2, 0);
            MainGrid.Children.Add(lineBottom, 0, 3, 1, 2);

			View = MainGrid;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			UsersModel usersModel = (UsersModel)this.BindingContext;

			if (usersModel != null) {
                if(usersModel.IMAGE != null && !usersModel.IMAGE.Equals(""))
                    imgPhoto.Source = ImageSource.FromUri(new Uri(usersModel.IMAGE));

                lblName.Text = string.Format ("{0} {1}", usersModel.NOME, usersModel.COGNOME).ToUpper ();
				lblAddress.Text = string.Format ("{0}, {1}, {2}", usersModel.RESID_COMUNE, usersModel.RESID_INDIRIZZO, usersModel.RESID_LOCALITA).ToUpper ();
				lblDateOfBirth.Text = string.Format("{0:dd/MM/yyyy}", usersModel.DATA_NASCITA).Replace("-", "/");
			}            
		}
                   
	}
}
