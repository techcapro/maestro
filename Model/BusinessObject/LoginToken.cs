﻿using System;
namespace Maestro
{
	public class LoginToken
	{
		public LoginToken()
		{
		}
		public string access_token
		{
			get;
			set;
		}
		public string token_type
		{
			get;
			set;
		}
		public string expires_in
		{
			get;
			set;
		}
		public string userName
		{
			get;
			set;
		}
		public DateTime issued
		{
			get;
			set;
		}
		public DateTime expires
		{
			get;
			set;
		}
	}
}
