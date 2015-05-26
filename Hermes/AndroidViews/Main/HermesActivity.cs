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
using Hermes.AndroidViews.WorkshopBooking;

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

		public string court { set; get;}
		public string date { set; get;}
		public string place { set; get;}
		public string block { set; get;}
		public string workshop { set; get;}

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.hermes);

      mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_hermes);
      mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
      mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

      SetSupportActionBar(mToolbar);
      SupportActionBar.SetHomeButtonEnabled(true);
      SupportActionBar.SetDisplayHomeAsUpEnabled(true);
      
      mLeftDataSet = new List<string>();
      mLeftDataSet.Add("Reservar cancha");
      mLeftDataSet.Add("Reservar taller");
      mLeftDataSet.Add("Mis reservas");
      mLeftDataSet.Add("Configuración");
      mLeftDataSet.Add("Ayuda y comentarios");
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

		//ChangeFragment (new UserReservations ());

    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.menu_hermes, menu);
      return base.OnCreateOptionsMenu(menu);
    }
    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      //switch (item.ItemId)
      //{
      //  case Resource.Id.add:
      //    break;
      //  case Resource.Id.Remove:
      //    break;
      //}
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
			replaceFragment(new BookingCourtTypeFragment());
          break;
        case 2: //Reservar taller
			replaceFragment(new BookngWorkshopTypeFragment());
          break;
        case 3: //Mis Reserva
      		replaceFragment(new UserReservations());
          break;
        case 4: //Configuracion
          break;
        case 5: //ayuda
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
  }
}

