﻿using System;
namespace Maestro
{
	public class LoginRequest
	{
		public LoginRequest()
		{
		}

		public string grant_type
		{
			get;
			set;
		}
		public string username
		{
			get;
			set;
		}
		public string password
		{
			get;
			set;
		}
	}
}
