using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Hermes
{
	[Activity (Label = "Hermes", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

			//Toolbar will now take on default actionbar characteristics
			SetSupportActionBar (toolbar);

			SupportActionBar.Title = "Hermes";

			replaceFragment(new LoginFragment ());

        }

		public void replaceFragment(Fragment f){
			Toast.MakeText (this, "Cambiar Fragment", ToastLength.Long).Show();

			// Create a new fragment and a transaction.
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
			// The fragment will have the ID of Resource.Id.fragment_container.
			fragmentTx.Add(Resource.Id.fragment_container, f);
			// Add the transaction to the back stack.
			fragmentTx.AddToBackStack(null);
			// Commit the transaction.
			fragmentTx.Commit();

		}

    }
}

