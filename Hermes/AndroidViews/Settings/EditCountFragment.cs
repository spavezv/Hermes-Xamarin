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
using Android.Support.V7.App;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Hermes
{
	[Activity(Label = "Hermes", Theme = "@style/MyTheme", ParentActivity = typeof(HermesActivity))]
	public class EditCountFragment: AppCompatActivity, View.IOnClickListener
	{
		private EditText etNames, etLastnames, etEmail, etPhone, etPassword, etPasswordConfirmation;
		private Button btnEdit;
		private EditText edPassword;
		private string passHash;
		static ISharedPreferences prefs;
		private SupportToolbar mToolbar;
		public Block block;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.edit_account);

			mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_edit_account);
			SetSupportActionBar(mToolbar);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			etNames = FindViewById<EditText>(Resource.Id.et_names);
			etLastnames = FindViewById<EditText>(Resource.Id.et_lastnames);
			etEmail = FindViewById<EditText>(Resource.Id.et_email);
			etPhone = FindViewById<EditText>(Resource.Id.et_phone);
			etPassword = FindViewById<EditText>(Resource.Id.et_password);
			etPassword.Enabled = false;
			etPassword.Visibility = ViewStates.Invisible;
			etPasswordConfirmation = FindViewById<EditText>(Resource.Id.et_password_confirmation);
			etPasswordConfirmation.Enabled = false;
			etPasswordConfirmation.Visibility = ViewStates.Invisible;
			btnEdit = FindViewById<Button>(Resource.Id.btn_signup);
			btnEdit.Text = "Actualizar";
			btnEdit.SetOnClickListener(this);

			prefs = (this).GetSharedPreferences (GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
			ISharedPreferencesEditor editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
			editor.PutBoolean(GlobalVar.EDIT_ACCOUNT, true);
			editor.Apply();

			var userID = prefs.GetInt (GlobalVar.USER_ID, -1);
			etNames.Text = prefs.GetString (GlobalVar.USER_NAMES, null);
			etLastnames.Text = prefs.GetString(GlobalVar.USER_LASTNAMES, null);
			etEmail.Text = prefs.GetString (GlobalVar.USER_EMAIL, null);
			etPhone.Text =prefs.GetString(GlobalVar.USER_PHONE, null);

		}
		
		public async void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.btn_signup:
					Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);

					var inflater = this.LayoutInflater;
					var dialogView = inflater.Inflate(Resource.Layout.confirmation_pass_dialog, null);

				if (dialogView != null) {
						builder.SetTitle("Confirmar actualización:");
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
							Toast.MakeText(this, "Error de contraseña", ToastLength.Long).Show();
						}
					});


				builder.SetNegativeButton("Cancelar", (obj, e) => { });
				builder.Show();
				break;
			}
		}

		async void editClient ()
		{
			prefs = this.GetSharedPreferences (GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
			Clients c = new Clients {id= prefs.GetInt(GlobalVar.USER_ID, -1), name = etNames.Text, lastname = etLastnames.Text, phone = etPhone.Text, email = etEmail.Text, encryptedPassword = HashPassword(etPassword.Text),
				createdAt = prefs.GetString(GlobalVar.USER_CREATED, null) , updatedAt = prefs.GetString(GlobalVar.USER_UPDATED, null)  };
			string json = JsonConvert.SerializeObject(c);
			string url = GlobalVar.URL + "clients/" + prefs.GetInt(GlobalVar.USER_ID, -1);
			WebService ws = new WebService();
			json = await ws.PutTask(url, json);

			if (json != null)
			{
				Toast.MakeText(this, "Actualización realizada", ToastLength.Long).Show();
			}
			else
			{
				Toast.MakeText(this, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
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

