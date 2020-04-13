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
	public class TicketBlockModelViewCell : ViewCell
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
				new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
			}
		};

		Label lblBlocco_dateassign = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};

		Label lblBlocco_start = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};

		Label lblBlocco_end = new Label {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			LineBreakMode = LineBreakMode.WordWrap,
			FontSize = 12,
			BackgroundColor = Color.White,
		};


		public TicketBlockModelViewCell ()
		{            			
			MainGrid.Children.Add (lblBlocco_dateassign, 0, 0);
			MainGrid.Children.Add (lblBlocco_start, 1, 0);
			MainGrid.Children.Add (lblBlocco_end, 2, 0);

			View = MainGrid;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			TicketBlockModel ticketModel = (TicketBlockModel)this.BindingContext;

			if (ticketModel != null) {
				lblBlocco_dateassign.Text = string.Format("{0:dd/MM/yyyy}", ticketModel.Blocco_dateassign).Replace("-", "/");
				lblBlocco_start.Text = string.Format ("{0}", ticketModel.Blocco_start.ToString()).ToUpper ();
				lblBlocco_end.Text = string.Format("{0}", ticketModel.Blocco_end.ToString()).ToUpper();
			}            
		}
                   
	}
}
