using System;
using Xamarin.Forms;

namespace Maestro
{
	public class MyTemplateSelector : DataTemplateSelector
	{
		readonly DataTemplate templateSave;
		readonly DataTemplate templateUnSave;

		public MyTemplateSelector()
		{
			this.templateSave = new DataTemplate(typeof(AlertNewsFeedCell));
			this.templateUnSave = new DataTemplate(typeof(AlertNewsFeed_UnSaveCell));
		}

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			var obj = item as AlertNewsFeed;
			if (obj == null)
				return templateSave;

			if (obj.Saved)
				return templateUnSave;
			return templateSave;
		}
	}
}
