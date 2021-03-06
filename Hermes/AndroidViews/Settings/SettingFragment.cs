﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.Main;
using Android.Support.V7.App;
using Hermes.WebServices;

namespace Hermes
{
	public class SettingFragment : Fragment
	{
		ListView settingList;
		List<string> items = new List<string>();
		private Context mContext;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.settings_fragment, container, false);
			settingList = view.FindViewById<ListView> (Resource.Id.list_settings_fragment);
			poblateItems ();
			SettingsAdapter myAdapter = new SettingsAdapter ((AppCompatActivity)(container.Context), items);
			settingList.Adapter = myAdapter;
			mContext = Activity;
			settingList.ItemClick += (sender, e) => 
			{
				var option = items[e.Position];
				if(option.Equals("Mi cuenta"))
				{
					//SupportActionBar.SetTitle(Resource.String.Update);
					var intent = new Intent(((HermesActivity)mContext), typeof(EditCountFragment));
					((HermesActivity)mContext).StartActivity(intent);
				}
				else if(option.Equals("Sobre Hermes"))
				{
					//SupportActionBar.SetTitle(Resource.String.Update);
					var intent = new Intent(((HermesActivity)mContext), typeof(AboutActivity));
					((HermesActivity)mContext).StartActivity(intent);
				}
			};      
			return view;
		}
		void poblateItems ()
		{
			items.Add ("Mi cuenta");
			items.Add ("Sobre Hermes");
		}

	}
}

