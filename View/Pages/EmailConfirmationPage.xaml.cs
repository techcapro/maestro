using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Maestro
{
	public partial class EmailConfirmationPage : ContentPage
	{
		public EmailConfirmationPage()
		{
			InitializeComponent();
		}

		void btnGetStarted_Clicked(object sender, System.EventArgs e)
		{
			App.Current.MainPage = new CreateProfilePage();
		}
	}
}
