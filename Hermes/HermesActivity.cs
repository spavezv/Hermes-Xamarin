using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Views;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;

namespace Hermes
{
    [Activity(Label = "Hermes", Theme = "@style/MyTheme")]			
	public class HermesActivity : AppCompatActivity 
	{
	    private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.hermes);

			mToolbar = FindViewById <Toolbar> (Resource.Id.toolbar_hermes);
            mDrawerLayout = FindViewById <DrawerLayout> (Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

			SetSupportActionBar (mToolbar);

            mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.String.OpenDrawer, Resource.String.CloseDrawer);

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

			SupportActionBar.Title = "Hermes";

		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu_main, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
            mDrawerToggle.OnOptionsItemSelected(item);
			return base.OnOptionsItemSelected (item);
		}

	}
}

