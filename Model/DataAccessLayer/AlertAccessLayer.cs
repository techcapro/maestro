using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maestro
{
	public class AlertAccessLayer
	{
		static string apiAlertNewsFeed = "/MaestroQAMobile/api/Alert/NewsFeed";
		static string apiAlertNewsFeedDetails = "/MaestroQAMobile/api/Alert/Get?appliedEntityId={0}&alertId={1}";
		static string apiAlertCatergories = "/MaestroQAMobile/api/Alert/Categories";

		public static async Task<List<AlertNewsFeed>> GetAlerts(AlertNewsRequest obj)
		{
			try
			{
				var alertNewsFeedString = await MaestroHttpClientRequest.PostAsync(apiAlertNewsFeed, JsonConvert.SerializeObject(obj), true);

				var alertNewsFeedObject = JsonConvert.DeserializeObject<List<AlertNewsFeed>>(alertNewsFeedString);
				return alertNewsFeedObject;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static async Task<AlertNewsFeed> GetAlertById(int appliedEntityId, int alertId)
		{
			try
			{
				string _apiRelativeUrl = string.Format(apiAlertNewsFeedDetails, appliedEntityId, alertId);
				var alertNewsFeedString = await MaestroHttpClientRequest.GetAsync(_apiRelativeUrl);

				var alertNewsFeedObject = JsonConvert.DeserializeObject<AlertNewsFeed>(alertNewsFeedString);
				return alertNewsFeedObject;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public static async Task<List<AlertCategory>> GetAlertCategories()
		{
			try
			{
				var alertCategoriesString = await MaestroHttpClientRequest.GetAsync(apiAlertCatergories);

				var alertCategoriesObject = JsonConvert.DeserializeObject<List<AlertCategory>>(alertCategoriesString);
				return alertCategoriesObject;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
