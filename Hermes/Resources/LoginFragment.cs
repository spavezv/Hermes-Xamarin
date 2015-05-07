
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

namespace Hermes
{
	public class LoginFragment : Fragment, View.IOnClickListener
	{

		private Button _signup, _login;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.layout_login, container, false);

			_signup = view.FindViewById<Button>(Resource.Id.signup);
			_signup.SetOnClickListener (this);
			_login = view.FindViewById<Button>(Resource.Id.login);
			_login.SetOnClickListener (this);

			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			return view;
		}
			
		public void OnClick (View v)
		{

			switch (v.Id) 
			{
			case Resource.Id.signup:
				((MainActivity) this.Activity).replaceFragment(new SignupFragment ());
				break;
			case Resource.Id.login:
				break;
			default:
				break;
			}

		}
	}
}

