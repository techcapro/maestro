using System;
using System.Collections.Generic;
using Maestro.Pages;
using Xamarin.Forms;

namespace Maestro
{
	public partial class LandingPage : ContentPage
	{
		public LandingPage()
		{
			InitializeComponent();
		}

		void btnIamNew_Clicked(object sender, System.EventArgs e)
		{
			App.Current.MainPage = new SendAuthenticatedEmailPage();
		}

		void btnLogin_Clicked(object sender, System.EventArgs e)
		{
			App.Current.MainPage = new LoginPage();
		}
	}
}
