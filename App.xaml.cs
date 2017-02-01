using Xamarin.Forms;
using Maestro.Pages;

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace Maestro
{
	public partial class App : Application
	{
		static MaestroDatabase database;
		public static MaestroDatabase Database
		{
			get
			{
				database = database ?? new MaestroDatabase(DependencyService.Get<ISQLiteDBFileHelper>().GetLocalFilePath(ApplicationObject.DBFileName));
				return database;
			}
		}


		public App()
		{
			InitializeComponent();

			//MainPage = new LandingPage();
			//MainPage = new AlertHomePage();
			MainPage = new NavigationPage(new HomePage());
			//MainPage = new MDPage();
			//MainPage = new NavigationPage(new AlertDetailsPage(new AlertNewsFeed()));
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
