using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Pages
{
	/// <summary>
	/// Alert details page.
	/// </summary>
	public partial class AlertDetailsPage : ContentPage
	{

		AlertNewsFeed SelectedAlertNewsFeed;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Maestro.Pages.AlertDetailsPage"/> class.
		/// </summary>
		/// <param name="objAlertNewsFeed">Object alert news feed.</param>
		public AlertDetailsPage(AlertNewsFeed objAlertNewsFeed)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			//_SaveobjAlertNewsFeed = objAlertNewsFeed;

			BindDataToControls(objAlertNewsFeed);

			// Go Back Image Click
			var tapBackPage = new TapGestureRecognizer();
			tapBackPage.Tapped += async (sender, e) =>
			{
				await Navigation.PopAsync(false);
				//App.Current.MainPage = new MDPage();
				App.Current.MainPage = new NavigationPage(new HomePage());
			};
			imgPageBack.GestureRecognizers.Add(tapBackPage);

			// Home Image Click
			var tapHomePage = new TapGestureRecognizer();
			tapHomePage.Tapped += (sender, e) =>
			{
				//App.Current.MainPage = new MDPage();
				App.Current.MainPage = new NavigationPage(new HomePage());
			};
			imgPageHome.GestureRecognizers.Add(tapHomePage);

			// Accoridian Behavious - Show / Hide
			var tapShowHideToggle = new TapGestureRecognizer();
			tapShowHideToggle.Tapped += (sender, e) =>
			{
				stckMoreDetailsAlerts.IsVisible = !stckMoreDetailsAlerts.IsVisible;
				imgShowHideToggle.Source = stckMoreDetailsAlerts.IsVisible ? "open_section.png" : "close_section.png";
			};
			imgShowHideToggle.GestureRecognizers.Add(tapShowHideToggle);


			// SAVE Button Clicked
			var tapSaveSection = new TapGestureRecognizer();
			tapSaveSection.Tapped += async (sender, e) =>
			{
				//TODO: Add Loading Images
				if (SelectedAlertNewsFeed != null && SelectedAlertNewsFeed.Id > 0)
				{
					if (SelectedAlertNewsFeed.Saved)
					{
						ApplicationObject.SaveAlerts = await App.Database.DeleteItemAsync(SelectedAlertNewsFeed);
						await DisplayAlert("Alert", "Removed Successfully", "OK");
						lblSaveUnSaveText.Text = "Save";
					}
					else
					{
						SelectedAlertNewsFeed.IsSaved = true;
						ApplicationObject.SaveAlerts = await App.Database.SaveItemAsync(SelectedAlertNewsFeed);
						await DisplayAlert("Alert", "Saved Successfully", "OK");
						lblSaveUnSaveText.Text = "UnSave";
					}

				}
				//TODO: Remove Loading Images

				//var a = await App.Database.GetItemsAsync();
			};
			stckSaveAlert.GestureRecognizers.Add(tapSaveSection);

		}


		/// <summary>
		/// Binds the data to controls. & It also load the details information regarding the AlertNewsFeed
		/// </summary>
		/// <returns>The data to controls.</returns>
		/// <param name="objAlertNewsFeed">Object alert news feed.</param>
		async Task BindDataToControls(AlertNewsFeed objAlertNewsFeed)
		{
			cvShowTime.IsVisible = false;

			SelectedAlertNewsFeed = await AlertAccessLayer.GetAlertById(objAlertNewsFeed.AppliedEntityId, objAlertNewsFeed.Id);

			//TODO : put null cehck for alert details
			if (!SelectedAlertNewsFeed.Read)
			{
				SelectedAlertNewsFeed.IsRead = true;
				ApplicationObject.SaveAlerts = await App.Database.SaveItemAsync(SelectedAlertNewsFeed);
				//await DisplayAlert("Alert", "Read Successfully", "OK");
			}

			this.BindingContext = SelectedAlertNewsFeed;

			lblHospitalName.Text = ApplicationObject.CurrentUser.Entities.Where(itm => itm.AppliedEntityId == SelectedAlertNewsFeed.AppliedEntityId).SingleOrDefault() != null ?
				ApplicationObject.CurrentUser.Entities.Where(itm => itm.AppliedEntityId == SelectedAlertNewsFeed.AppliedEntityId).SingleOrDefault().DisplayName :
				"";

			//TODO: Add the Image to Change the Icon for Save and Unsave
			lblSaveUnSaveText.Text = SelectedAlertNewsFeed.Saved ? "UnSave" : "Save";

			cvShowTime.IsVisible = true;

			//lblType.Text = SelectedAlertNewsFeed.Type;
			//lblAlertDate.Text = SelectedAlertNewsFeed.AlertDateTime.ToString("MMM, dd yyyy");
			//lblAlertTime.Text = SelectedAlertNewsFeed.AlertDateTime.ToString("H:mm");

			//lblSurgeon.Text = SelectedAlertNewsFeed.Surgeon;
			//lblManufacturer.Text = SelectedAlertNewsFeed.Manufacturer;
			//lblCheckInNotes.Text = SelectedAlertNewsFeed.ProcessedBy;
			//lblManufacturerRep.Text = SelectedAlertNewsFeed.ManufacturerRep;
			//lblLateReason.Text = SelectedAlertNewsFeed.LateReason;
			//lblInventory.Text = SelectedAlertNewsFeed.Inventory;

			//objListAlertNewsFeed.Add(GetAlertNewsFeedDetails(SelectedAlertNewsFeed));
			//objListAlertNewsFeed.Add(GetAlertNewsFeedDetails(__objAlertNewsFeed));

			//myCarousel.ItemTemplate = new DataTemplate(typeof(DetailCell));
			//myCarousel.ItemsSource = objListAlertNewsFeed;
			//myCarousel.Position = 0;
			//myCarousel.PositionSelected += PositionSelected;
			//myCarousel.ShowIndicators = false;
			//PopulateDetails();
		}

	}
}
