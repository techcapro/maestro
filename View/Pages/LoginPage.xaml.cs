using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Maestro.Pages
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		async void btnLogin_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				var loginObject = new LoginRequest()
				{
					grant_type = "password",
					username = txtEmail.Text.Trim(),
					password = txtPassword.Text.Trim()
				};

				UserDialogs.Instance.ShowLoading("Authenticating.. ");
				var objLoginToken = await LoginAccessLayer.AuthenticateUser(loginObject);
				UserDialogs.Instance.HideLoading();


				ApplicationObject.LoginToken = objLoginToken;
				//await DisplayAlert("Login Token", objLoginToken.access_token, "Ok");

				//TODO 
				//Open the Alert Landing Page is Authenticated Successfully
				//var pageAlertHomePage = new MDPage();
				//await Navigation.PushAsync(pageAlertHomePage, true);
				App.Current.MainPage = new MDPage();
				//App.Current.MainPage = new AlertHomePage();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error while login", ex.Message, "Cancel");
			}
		}
	}
}
