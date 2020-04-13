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
	public class SearchUserViewCell : ViewCell
	{		
		Grid MainGrid = new Grid {
			ColumnSpacing = 1,
			RowSpacing = 0,
			Padding = new Thickness (1),
			BackgroundColor = Color.FromHex (App.Black),
			RowDefinitions = new RowDefinitionCollection () {
				new RowDefinition () { Height = new GridLength (60, GridUnitType.Star) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection () {
				new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.35, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.25, GridUnitType.Star) },
			}
		};

		Label lblName = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};

		Label lblAddress = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};

		Label lblDateOfBirth = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};


		Image imgPhoto = new Image {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			Aspect = Aspect.Fill,
			BackgroundColor = Color.White,
			HeightRequest = 60,
		};

		public SearchUserViewCell ()
		{            			
			MainGrid.Children.Add (lblName, 0, 0);
			MainGrid.Children.Add (lblAddress, 1, 0);
			MainGrid.Children.Add (lblDateOfBirth, 2, 0);
			MainGrid.Children.Add (imgPhoto, 3, 0);

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
