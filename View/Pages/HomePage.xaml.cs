using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Maestro.Pages;
using Rg.Plugins.Popup.Extensions;

namespace Maestro
{
	public partial class HomePage : ContentPage
	{
		public static HomePage CurrentHomePage;
		static List<AlertNewsFeed> objClassListAlertNewsFeed = new List<AlertNewsFeed>();

		public Button BtnMenu
		{
			get { return btnMenu; }
		}

		public HomePage()
		{
			InitializeComponent();
			CurrentHomePage = this;

			NavigationPage.SetHasNavigationBar(this, false);

			var tapFilterClicked = new TapGestureRecognizer();
			tapFilterClicked.Tapped += async (sender, e) =>
			{
				try
				{
					await Navigation.PushPopupAsync(new FilterPopup());
					MessagingCenter.Subscribe<HomePage, AlertCategory>(this, "SelectedAlertCategory", (obj, arg) =>
					{
						BindAlertNewsFeedByCatergory();
					});
				}
				catch (Exception ex)
				{
					await DisplayAlert("Some error has occured", ex.Message, "Cancel");
				}
			};
			stckFilterClicked.GestureRecognizers.Add(tapFilterClicked);

			lstAlertNewsFeed.ItemTemplate = new MyTemplateSelector();
			//AuthenticatingFirstLoad();
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			AuthenticatingFirstLoad();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			//Binding all the Saved Alerts
			ApplicationObject.SaveAlerts = await App.Database.GetItemsAsync();

		}

		/// <summary>
		/// Lsts the alert news feed item selected.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void lstAlertNewsFeed_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			try
			{
				if (e.SelectedItem != null)
				{
					var objAlertNewFeed = e.SelectedItem as AlertNewsFeed;
					await Navigation.PushAsync(new AlertDetailsPage(objAlertNewFeed), false);
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error has occured", ex.Message, "Cancel");
			}
		}

		/// <summary>
		/// Lsts the alert news feed refreshing.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void lstAlertNewsFeed_Refreshing(object sender, System.EventArgs e)
		{
			await BindAlertNewsFeed();
			lstAlertNewsFeed.IsRefreshing = false;
		}

		/// <summary>
		/// Lsts the alert news feed item tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void lstAlertNewsFeed_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			try
			{
				if (e.Item != null)
				{
					var objAlertNewFeed = e.Item as AlertNewsFeed;
					await Navigation.PushAsync(new AlertDetailsPage(objAlertNewFeed), false);
					lstAlertNewsFeed.SelectedItem = null;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error has occured", ex.Message, "Cancel");
			}
		}

		/// <summary>
		/// Menu item reply alert.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void mnItmReplyAlert(object sender, EventArgs e)
		{
			var mnItemSelected = ((Xamarin.Forms.MenuItem)sender);
			var objAlertNewFeed = mnItemSelected.CommandParameter as AlertNewsFeed;
			await Navigation.PushAsync(new AlertDetailsPage(objAlertNewFeed), false);
		}

		/// <summary>
		/// Menu item Save alert.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void mnItmSave(object sender, EventArgs e)
		{
			var mnItemSelected = ((Xamarin.Forms.MenuItem)sender);
			var objAlertNewFeed = mnItemSelected.CommandParameter as AlertNewsFeed;
			var DetailedAlertNewsFeed = await AlertAccessLayer.GetAlertById(objAlertNewFeed.AppliedEntityId, objAlertNewFeed.Id);
			if (DetailedAlertNewsFeed != null && DetailedAlertNewsFeed.Id > 0)
			{
				if (DetailedAlertNewsFeed.Saved)
				{
					ApplicationObject.SaveAlerts = await App.Database.DeleteItemAsync(DetailedAlertNewsFeed);
					await DisplayAlert("Alert", "Removed Successfully", "OK");
				}
				else
				{
					DetailedAlertNewsFeed.IsSaved = true;
					ApplicationObject.SaveAlerts = await App.Database.SaveItemAsync(DetailedAlertNewsFeed);

					//objClassListAlertNewsFeed.First(obj => obj.Id.Equals(DetailedAlertNewsFeed.Id));
					await DisplayAlert("Alert", "Saved Successfully", "OK");
					BindAlertNewsFeedByCatergory();
				}
			}
			//TODO Change the Color of the Selected List View Item
		}

		/// <summary>
		/// Menu item reply alert.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void mnItmRemove(object sender, EventArgs e)
		{
			var mnItemSelected = ((Xamarin.Forms.MenuItem)sender);
			var objAlertNewFeed = mnItemSelected.CommandParameter as AlertNewsFeed;
			//TODO: Call the Remove API
		}

