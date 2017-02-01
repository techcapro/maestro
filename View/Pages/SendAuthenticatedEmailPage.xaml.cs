using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Maestro
{
	public partial class SendAuthenticatedEmailPage : ContentPage
	{
		public SendAuthenticatedEmailPage()
		{
			InitializeComponent();
		}

		void btnSendAuthenticationCode_Clicked(object sender, System.EventArgs e)
		{
			App.Current.MainPage = new VerifyEmailPage();
		}
	}
}
