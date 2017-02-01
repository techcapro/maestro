using System;
using Xamarin.Forms;

namespace Maestro
{
	public class ContentViewRoundedCorners : ContentView
	{
		public static readonly BindableProperty CornerRaidusProperty =
		BindableProperty.Create<ContentViewRoundedCorners, float>(x => x.CornerRadius, 0);
		public float CornerRadius
		{
			get { return (float)GetValue(CornerRaidusProperty); }
			set { SetValue(CornerRaidusProperty, value); }
		}
	}

	public class XViewCell : ViewCell
	{
		public Color ContextOptionColor
		{
			get;
			set;
		}
	}
}
