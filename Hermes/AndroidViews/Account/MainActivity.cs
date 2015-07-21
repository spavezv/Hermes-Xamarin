using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
using Android.Views;
using Hermes.AndroidViews.Account;
using System;
using Android.Content;
using Hermes.AndroidViews.Main;
using Hermes.WebServices;

namespace Hermes.AndroidViews
{
    [Activity(Label = "Hermes", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            Fragment f = new LoginFragment();
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Add(Resource.Id.fragment_container, f);
            fragmentTx.Commit();
        }

        public void replaceFragment(Fragment f)
        {
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Replace(Resource.Id.fragment_container, f);
            fragmentTx.AddToBackStack(null);
            fragmentTx.Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (this.FragmentManager.BackStackEntryCount > 0)
            {
                this.FragmentManager.PopBackStack();
            }
            else
            {
                base.OnBackPressed();
            }
        }
        protected override void OnResume()
        {
            Console.WriteLine("On Resume");
            ISharedPreferences prefs = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
            var remember = (prefs.GetBoolean(GlobalVar.REMEMBER_USER, false)) ? "Recuerdame " : "NO Recuerdame ";
            Console.WriteLine(remember);
            if (prefs.GetBoolean(GlobalVar.REMEMBER_USER, false))
            {
                var intent = new Intent(this, typeof(HermesActivity));
                intent.PutExtra(GlobalVar.USER_ID, prefs.GetInt(GlobalVar.USER_ID, -1));
                intent.PutExtra(GlobalVar.REMEMBER_USER, prefs.GetBoolean(GlobalVar.REMEMBER_USER, false));
                intent.PutExtra(GlobalVar.USER_EMAIL, prefs.GetString(GlobalVar.USER_EMAIL, null));
                intent.PutExtra(GlobalVar.USER_PASSWORD, prefs.GetString(GlobalVar.USER_PASSWORD, null));
                StartActivity(intent);
                Finish();
                
            }
            base.OnResume();
        }



    }
}
