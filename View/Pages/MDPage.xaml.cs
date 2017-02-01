using System;
using System.Collections.Generic;
using Maestro.Pages;
using Xamarin.Forms;

namespace Maestro
{
	public partial class MDPage : MasterDetailPage
	{
		MasterPage master;
		HomePage detail;

		public MDPage()
		{
			InitializeComponent();

			master = new MasterPage();
			//detail = new AlertHomePage(); 
			detail = new HomePage();
			detail.BtnMenu.Clicked += BtnMenu_Clicked;

			Master = master;
			Detail = new NavigationPage(detail);
		}

		void BtnMenu_Clicked(object sender, EventArgs e)
		{
			IsPresented = !IsPresented;
		}
	}
}
