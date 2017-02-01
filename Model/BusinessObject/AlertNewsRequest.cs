using System;
namespace Maestro
{
	public class AlertNewsRequest
	{
		public int AppliedEntityId
		{
			get;
			set;
		}
		public int CategoryId
		{
			get;
			set;
		} = 0;
		public int PageSize
		{
			get;
			set;
		} = 0;
		public int PageNumber
		{
			get;
			set;
		} = 0;
	}
}
