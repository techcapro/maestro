using System;
using System.Collections.Generic;
using System.Globalization;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace Maestro
{
	public class AlertNewsFeed
	{
		public int Id
		{
			get;
			set;
		}
		public int AppliedEntityId
		{
			get;
			set;
		}
		public int TypeId
		{
			get;
			set;
		}
		public string Type
		{
			get;
			set;
		}
		public int CategoryId
		{
			get;
			set;
		}
		public string Category
		{
			get;
			set;
		}
		public DateTime AlertDateTime
		{
			get;
			set;
		}

		[Ignore]
		public List<AlertData> Data
		{
			get;
			set;
		}

		public string IconPathCategory
		{
			get
			{
				if (Saved)
				{
					switch (this.Category.ToLower())
					{
						case "sterility":
							return "sv_sterility_indicator_icon.png";
						case "loaner":
							return "sv_loaner_indicator_icon.png";
						case "inventory":
							return "sv_inventory_indicator_icon.png";
						case "staff":
							return "sv_staff_indicator_icon.png";
						default:
							return "sv_loaner_indicator_icon.png";
					};
				}
				else
				{
					switch (this.Category.ToLower())
					{
						case "sterility":
							return "sterility_indicator_icon.png";
						case "loaner":
							return "loaner_indicator_icon.png";
						case "inventory":
							return "inventory_indicator_icon.png";
						case "staff":
							return "staff_indicator_icon.png";
						default:
							return "loaner_indicator_icon.png";
					};
				}
			}
		}

		[Ignore]
		public Color TextFontColor
		{
			get
			{
				var _selectedAlertHexColor = ApplicationObject.AlertCategories.Where(itm => itm.Id == this.CategoryId).SingleOrDefault() != null ?
															  ApplicationObject.AlertCategories.Where(itm => itm.Id == this.CategoryId).SingleOrDefault().HexColor : "#000000";
				return Color.FromHex(_selectedAlertHexColor);
			}
		}

		[PrimaryKey, AutoIncrement]
		public int UID { get; set; }

		public string ProcessedBy
		{
			get { return GetValue(this, "processed by"); }
		}

		public string ProcessedDateStr
		{
			get { return GetValue(this, "processed date"); }
		}

		public DateTime? ProcessedDate
		{
			get { return DateTime.ParseExact(ProcessedDateStr, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture); }
		}

		public string Manufacturer
		{
			get { return GetValue(this, "manufacturer"); }
		}

		public string ManufacturerRep
		{
			get { return GetValue(this, "manufacturer rep"); }
		}

		public string CaseStartStr
		{
			get { return GetValue(this, "case start time"); }
		}

		public DateTime? CaseStartDate
		{
			get { return DateTime.ParseExact(CaseStartStr, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture); }
		}

		public string Surgeon
		{
			get { return GetValue(this, "surgeon"); }
		}

		public string CheckInNotes
		{
			get { return GetValue(this, "check-in notes"); }
		}

		public string LateReason
		{
			get { return GetValue(this, "late reason"); }
		}

		public string TimeLate
		{
			get { return GetValue(this, "time late"); }
		}

		public string Inventory
		{
			get { return GetValue(this, "inventory"); }
		}

		string GetValue(AlertNewsFeed _objAlertNewsFeed, string forKey)
		{
			return _objAlertNewsFeed.Data != null && _objAlertNewsFeed.Data.Count > 0 ?
													   (_objAlertNewsFeed.Data.Where(itm => itm.FieldName.ToLower() == forKey).SingleOrDefault() != null ?
														string.Join("\\n", _objAlertNewsFeed.Data.Where(itm => itm.FieldName.ToLower() == forKey).SingleOrDefault().Value) : "") : "";
		}

		public bool IsSaved
		{
			get;
			set;
		}

		[Ignore]
		public bool Saved
		{
			get
			{
				this.IsSaved = ApplicationObject.SaveAlerts.Where(itm => itm.Id == this.Id && itm.AppliedEntityId == this.AppliedEntityId).SingleOrDefault() != null &&
											ApplicationObject.SaveAlerts.Where(itm => itm.Id == this.Id && itm.AppliedEntityId == this.AppliedEntityId).SingleOrDefault().IsSaved == true ? true : false;

				return this.IsSaved; ;
			}
		}

		public bool IsRead
		{
			get;
			set;
		}

		[Ignore]
		public bool Read
		{
			get
			{
				this.IsRead = ApplicationObject.SaveAlerts.Where(itm => itm.Id == this.Id && itm.AppliedEntityId == this.AppliedEntityId).SingleOrDefault() != null &&
											ApplicationObject.SaveAlerts.Where(itm => itm.Id == this.Id && itm.AppliedEntityId == this.AppliedEntityId).SingleOrDefault().IsRead == true ? true : false;
				return IsRead;
			}
		}

		[Ignore]
		public bool IsReadOrSaved
		{
			get { return (Read == true || Saved == true) ? true : false; }
		}

		[Ignore]
		public Color ItemSelectedColor
		{
			get
			{
				return IsReadOrSaved ? Color.FromHex(ApplicationObject.ColorGrey) : Color.White;
			}
		}

	}

	public class AlertData
	{
		public int Id
		{
			get;
			set;
		}
		public int AlertId
		{
			get;
			set;
		}
		public int SortOrder
		{
			get;
			set;
		}
		public string FieldName
		{
			get;
			set;
		}
		public List<string> Value
		{
			get;
			set;
		}
	}
}
