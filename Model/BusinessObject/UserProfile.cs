using System;
using System.Collections.Generic;

namespace Maestro
{
	public class UserProfile
	{
		public int Id
		{
			get;
			set;
		}

		public string EmailAddress
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public string StaffType
		{
			get;
			set;
		}

		public DateTime CodeValidated
		{
			get;
			set;
		}

		public bool HasBeenValidated
		{
			get;
			set;
		}

		public List<HospitalEntity> Entities
		{
			get;
			set;
		}
	}
}