		/// <summary>
		/// Picker hospital selected index changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void pckHospital_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			await BindAlertNewsFeed();
		}

		/// <summary>
		/// filter Buttons clicked.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void btnFilter_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PushPopupAsync(new FilterPopup());
				MessagingCenter.Subscribe<HomePage, AlertCategory>(this, "SelectedAlertCategory", (obj, arg) =>
				{
					BindAlertNewsFeedByCatergory();
				});
			}
			catch (Exception ex)
			{
				await DisplayAlert("Some error has occured", ex.Message, "Cancel");
			}
		}

		/// <summary>
		/// Binds the picker control.
		/// </summary>
		void BindPickerControl()
		{
			try
			{
				if (pckHospital.Items != null && pckHospital.Items.Count > 0)
				{
					pckHospital.Items.Clear();
				}

				foreach (var item in ApplicationObject.CurrentUser.Entities)
				{
					pckHospital.Items.Add(item.DisplayName);
				}

				pckHospital.SelectedIndex = 0;
				pckHospital_SelectedIndexChanged(null, null);

			}
			catch (Exception ex)
			{
				DisplayAlert("Some error has occured", ex.Message, "Cancel");
			}
		}

		/// <summary>
		/// Authenticatings the first load.
		/// </summary>
		async void AuthenticatingFirstLoad()
		{
			try
			{
				var loginObject = new LoginRequest
				{
					grant_type = "password",
					username = "nirmal.subedi@randstadusa.com",
					password = "Aa123789%"
				};


				ApplicationObject.LoginToken = await LoginAccessLayer.AuthenticateUser(loginObject);


				if (ApplicationObject.LoginToken != null)
				{
					//Calling the Get Current Loggined In User Details
					//UserDialogs.Instance.ShowLoading("Fetching user info.. ");
					UserDialogs.Instance.ShowLoading();
					ApplicationObject.CurrentUser = await ProfileAccessLayer.GetLoggedInUserInfo();
					UserDialogs.Instance.HideLoading();

					//Get the Application Default Category of Alerts
					if (!ApplicationObject.AlertCategories.Any())
					{
						//UserDialogs.Instance.ShowLoading("Getting categories, almost there.. ");
						UserDialogs.Instance.ShowLoading();
						ApplicationObject.AlertCategories = await AlertAccessLayer.GetAlertCategories();
						UserDialogs.Instance.HideLoading();

						ApplicationObject.AlertCategories.Insert(0, new AlertCategory { Id = 0, Description = ApplicationObject.DefaultCategoryText, HexColor = "#543669" });
						ApplicationObject.SelectedAlertCategory = ApplicationObject.AlertCategories.FirstOrDefault();
					}

					if (ApplicationObject.CurrentUser != null && ApplicationObject.AlertCategories.Any())
					{
						//Binding the Hosptial Picker Control
						BindPickerControl();
					}
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Authentication Issue", ex.Message, "Cancel");
			}
		}

		/// <summary>
		/// Binds the alerts.
		/// </summary>
		async Task BindAlertNewsFeed()
		{
			try
			{
				string strAppliedEntityDisplayName = pckHospital.Items[pckHospital.SelectedIndex];
				var objAppliedEntityHospital = ApplicationObject.CurrentUser.Entities.Where(itm => itm.DisplayName == strAppliedEntityDisplayName).SingleOrDefault();

				//Load the Alerts
				UserDialogs.Instance.ShowLoading("Fetching alerts..");
				objClassListAlertNewsFeed = await AlertAccessLayer.GetAlerts(new AlertNewsRequest { AppliedEntityId = objAppliedEntityHospital.AppliedEntityId });
				UserDialogs.Instance.HideLoading();
				//var jsonStringNewsFeed = "[{    \"Id\": 10,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T16:23:36.177\",    \"Data\": [        {            \"Id\": 53,            \"AlertId\": 10,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 04:23 PM\"            ]        },        {            \"Id\": 54,            \"AlertId\": 10,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 10,            \"SpecialtyId\": 155,            \"EntitySpecialtyId\": 81,            \"Description\": \"BAM\"        }    ]},{    \"Id\": 9,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T16:12:03.363\",    \"Data\": [        {            \"Id\": 43,            \"AlertId\": 9,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 04:12 PM\"            ]        },        {            \"Id\": 44,            \"AlertId\": 9,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"Prezio Health\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 9,            \"SpecialtyId\": 114,            \"EntitySpecialtyId\": 1,            \"Description\": \"LOANER\"        }    ]},{    \"Id\": 8,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T15:20:11.16\",    \"Data\": [        {            \"Id\": 31,            \"AlertId\": 8,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 03:20 PM\"            ]        },        {            \"Id\": 32,            \"AlertId\": 8,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 8,            \"SpecialtyId\": 98,            \"EntitySpecialtyId\": 63,            \"Description\": \"EXACTECH\"        },        {            \"AlertId\": 8,            \"SpecialtyId\": 126,            \"EntitySpecialtyId\": 10,            \"Description\": \"ORTHOPEDIC\"        }    ]},{    \"Id\": 10,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T16:23:36.177\",    \"Data\": [        {            \"Id\": 53,            \"AlertId\": 10,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 04:23 PM\"            ]        },        {            \"Id\": 54,            \"AlertId\": 10,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 10,            \"SpecialtyId\": 155,            \"EntitySpecialtyId\": 81,            \"Description\": \"BAM\"        }    ]},{    \"Id\": 9,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T16:12:03.363\",    \"Data\": [        {            \"Id\": 43,            \"AlertId\": 9,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 04:12 PM\"            ]        },        {            \"Id\": 44,            \"AlertId\": 9,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"Prezio Health\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 9,            \"SpecialtyId\": 114,            \"EntitySpecialtyId\": 1,            \"Description\": \"LOANER\"        }    ]},{    \"Id\": 8,    \"AppliedEntityId\": 234,    \"TypeId\": 1,    \"Type\": \"Loaner Inventory Checked-In Late\",    \"CategoryId\": 2,    \"Category\": \"Loaner\",    \"AlertDateTime\": \"2016-12-28T15:20:11.16\",    \"Data\": [        {            \"Id\": 31,            \"AlertId\": 8,            \"SortOrder\": 1,            \"FieldName\": \"Processed Date\",            \"Value\": [                \"12/28/2016 03:20 PM\"            ]        },        {            \"Id\": 32,            \"AlertId\": 8,            \"SortOrder\": 2,            \"FieldName\": \"Manufacturer\",            \"Value\": [                \"\"            ]        }    ],    \"Specialties\": [        {            \"AlertId\": 8,            \"SpecialtyId\": 98,            \"EntitySpecialtyId\": 63,            \"Description\": \"EXACTECH\"        },        {            \"AlertId\": 8,            \"SpecialtyId\": 126,            \"EntitySpecialtyId\": 10,            \"Description\": \"ORTHOPEDIC\"        }    ]}]";
				//objClassListAlertNewsFeed = JsonConvert.DeserializeObject<List<AlertNewsFeed>>(jsonStringNewsFeed);

				BindAlertNewsFeedByCatergory();
			}
			catch (UnauthorizedAccessException ex)
			{
				await DisplayAlert("UnAuthorized", ex.Message, "Cancel");
				await Navigation.PushAsync(new LoginPage());
			}
			catch (Exception ex)
			{
				await DisplayAlert("Some error has occured", ex.Message, "Cancel");
			}
		}


		/// <summary>
		/// Creates the alert new feed view binding to ListView.
		/// </summary>
		/// <param name="objListAlertNewsFeed">Object list alert news feed.</param>
		void CreateAlertNewFeedView(List<AlertNewsFeed> objListAlertNewsFeed)
		{
			lstAlertNewsFeed.ItemsSource = objListAlertNewsFeed;
		}


		/// <summary>
		/// Binds the filter news feeds, based on the category selected
		/// </summary>
		/// <param name="strCategory">String category.</param>
		void BindAlertNewsFeedByCatergory()
		{
			if (objClassListAlertNewsFeed.Count > 0)
			{
				var _selectedCatergoryNewsFeed = ApplicationObject.SelectedAlertCategory.Description == ApplicationObject.DefaultCategoryText ?
					objClassListAlertNewsFeed : objClassListAlertNewsFeed.Where(itm => itm.Category == ApplicationObject.SelectedAlertCategory.Description).ToList();

				CreateAlertNewFeedView(_selectedCatergoryNewsFeed);
			}
			else
			{
				CreateAlertNewFeedView(new List<AlertNewsFeed>() { });
			}
			lblFilterText.Text = ApplicationObject.SelectedAlertCategory.Description.Trim();
			frmAlertTypeSelected.BackgroundColor = Color.FromHex(ApplicationObject.SelectedAlertCategory.HexColor);
		}

	}
}
