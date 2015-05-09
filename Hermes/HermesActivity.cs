using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Views;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using ItemClickEventArgs = Android.Widget.AdapterView;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using System.Collections;
using Android.Content;

namespace Hermes
{
    [Activity(Label = "Hermes", Theme = "@style/MyTheme")]			
	public class HermesActivity : AppCompatActivity 
	{
	    private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
		private ItemsAdapter mLeftAdapter;
		private List<string> mLeftDataSet;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.hermes);

			mToolbar = FindViewById <Toolbar> (Resource.Id.toolbar_hermes);
            mDrawerLayout = FindViewById <DrawerLayout> (Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

			SetSupportActionBar (mToolbar);
			mLeftDataSet = new List<string> ();
			mLeftDataSet.Add ("Reservar cancha");
			mLeftDataSet.Add ("Reservar taller");
			mLeftDataSet.Add ("Mis reservas");
			mLeftDataSet.Add ("Configuración");
			mLeftDataSet.Add ("Ayuda y comentarios");
			mLeftAdapter = new ItemsAdapter (this, mLeftDataSet);
			mLeftDrawer.Adapter = mLeftAdapter;

			mLeftDrawer.ItemClick += OnListItemClick;

			mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.String.OpenDrawer, Resource.String.CloseDrawer);

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.OpenDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.CloseDrawer);
				}
			}

			else
			{
				//This is the first the time the activity is ran
				SupportActionBar.SetTitle(Resource.String.CloseDrawer);
			}

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
		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}
		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}
		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}

		void OnListItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var listView = sender as ListView;
			switch (e.Position) {
			case 0: //Reservar cancha
				FragmentTransaction transaction = FragmentManager.BeginTransaction ();
				SlidingTabsFragment fragment = new SlidingTabsFragment ();
				transaction.Replace (Resource.Id.container, fragment);
				transaction.Commit ();
				mDrawerLayout.CloseDrawers ();

				break;
//
//			case 1: //Reservar taller
//
//			case 2: //Mis Reserva
//
//			case 3: //Configuracion
//
//			case 4: //ayuda

		
			}
		}
	}
}

