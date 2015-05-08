using Android.Support.V4.Widget;
using Android.Support.V7.App;
using System;
using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;

namespace Hermes
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
		private AppCompatActivity  mHostActivity;
        private int mOpenedResource;
        private int mClosedResource;
		public MyActionBarDrawerToggle(AppCompatActivity  host, DrawerLayout drawerLayout, int openedResource, int closedResource)
            : base(host, drawerLayout, openedResource, closedResource)
        {
            mHostActivity = host;
            mOpenedResource = openedResource;
            mClosedResource = closedResource;
        }

        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}