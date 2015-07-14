
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
using Hermes.AndroidViews.Main;
using Android.Support.V7.App;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Hermes
{
	[Activity(Label = "Sobre Hermes", Theme = "@style/MyTheme", ParentActivity = typeof(HermesActivity))]
	public class AboutActivity : AppCompatActivity
	{
		private SupportToolbar mToolbar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.about_hermes);

			mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_about_hermes);
			SetSupportActionBar(mToolbar);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			TextView txtAbout = FindViewById<TextView> (Resource.Id.textAbout);
			txtAbout.Text = "Hermes creado en Xamarin";
		}
	}
}

