using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Maestro
{
	public class LoginAccessLayer
	{
		static string apiAuthenticateRelativePath = "/MaestroQAMobile/Token";
		public LoginAccessLayer()
		{
		}

		public static async Task<LoginToken> AuthenticateUser(LoginRequest objLoginRequest)
		{
			try
			{
				var objLoginRequestDictionary = new Dictionary<string, string>();
				objLoginRequestDictionary.Add("grant_type", objLoginRequest.grant_type);
				objLoginRequestDictionary.Add("username", objLoginRequest.username);
				objLoginRequestDictionary.Add("password", objLoginRequest.password);


				var loginTokenString = await MaestroHttpClientRequest.PostAsFormUrlAsync(apiAuthenticateRelativePath,
																				objLoginRequestDictionary);
				var loginTokenObject = JsonConvert.DeserializeObject<LoginToken>(loginTokenString);

				return loginTokenObject;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
