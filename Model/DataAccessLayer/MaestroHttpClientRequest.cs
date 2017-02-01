using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maestro
{
	/// <summary>
	/// Maestro http client request helper class
	/// </summary>
	public class MaestroHttpClientRequest
	{

		/// <summary>
		/// Posts the async using Body
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="apiRelativePath">API relative path.</param>
		/// <param name="serializableString">Serializable string.</param> 
		public static async Task<string> PostAsync(string apiRelativePath, string serializableString, bool Authorization = false)
		{
			try
			{
				var jsonString = string.Empty;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(ApplicationObject.ApiBaseAddress);

					var content = new StringContent(serializableString, Encoding.UTF8, "application/json");

					if (Authorization)
					{
						var _accessToken = ApplicationObject.LoginToken != null ? ApplicationObject.LoginToken.access_token : string.Empty;
						client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
					}

					var result = await client.PostAsync(apiRelativePath, content);

					jsonString = await result.Content.ReadAsStringAsync();
				}
				return jsonString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Posts as URL async using Key Value Pair
		/// </summary>
		/// <returns>The as URL async.</returns>
		/// <param name="apiRelativePath">API relative path.</param>
		/// <param name="objKeyValueObject">Object key value object.</param>
		public static async Task<string> PostAsFormUrlAsync(string apiRelativePath, Dictionary<string, string> objKeyValueObject)
		{
			try
			{
				var jsonString = string.Empty;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(ApplicationObject.ApiBaseAddress);

					var content = new FormUrlEncodedContent(objKeyValueObject);
					var result = await client.PostAsync(apiRelativePath, content);

					jsonString = await result.Content.ReadAsStringAsync();
				}
				return jsonString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="apiRelativePath">API relative path.</param>
		public static async Task<string> GetAsync(string apiRelativePath)
		{
			try
			{
				var jsonString = string.Empty;
				using (var client = new HttpClient())
				{
					var _accessToken = ApplicationObject.LoginToken != null ? ApplicationObject.LoginToken.access_token : string.Empty;
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

					var _fullUri = string.Format("{0}{1}", ApplicationObject.ApiBaseAddress, apiRelativePath);

					jsonString = await client.GetStringAsync(_fullUri);
				}
				return jsonString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}