using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace Maestro
{
	public partial class FilterPopup : PopupPage
	{
		public FilterPopup()
		{
			InitializeComponent();

			foreach (var item in ApplicationObject.AlertCategories)
			{
				var lbl = new Label
				{
					Text = item.Description,
					TextColor = Color.FromHex(item.HexColor),
					FontAttributes = item == ApplicationObject.SelectedAlertCategory ? FontAttributes.Bold : FontAttributes.None
				};


				lbl.BindingContext = item;
				lbl.GestureRecognizers.Add(new TapGestureRecognizer()
				{
					TappedCallback = (arg1, arg2) =>
					{

						ApplicationObject.SelectedAlertCategory = arg1.BindingContext as AlertCategory;

						foreach (var itemChild in lytOptions.Children)
						{
							var lblAlerts = itemChild as Label;
							lblAlerts.FontAttributes = FontAttributes.None;
						}

						var lblSelected = arg1 as Label;
						lblSelected.FontAttributes = FontAttributes.Bold;
					}
				});

				lytOptions.Children.Add(lbl);
			}
		}

		void btnCancel_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PopAllPopupAsync();
		}

		void btnOk_Clicked(object sender, System.EventArgs e)
		{
			MessagingCenter.Send<HomePage, AlertCategory>(HomePage.CurrentHomePage, "SelectedAlertCategory", ApplicationObject.SelectedAlertCategory);
			Navigation.PopAllPopupAsync();
		}
	}
}
