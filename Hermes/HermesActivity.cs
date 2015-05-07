
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace Hermes
{
	[Activity (Label = "HermesActivity")]			
	public class HermesActivity : AppCompatActivity
	{
		private SupportToolbar stoolbar;
		private ActionBarDrawerToggle drawerToggle;
		private DrawerLayout drawerLayout;
		private ListView lstItemsDrawer;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HermesMain);

			stoolbar = FindViewById<SupportToolbar> (Resource.Id.toolbar);
			drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			lstItemsDrawer = FindViewById<ListView> (Resource.Id.left_drawer);
			lstItemsDrawer.Tag = 0;

			SetSupportActionBar (stoolbar);
			drawerToggle = new ActionBarDrawerToggle (
				this, //Activity
				drawerLayout, //Drawer Layout
				Resource.String.OpenDrawer, //Open Message
				Resource.String.ApplicationName); //Close Message
			
			drawerLayout.SetDrawerListener(drawerToggle);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			drawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.OpenDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.ApplicationName);
				}
			}

			else
			{
				//This is the first the time the activity is ran
				SupportActionBar.SetTitle(Resource.String.ApplicationName);
			}

		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			drawerToggle.OnOptionsItemSelected (item);
			return base.OnOptionsItemSelected (item);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (drawerLayout.IsDrawerOpen((int)GravityFlags.Left))
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
			drawerToggle.SyncState();
		}
	}
}

