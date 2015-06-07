
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.Main;
using Hermes.Models;
using Hermes.WebServices;
using Newtonsoft.Json;
using Java.Util.Regex;
using Android.Graphics.Drawables;

namespace Hermes.AndroidViews.Account
{
    public class SignupFragment : Fragment, View.IOnClickListener
    {

        private EditText etNames, etLastnames, etEmail, etPhone, etPassword, etPasswordConfirmation;
        private Button btnSignup;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.signup, container, false);
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
                    Console.WriteLine("Iniciando sesión.");

                    if (!haveErrors())
                    {
                        Clients c = new Clients { name = etNames.Text, lastname = etLastnames.Text, phone = etPhone.Text, email = etEmail.Text, encryptedPassword = HashPassword(etPassword.Text) };

                        string json = JsonConvert.SerializeObject(c);

                        string url = GlobalVar.URL + "clients";
                        WebService ws = new WebService();
                        json = await ws.PostTask(url, json);

                        if (json != null)
                        {
                            Toast.MakeText((MainActivity)this.Activity, "Cuenta creada, ahora puede iniciar sesión.", ToastLength.Long).Show();
                            ((MainActivity)this.Activity).replaceFragment(new LoginFragment());
                        }
                        else
                        {
                            Toast.MakeText((MainActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
                        }
                    }
                    break;
            }
        }


        bool isValidPhone(string phone)
        {
            const string phone_pattern = "[6-9][0-9]{7}";

            Java.Util.Regex.Pattern pattern = Java.Util.Regex.Pattern.Compile(phone_pattern);
            Matcher matcher = pattern.Matcher(phone);
            return matcher.Matches();
        }

        private bool haveErrors()
        {
            Console.WriteLine("REVISANDO ERRORES");

            bool errors = false;
            string name = etNames.Text.ToString();
            string email = etEmail.Text.ToString();

            if (string.IsNullOrEmpty(name))
            {
                etNames.Error = "No puede estar vacío";
                errors = true;
            }
            if (!GlobalVar.IsValidEmail(email))
            {
                etEmail.Error = "No es una dirección de correo válida";
                errors = true;
            }
            string m = (errors) ? "HAY ERROR" : "NO HAY ERROR";
            Console.WriteLine(m);
            return errors;
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

