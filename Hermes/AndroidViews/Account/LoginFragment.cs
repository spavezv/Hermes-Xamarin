
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

namespace Hermes.AndroidViews.Account
{
	public class LoginFragment : Fragment, View.IOnClickListener
	{

		private Button btnSignup, btnLogin;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.login, container, false);

			btnSignup = view.FindViewById<Button>(Resource.Id.btnSignup);
			btnSignup.SetOnClickListener (this);
			btnLogin = view.FindViewById<Button>(Resource.Id.btnLogin);
			btnLogin.SetOnClickListener (this);

			btnLogin.Click += (sender, e) =>
			{
				var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));

				StartActivity(intent);
			};

			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			return view;
		}
			
		public void OnClick (View v)
		{

			switch (v.Id) 
			{
			case Resource.Id.btnSignup:
				((MainActivity) this.Activity).replaceFragment(new SignupFragment ());
				break;
			case Resource.Id.btnLogin:
				break;
			default:
				break;
			}

		}
	}
}

