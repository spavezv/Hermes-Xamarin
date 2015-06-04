
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

namespace Hermes.AndroidViews.Account
{
  public class LoginFragment : Fragment, View.IOnClickListener
  {

    private Button btnSignup, btnLogin;
    private EditText etMail, etPassword;
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
      etPassword = view.FindViewById<EditText>(Resource.Id.et_password);
      btnSignup = view.FindViewById<Button>(Resource.Id.btn_signup);
      btnLogin = view.FindViewById<Button>(Resource.Id.btn_login);
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
          /**
           * Debe llamar al metodo para comprobar las credenciales del usuario
           * Si el Json retorna que son correctos, iniciar sesión
           * Si no, debería avisar indicar que no fue así
           */
          email = etMail.Text;
          password = HashPassword(etPassword.Text);
          Console.WriteLine("Password: " + password);

          var progressDialog = ProgressDialog.Show((MainActivity)this.Activity, "Por favor espere", "Iniciando sesión", true);

          url = GlobalVar.URL + "clients/authenticate/" + email + "/" + password;
          json = await ws.GetTask(url);

          if (json != null)
          {

            if (json["id"] != -1)
            {
			Client = JsonConvert.DeserializeObject<Clients>(json.ToString());
              Toast.MakeText((MainActivity)this.Activity, "Bienvenido a Hermes.", ToastLength.Long).Show();
              var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
              StartActivity(intent);
            } else
            {
              Toast.MakeText((MainActivity)this.Activity, "Contraseña o correo incorrectos.", ToastLength.Long).Show();
            }
            //Esto debería ir arriba, en el if.
            
          }
          else
          {
            //Error en la obtención del JsonValue, puede ser mal url
            Toast.MakeText((MainActivity)this.Activity, "Problemas de conexión, intente más tarde.", ToastLength.Long).Show();

            var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
            StartActivity(intent);
          }
         
          break;
        default:
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

