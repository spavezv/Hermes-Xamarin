﻿
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
using Android.Support.V7.App;

namespace Hermes
{
	[Activity (Label = "CourtBookingActivity")]			
	public class CourtBookingActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.tab_page);

			FragmentTransaction transaction = FragmentManager.BeginTransaction();
			SlidingTabsFragment fragment = new SlidingTabsFragment ();
			transaction.Replace(Resource.Id.sample_content_fragment, fragment);
			transaction.Commit();
		}
	}
}

