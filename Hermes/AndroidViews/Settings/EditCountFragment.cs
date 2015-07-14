using System;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Hermes.AndroidViews.Main;
using Hermes.WebServices;
using Hermes.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Hermes.AndroidViews;

namespace Hermes
{
	public class EditCountFragment: Fragment, View.IOnClickListener
	{
		private EditText etNames, etLastnames, etEmail, etPhone, etPassword, etPasswordConfirmation;
		private Button btnEdit;
		private EditText edPassword;
		private string passHash;
		static ISharedPreferences prefs;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.signup, container, false);
			etNames = view.FindViewById<EditText>(Resource.Id.et_names);
			etLastnames = view.FindViewById<EditText>(Resource.Id.et_lastnames);
			etEmail = view.FindViewById<EditText>(Resource.Id.et_email);
			etPhone = view.FindViewById<EditText>(Resource.Id.et_phone);
			etPassword = view.FindViewById<EditText>(Resource.Id.et_password);
			etPassword.Enabled = false;
			etPassword.Visibility = ViewStates.Invisible;
			etPasswordConfirmation = view.FindViewById<EditText>(Resource.Id.et_password_confirmation);
			etPasswordConfirmation.Enabled = false;
			etPasswordConfirmation.Visibility = ViewStates.Invisible;
			btnEdit = view.FindViewById<Button>(Resource.Id.btn_signup);
			btnEdit.Text = "Actualizar";
			btnEdit.SetOnClickListener(this);

			prefs = ((HermesActivity)this.Activity).GetSharedPreferences (GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);

			var userID = prefs.GetInt (GlobalVar.USER_ID, -1);
			etNames.Text = prefs.GetString (GlobalVar.USER_NAMES, null);
			etLastnames.Text = prefs.GetString(GlobalVar.USER_LASTNAMES, null);
			etEmail.Text = prefs.GetString (GlobalVar.USER_EMAIL, null);
			etPhone.Text =prefs.GetString(GlobalVar.USER_PHONE, null);

			return view;
		}
		
		public async void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.btn_signup:

					Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.Activity));
					var inflater = Activity.LayoutInflater;
					var dialogView = inflater.Inflate(Resource.Layout.confirmation_pass_dialog, null);

					if (dialogView != null) {
						builder.SetTitle("Confirmar edición:");
						edPassword = dialogView.FindViewById<EditText>(Resource.Id.editText_Pwd1);
						builder.SetView(dialogView);
					}
					
				builder.SetPositiveButton(
					"Aceptar",
					(obj, e) => 
					{
						if (edPassword.Text != string.Empty && searchPassword(edPassword.Text))
						{
							editClient();
						}

						else
						{
							Toast.MakeText((HermesActivity)this.Activity, "Error de contraseña", ToastLength.Long).Show();
						}
					});


				builder.SetNegativeButton("Cancelar", (obj, e) => { });
				builder.Show();
				break;
			}
		}

		async void editClient ()
		{
			prefs = ((HermesActivity)this.Activity).GetSharedPreferences (GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
			Clients c = new Clients {id= prefs.GetInt(GlobalVar.USER_ID, -1), name = etNames.Text, lastname = etLastnames.Text, phone = etPhone.Text, email = etEmail.Text, encryptedPassword = HashPassword(etPassword.Text),
				createdAt = prefs.GetString(GlobalVar.USER_CREATED, null) , updatedAt = prefs.GetString(GlobalVar.USER_UPDATED, null)  };
			string json = JsonConvert.SerializeObject(c);
			string url = GlobalVar.URL + "clients/" + prefs.GetInt(GlobalVar.USER_ID, -1);
			WebService ws = new WebService();
			json = await ws.PutTask(url, json);

			if (json != null)
			{
				Toast.MakeText((HermesActivity)this.Activity, "Actualización realizada", ToastLength.Long).Show();
//				var intent = new Intent((HermesActivity)this.Activity, typeof(HermesActivity));
//				StartActivity(intent);

			}
			else
			{
				Toast.MakeText((MainActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
			}
		}

		bool searchPassword (string text)
		{
			var hash = HashPassword (text);
			return (hash.Equals(prefs.GetString(GlobalVar.USER_PASSWORD, null)));
		}

		public String HashPassword(String password)
		{
			HashAlgorithm algorithm = new SHA256Managed();
			Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
			Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
			password = BitConverter.ToString(hashedBytes);
			return password;
		}
	}
}

