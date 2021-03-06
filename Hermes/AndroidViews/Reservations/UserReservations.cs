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
using Android.Support.V7.Widget;
using Hermes.AndroidViews.Reservations;
using Hermes.WebServices;
using System.Json;
using Hermes.Models;
using Hermes.AndroidViews.Main;
using Newtonsoft.Json;

namespace Hermes.AndroidViews.Reservations
{
  public class UserReservations : Fragment
  {
    private RecyclerView mRecyclerView;
    private RecyclerView.LayoutManager mLayoutManager;
    private RecyclerView.Adapter mAdapter;
    private MyList<Block> mReservations;
		private TextView txtMessage;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Create your fragment here
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Resource.Layout.user_reservations, container, false);

		ISharedPreferencesEditor editor = this.Activity.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
		editor.PutString(GlobalVar.CURRENT_FRAGMENT, "USER_RESERVATIONS");
		editor.Apply();

      mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rv_reservations);
	  txtMessage = view.FindViewById<TextView> (Resource.Id.textNoReservations);
      //Create our layout manager
      mLayoutManager = new LinearLayoutManager(this.Activity);
      mRecyclerView.SetLayoutManager(mLayoutManager);
	  fillReservations ();
      return view;
    }
	


		public async void fillReservations ()
		{
			Console.WriteLine ("cargando reservas");
			mReservations = new MyList<Block>();
			WebService ws = new WebService ();
			JsonValue json;
			string url;
			Clients c = HermesActivity.Client;
            ISharedPreferences prefs = this.Activity.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);

			url = GlobalVar.URL + "clients/blocks/" + prefs.GetInt(GlobalVar.USER_ID, -1).ToString();
			json = await ws.GetTask (url);
			if (json != null) {
				
				if(!(json.ToString().Equals("[]")))
				{
					Console.WriteLine ("RECIBIDO" + json.ToString());
					List<Block> userReservations = JsonConvert.DeserializeObject<List<Block>> (json.ToString ());
					for (int i = 0; i < userReservations.Count; i++) {
						mReservations.Add (userReservations[i]);
					}

					mAdapter = new RecyclerAdapter (mReservations, mRecyclerView, Activity);
					mReservations.Adapter = mAdapter;
					mRecyclerView.SetAdapter (mAdapter);

					mRecyclerView.Visibility = ViewStates.Visible;
					txtMessage.Visibility = ViewStates.Invisible;
				}

				else {
					//messageNoReservations ();


					mRecyclerView.Visibility = ViewStates.Invisible;
					txtMessage.Visibility = ViewStates.Visible;

				}
			} else {
				Toast.MakeText((HermesActivity)this.Activity, "Problema de conexion", ToastLength.Long).Show();
			}
		}

		void messageNoReservations ()
		{
			Toast.MakeText((HermesActivity)this.Activity, "No hay reservas", ToastLength.Long).Show();
		}
  }
}
