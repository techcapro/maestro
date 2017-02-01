using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Maestro
{
	public class ApplicationObject
	{
		public static LoginToken LoginToken
		{
			get;
			set;
		} = new LoginToken();

		public static UserProfile CurrentUser
		{
			get;
			set;
		} = new UserProfile();

		public static List<AlertCategory> AlertCategories
		{
			get;
			set;
		} = new List<AlertCategory>();
		public static AlertCategory SelectedAlertCategory
		{
			get;
			set;
		}

		public static List<AlertNewsFeed> SaveAlerts
		{
			get;
			set;
		} = new List<AlertNewsFeed>();


		public const string ApiBaseAddress = "https://intergrationservices.mysurgicaltracking.com";
		public const string DefaultCategoryText = "All";
		public const string DBFileName = "MaestroSQLite.db3";
		public const string ColorGrey = "#c7c9cb";
	}
}
