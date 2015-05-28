
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
          string name = etNames.Text;
          string lastname = etLastnames.Text;
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

          password = HashPassword(password);
          Clients c = new Clients {name = name, lastname = lastname, phone = phone, email = email, encryptedPassword = password};

          string json = JsonConvert.SerializeObject(c);

          Console.WriteLine(json);

          string url = GlobalVar.URL + "clients";
          WebService ws = new WebService();
          json = await ws.PostTask(url, json);

          if (json != null)
          {
            Toast.MakeText((MainActivity)this.Activity, "Bienvenido a Hermes.", ToastLength.Long).Show();
            var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
            StartActivity(intent);
          }
          else
          {
            Toast.MakeText((MainActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
          }

          
          break;
      }
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

