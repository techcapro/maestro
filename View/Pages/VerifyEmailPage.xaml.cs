	using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Maestro
{
	public partial class VerifyEmailPage : ContentPage
	{
		public VerifyEmailPage()
		{
			InitializeComponent();
		}

		void btnSubmit_Clicked(object sender, System.EventArgs e)
		{
			App.Current.MainPage = new EmailConfirmationPage();
		}
	}
}
