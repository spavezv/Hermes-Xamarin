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
using Hermes.AndroidViews.SlidingTab;
using Hermes.AndroidViews.Reservations;

namespace Hermes.AndroidViews
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

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.menu_main, menu);
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
        case 0: //Reservar cancha
          ChangeFragment(new SlidingTabsFragment());
          break;
        case 1: //Reservar taller
          break;
        case 2: //Mis Reserva
          ChangeFragment(new UserReservations());
          break;
        case 3: //Configuracion
          break;
        case 4: //ayuda
          break;
      }
    }

    public void ChangeFragment(Fragment fragment)
    {
      FragmentTransaction transaction = FragmentManager.BeginTransaction();
      transaction.Replace(Resource.Id.container, fragment);
      transaction.Commit();
      mDrawerLayout.CloseDrawers();
    }
  }
}

