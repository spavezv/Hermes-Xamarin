
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hermes.WebServices;

namespace Hermes.AndroidViews.Account
{
  public class SignupFragment : Fragment, View.IOnClickListener
	{

    private EditText etNames, etLastnames, etEmail, etPhone, etPassword, etPasswordConfirmation;
    private Button btnSignup;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.signup, container, false);
      etNames = view.FindViewById<EditText>(Resource.Id.et_names);
      etLastnames = view.FindViewById<EditText>(Resource.Id.et_lastnames);
      etEmail = view.FindViewById<EditText>(Resource.Id.et_email);
      etPhone = view.FindViewById<EditText>(Resource.Id.et_phone);
      etPassword = view.FindViewById<EditText>(Resource.Id.et_password);
      etPasswordConfirmation = view.FindViewById<EditText>(Resource.Id.et_password_confirmation);
      btnSignup = view.FindViewById<Button>(Resource.Id.btn_signup);
      btnSignup.SetOnClickListener(this);

			return view;
		}

    public async void OnClick(View v)
    {
      switch (v.Id)
      {
        case Resource.Id.btn_signup:
          string names = etNames.Text;
          string lastnames = etLastnames.Text;
          string email = etEmail.Text;
          string phone = etPhone.Text;
          string password = etPassword.Text;
          string passwordConfirmation = etPasswordConfirmation.Text;

          /**
           * Se deben validar los datos:
           * 1. Que todos los campos esten ingresados.
           * 2. Que el email tenga el formato correcto.
           * 3. Que el teléfono tenga el formato correcto.
           * 4. Que la contraseña sea mayor que 6 carácteres.
           * 5. Que las contraseñas coincidan.
           */

          string url = GlobalVar.URL + "clients/authenticate/" + email + "/" + password;
          JsonValue json = await new WebService().GetTask(url);

          if (json != null)
          {

          } else
          {

          }
          break;
      }
    }

	}
}

