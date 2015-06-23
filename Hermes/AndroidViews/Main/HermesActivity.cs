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
using Hermes.AndroidViews.Reservations;
using Hermes.AndroidViews.ActionBarDrawerToggle;
using Hermes.AndroidViews.CourtBooking;
using Hermes.Models;
using System;
using Hermes.AndroidViews.Account;
using Android.Content;
using Hermes.WebServices;

namespace Hermes.AndroidViews.Main
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

        public string TypeSport { set; get; }
        public string DateEsp { set; get; }
        public string Date { set; get; }
        public Branches mBranch { set; get; }

        public string workshop { set; get; }

        public Block mBlock { set; get; }
        public static Clients Client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.hermes);

            mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_hermes);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

            Client = new Clients();

            SetSupportActionBar(mToolbar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Reservar cancha");
            mLeftDataSet.Add("Mis reservas");
            mLeftDataSet.Add("Configuración");
            mLeftDataSet.Add("Ayuda");
            mLeftAdapter = new ItemsAdapter(this, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.AddHeaderView(LayoutInflater.From(this).Inflate(Resource.Layout.header, null, false), null, true);

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

            Fragment f = new UserReservations();
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Add(Resource.Id.container, f);
            fragmentTx.Commit();

        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_hermes, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.ic_settings:
                    break;
                case Resource.Id.ic_signout:
                    signout();
                    break;
            }
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }

            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }
        
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }
        
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            switch (e.Position)
            {
                case 0: //Usuario
                    break;
                case 1: //Reservar cancha
                    replaceFragment(new TypeFragment());
                    break;
                case 2: //Mis Reserva
                    replaceFragment(new UserReservations());
                    break;
                case 3: //Configuracion

                    break;
                case 4: //ayuda
				replaceFragment(new HelpFragment());
				break;
            }
        }

        public void replaceFragment(Fragment fragment)
        {

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, fragment);
            transaction.Commit();
            mDrawerLayout.CloseDrawers();
        }

        public void nextFragment(Fragment fragment)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        public void signout()
        {
            ISharedPreferencesEditor editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
            editor.PutBoolean(GlobalVar.REMEMBER_USER, false);
            editor.Apply();

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}

