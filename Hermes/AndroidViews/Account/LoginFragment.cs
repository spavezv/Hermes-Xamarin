
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

namespace Hermes.AndroidViews.Account
{
  public class LoginFragment : Fragment, View.IOnClickListener
  {

    private Button btnSignup, btnLogin;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Create your fragment here

    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Resource.Layout.login, container, false);

      btnSignup = view.FindViewById<Button>(Resource.Id.btnSignup);
      btnLogin = view.FindViewById<Button>(Resource.Id.btnLogin);
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
        case Resource.Id.btnSignup :
          ((MainActivity)this.Activity).replaceFragment(new SignupFragment());
          break;
        case Resource.Id.btnLogin:
          /**
           * Debe llamar al metodo para comprobar las credenciales del usuario
           * Si el Json retorna que son correctos, iniciar sesión
           * Si no, debería avisar indicar que no fue así
           */

          url = "http://192.168.1.101:8080/HermesWS/webresources/hermes.users";
          jsonValue = await ws.GetTask(url);

          var intent = new Intent((MainActivity)this.Activity, typeof(HermesActivity));
          StartActivity(intent);
          break;
        default:
          break;
      }

    }
  }  
}

