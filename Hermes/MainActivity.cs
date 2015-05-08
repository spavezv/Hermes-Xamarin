using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Views;

namespace Hermes
{
    [Activity(Label = "Hermes", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
	public class MainActivity : AppCompatActivity 
	{

		protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);

			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			SetSupportActionBar (toolbar);

			Fragment f = new LoginFragment ();
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Add(Resource.Id.fragment_container, f);
			fragmentTx.AddToBackStack(null);
			fragmentTx.Commit();

        }

		public void replaceFragment(Fragment f){
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.fragment_container, f);
			fragmentTx.AddToBackStack(null);
			fragmentTx.Commit();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu_main, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}

    }
}
