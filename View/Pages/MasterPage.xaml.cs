using System;
using System.Collections.Generic;
using Maestro.Pages;
using Xamarin.Forms;

namespace Maestro
{
	public partial class MasterPage : ContentPage
	{
		public MasterPage()
		{
			InitializeComponent();

			lstMenu.ItemsSource = new List<MenuItem> {
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "Profile", Target = typeof(AlertDetailsPage) },
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "Alert Settings", Target = typeof(AlertDetailsPage) },
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "View Saved Alerts", Target = typeof(AlertDetailsPage) },
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "Alert Search", Target = typeof(AlertDetailsPage) },
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "Help", Target = typeof(AlertDetailsPage) },
				new MenuItem{ ImgSource = "ic_panorama_fish_eye", Title = "Contact Maestro", Target = typeof(AlertDetailsPage) },
			};

		}
	}
}
