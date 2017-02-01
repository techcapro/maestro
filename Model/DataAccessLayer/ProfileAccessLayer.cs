using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maestro
{
	/// <summary>
	/// User Profile access layer.
	/// </summary>
	public class ProfileAccessLayer
	{
		static string apiUserInfoRelativePath = "/MaestroQAMobile/api/Account/UserInfo";

		/// <summary>
		/// Gets the logged in user info.
		/// </summary>
		/// <returns>The logged in user info.</returns>
		public static async Task<UserProfile> GetLoggedInUserInfo()
		{
			try
			{
				// Calling the Get Request to Get the User Logged in User Info
				var userInfoString = await MaestroHttpClientRequest.GetAsync(apiUserInfoRelativePath);

				// Converting the String response to Object
				var userInfoObject = JsonConvert.DeserializeObject<UserProfile>(userInfoString);

				// Returning the data
				return userInfoObject;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
