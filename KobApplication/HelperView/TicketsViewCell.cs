using System;

using Xamarin.Forms;
using KobApp.DataModel;

namespace KobApp.HelperView
{
    public class TicketsViewCell : ViewCell
    {
        Grid CellGrid = new Grid
        {
            ColumnSpacing = 0,
            RowSpacing = 0,
            Padding = new Thickness(1),
            BackgroundColor = Color.Transparent,
            RowDefinitions = new RowDefinitionCollection() {
                new RowDefinition () { Height = new GridLength (60, GridUnitType.Star) },
                new RowDefinition () { Height = 1 },
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
                new ColumnDefinition () { Width = 1 },
                new ColumnDefinition () { Width = new GridLength (0.40, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1 },
                new ColumnDefinition () { Width = new GridLength (0.23, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1 },
                new ColumnDefinition () { Width = new GridLength (0.23, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1 },
                new ColumnDefinition () { Width = new GridLength (0.16, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1 },
            }
        };

        View lineWhite1 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };

        View lineWhite2 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };
        View lineWhite3 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };
        View lineWhite4 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };

        View lineWhite5 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };
        View lineWhite6 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.White,
        };

        Label lblTicketType = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold,
        };

        Label lblFrom = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.WordWrap,
            FontSize = 12,
            BackgroundColor = Color.Transparent,
        };

        Label lblTo = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.WordWrap,
            FontSize = 12,
            BackgroundColor = Color.Transparent,
        };

        Image imgValid = new Image
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Aspect = Aspect.AspectFit,
            Margin = new Thickness(10, 0),
            //TextColor = Color.White,
            //HorizontalTextAlignment = TextAlignment.Center,
            //VerticalTextAlignment = TextAlignment.Center,
            //LineBreakMode = LineBreakMode.WordWrap,
            //FontSize = 12,
            BackgroundColor = Color.Transparent,
        };

        public TicketsViewCell()
        {
            CellGrid.Children.Add(lineWhite1, 0, 0);
            CellGrid.Children.Add(lblTicketType, 1, 0);
            CellGrid.Children.Add(lineWhite2, 2, 0 );
            CellGrid.Children.Add(lblFrom, 3, 0);
            CellGrid.Children.Add(lineWhite3, 4, 0);
            CellGrid.Children.Add(lblTo, 5, 0);
            CellGrid.Children.Add(lineWhite4, 6, 0);
            CellGrid.Children.Add(imgValid, 7, 0);
            CellGrid.Children.Add(lineWhite5, 8, 0);
            CellGrid.Children.Add(lineWhite6, 0, 8, 1, 2);

            View = CellGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            try
            {
                TicketModel ticketModel = (TicketModel)this.BindingContext;
                string fromDate = "", toDate = "";
                if (ticketModel != null)
                {
                    lblTicketType.Text = ticketModel.DATA_FIELD_1;
					//if(ticketModel.DATA_FIELD_2 != null && !ticketModel.DATA_FIELD_2.Equals(""))
					//{
					//    fromDate = ticketModel.DATA_FIELD_2.Substring(ticketModel.DATA_FIELD_2.LastIndexOf(" "));
					//    System.Diagnostics.Debug.WriteLine("From Date : " + fromDate);
					//    lblFrom.Text = fromDate.Replace("/","-");
					//}

					//if(ticketModel.DATA_FIELD_3 != null && !ticketModel.DATA_FIELD_3.Equals(""))
					//{
					//    toDate = ticketModel.DATA_FIELD_3.Substring(ticketModel.DATA_FIELD_3.LastIndexOf(" "));
					//    System.Diagnostics.Debug.WriteLine("to Date :" + toDate);
					//    lblTo.Text = toDate.Replace("/", "-");
					//}
					lblFrom.Text = ticketModel.DATA_FIELD_2;
					lblTo.Text = ticketModel.DATA_FIELD_3;
                    imgValid.Source = ticketModel.DATA_FIELD_4 == 0 ? "no.png" : "yes.png";
                }
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Ticket Cell Exception : " + pException.Message + " StackTrace : " + pException.StackTrace);
            }            
        }

    }
}


