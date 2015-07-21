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
using Android.Content;
using Hermes.WebServices;
using System;

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
        public int title { set; get; }
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
            mLeftDataSet.Add("Comentarios");
            mLeftDataSet.Add("Ayuda");
            mLeftAdapter = new ItemsAdapter(this, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            mLeftDrawer.AddHeaderView(LayoutInflater.From(this).Inflate(Resource.Layout.header, null, false), null, true);

            ISharedPreferences prefs = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);

            TextView txtEmail = FindViewById<TextView>(Resource.Id.email);
            txtEmail.Text = prefs.GetString(GlobalVar.USER_EMAIL, null);

            ISharedPreferences editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);

            String currentFragment = editor.GetString(GlobalVar.CURRENT_FRAGMENT, "");
            Fragment f;
            if (currentFragment.Equals("SETTINGS"))
            {
                title = Resource.String.Settings;
                f = new SettingFragment();
            }
            else if (currentFragment.Equals("USER_RESERVATIONS"))
            {
                title = Resource.String.MyReservation;
                f = new UserReservations();
            }
            else
            {
                title = Resource.String.Book;
                f = new TypeFragment();
            }

            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Add(Resource.Id.container, f, "");
            fragmentTx.Commit();

            

            mLeftDrawer.ItemClick += OnListItemClick;
            mDrawerToggle = new MyActionBarDrawerToggle(this, mDrawerLayout, Resource.String.OpenDrawer, title);

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
                    SupportActionBar.SetTitle(title);
                }
            }

            else
            {
                //This is the first the time the activity is ran
                SupportActionBar.SetTitle(title);
            }
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
                    ISharedPreferencesEditor editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
                    editor.PutString(GlobalVar.CURRENT_FRAGMENT, "SETTINGS");
                    mDrawerToggle.mClosedResource = Resource.String.Settings;
                    editor.Apply();
                    nextFragment(new SettingFragment());
                    break;
                case Resource.Id.ic_signout:
                    ISharedPreferencesEditor aditor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
                    aditor.Clear();
                    aditor.Apply();
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
            Console.WriteLine(GetString(title));
            var listView = sender as ListView;
            ISharedPreferencesEditor editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
            switch (e.Position)
            {
                case 0: //Usuario
                    break;
                case 1: //Reservar cancha
                    editor.PutString(GlobalVar.CURRENT_FRAGMENT, "BOOK");
                    editor.Apply();
                    mDrawerToggle.mClosedResource = Resource.String.Book;
                    replaceFragment(new TypeFragment(), "");
                    break;
                case 2: //Mis Reserva
                    editor.PutString(GlobalVar.CURRENT_FRAGMENT, "USER_RESERVATIONS");
                    editor.Apply();
                    mDrawerToggle.mClosedResource = Resource.String.MyReservation;
                    replaceFragment(new UserReservations(), GlobalVar.RESERVATION_DETAILS);
                    break;
                case 3: //Comentarios
                    sendComments();
                    //mDrawerToggle.mClosedResource = Resource.String.Comments;
                    break;
                case 4: //ayuda
                    replaceFragment(new HelpFragment(), "");
                    mDrawerToggle.mClosedResource = Resource.String.Help;
                    break;
            }

        }

        void sendComments()
        {
            var email = new Intent(Android.Content.Intent.ActionSend);

            email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { "hermes.app.android@gmail.com" });
            email.PutExtra(Android.Content.Intent.ExtraSubject, "Envio de comentario");
            email.PutExtra(Android.Content.Intent.ExtraText, "");
            email.SetType("message/rfc822");
            try
            {
                StartActivity(email);
            }
            catch (Android.Content.ActivityNotFoundException ex)
            {
                Toast.MakeText(this, "No existe aplicación de correo instalada", ToastLength.Short).Show();
            }
        }

        public void replaceFragment(Fragment fragment, String id)
        {

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, fragment, id);
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

