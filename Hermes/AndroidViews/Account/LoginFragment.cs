
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.Main;
using System.Net.Http;
using Hermes.WebServices;
using System.Security.Cryptography;
using Hermes.Models;
using Newtonsoft.Json;
using Android.Preferences;
using Java.Text;

namespace Hermes.AndroidViews.Account
{
    public class LoginFragment : Fragment, View.IOnClickListener
    {

        private Button btnSignup, btnLogin;
        private EditText etMail, etPassword;
        private CheckBox checkbox;
        private String email, password;
        public static Clients Client;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.login, container, false);

            etMail = view.FindViewById<EditText>(Resource.Id.et_email);
			etMail.Text = SignupFragment.email;
            etPassword = view.FindViewById<EditText>(Resource.Id.et_password);
			etPassword.Text = SignupFragment.password;
            btnSignup = view.FindViewById<Button>(Resource.Id.btn_signup);
            btnLogin = view.FindViewById<Button>(Resource.Id.btn_login);
            checkbox = view.FindViewById<CheckBox>(Resource.Id.chk_session);
            btnSignup.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);

            return view;
        }

        public async void OnClick(View v)
        {
            WebService ws = new WebService();
            JsonValue json;
            string url;

            switch (v.Id)
            {
                case Resource.Id.btn_signup:
                    ((MainActivity)this.Activity).replaceFragment(new SignupFragment());
                    break;
                case Resource.Id.btn_login:


                    if (!haveErrors())
                    {

                        email = etMail.Text;
                        password = HashPassword(etPassword.Text);
                        var progressDialog = ProgressDialog.Show((MainActivity)this.Activity, "Por favor espere", "Iniciando sesión", true);
                        url = GlobalVar.URL + "clients/authenticate/" + email + "/" + password;
                        json = await ws.GetTask(url);

                        if (json != null)
                        {
                            if (json["id"] != -1)
                            {
                                Clients c = JsonConvert.DeserializeObject<Clients>(json.ToString());
                                SavePreferences(c);
                                var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
                                Toast.MakeText((MainActivity)this.Activity, "Bienvenido a Hermes.", ToastLength.Long).Show();
                                StartActivity(intent);
                                this.Activity.Finish();

                            }
                            else
                            {
                                Toast.MakeText((MainActivity)this.Activity, "Contraseña o correo equivocado.", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            Toast.MakeText((MainActivity)this.Activity, "Problemas de conexión, intente más tarde.", ToastLength.Long).Show();
                            var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
                            StartActivity(intent);
                            this.Activity.Finish();
                        }

                        progressDialog.Dismiss();
                    }
                    break;
                default:
                    break;
            }

        }

        void SavePreferences(Clients c)
        {
            ISharedPreferences prefs = this.Activity.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();

            editor.PutInt(GlobalVar.USER_ID, c.id);
            editor.PutBoolean(GlobalVar.REMEMBER_USER, checkbox.Checked);
            editor.PutString(GlobalVar.USER_EMAIL, c.email);
            editor.PutString(GlobalVar.USER_PASSWORD, c.encryptedPassword);
			editor.PutString (GlobalVar.USER_NAMES, c.name);
			editor.PutString (GlobalVar.USER_LASTNAMES, c.lastname);
			editor.PutString (GlobalVar.USER_PHONE, c.phone);
			editor.PutString (GlobalVar.USER_CREATED, c.createdAt);
			editor.PutString (GlobalVar.USER_UPDATED, c.updatedAt);
            editor.Apply();        // applies changes asynchronously on newer APIs

            var remember = (checkbox.Checked) ? "Recuerdame " : "NO Recuerdame ";
        }

	    public String HashPassword(String password)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            password = BitConverter.ToString(hashedBytes);
            return password;
        }

        public bool haveErrors()
        {
            bool errors = false;
            string email = etMail.Text.ToString();
            string password = etPassword.Text.ToString();


            if (!GlobalVar.IsValidEmail(email)) {
                etMail.Error = "No es una dirección de correo válida";
                errors = true;
            }
            if (password.Length < 8)
            {
                etPassword.Error = "La contraseña debe tener al menos 8 caracteres";
                errors = true;
            }
            return errors;
        }
    }

}

