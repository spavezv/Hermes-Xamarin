
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

namespace Hermes.AndroidViews.Account
{
  public class LoginFragment : Fragment, View.IOnClickListener
  {

    private Button btnSignup, btnLogin;
    private EditText et_mail, et_password;
    private String email, password;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Create your fragment here

    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Resource.Layout.login, container, false);

      et_mail = view.FindViewById<EditText>(Resource.Id.et_email);
      et_password = view.FindViewById<EditText>(Resource.Id.et_password);
      btnSignup = view.FindViewById<Button>(Resource.Id.btn_signup);
      btnLogin = view.FindViewById<Button>(Resource.Id.btn_login);
      btnSignup.SetOnClickListener(this);
      btnLogin.SetOnClickListener(this);

      return view;
    }

    public async void OnClick(View v)
    {
      WebService ws = new WebService();
      JsonValue jsonValue;
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
          email = et_mail.Text;
          password = HashPassword(et_password.Text);
          Console.WriteLine("Password: " + password);

          url = GlobalVar.URL + "hermes.users";
          jsonValue = await ws.GetTask(url);

          if (jsonValue != null)
          {
            var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
            StartActivity(intent);
          }
          else
          {
            //Error en la obtención del JsonValue
            Console.WriteLine("ES NULL");
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

