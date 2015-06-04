using System;
using System.Collections.Generic;
using Android.Widget;

using Android.Support.V4.View;

using Android.OS;
using Android.Views;
using Android;
using Hermes.AndroidViews.CourtBooking;

using Android.Content;
using Hermes.WebServices;
using System.Json;
using Hermes.AndroidViews;
using Android.App;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;

namespace Hermes.AndroidViews.CourtBooking
{
	public class TypeFragment: Fragment, View.IOnClickListener
	{
		private List<string> courtTypesItems = new List<string> ();
		private ListView listViewCourtTypes;
		private ImageView imgLeft, imgRight;
		private TextView txtCategory;



		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			
			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewCourtTypes = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.TipodeCancha);
			imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_disable);
			imgLeft.SetImageResource (Resource.Drawable.ic_arrow_left_disable);

			listViewCourtTypes.ChoiceMode = ChoiceMode.Single;

		    poblateItemsAdapter(container);			

			listViewCourtTypes.ItemClick += (sender, e) => 
			{
				var tipoCancha = courtTypesItems[e.Position];
				((HermesActivity)this.Activity).TypeSport = tipoCancha;
				imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_available);
				imgRight.SetOnClickListener (this);

			};
			return view;
		}

    private async void poblateItemsAdapter(ViewGroup container)
    {
      WebService ws = new WebService();
      JsonValue json;
      string url;
      
      url = GlobalVar.URL + "courts/sports";
      json = await ws.GetTask(url);

      if (json != null)
      {
        //Le devuelve una lista
        JsonValue sportsResults = json["Item"];
        JsonArray ResName = (JsonArray)JsonArray.Parse(sportsResults.ToString());
        for (int i = 0; i < ResName.Count; i++)
        {
          var jsonObj = ResName[i];
          courtTypesItems.Add(jsonObj["value"]);

        }

        if (courtTypesItems.Count != 0)
        {
          CourtTypesAdapter myAdapter = new CourtTypesAdapter((AppCompatActivity)(container.Context), courtTypesItems);
          listViewCourtTypes.Adapter = myAdapter;
        }
        else 
        {
          messageEmptySport();
        }

        
        
      }
      else
      {
        //Error en la obtención del JsonValue, puede ser mal url
        Toast.MakeText((HermesActivity)this.Activity, "No hay tipo de deportes disponible", ToastLength.Long).Show();

      }
    }

    private void messageEmptySport()
    {
      Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.Activity));
      Android.App.AlertDialog alertDialog = builder.Create();
      alertDialog.SetTitle("No hay canchas disponibles.");
      alertDialog.SetMessage("Todas las canchas del sistema están reservadas");
      alertDialog.SetButton("OK", (s, ev) =>
      { 
          var intent = new Intent((HermesActivity)this.Activity, typeof(HermesActivity));
          StartActivity(intent);
          alertDialog.Dismiss();
      });
      alertDialog.Show();
    }



		public void OnClick(View v)
		{
			switch (v.Id)
			{
			case Resource.Id.img_arrow_left:
				//Toast.MakeText((MainActivity)this.Activity, "No hay izquierda", ToastLength.Long).Show();
				break;
			case Resource.Id.img_arrow_right:
				((HermesActivity)this.Activity).replaceFragment(new DateFragment());		
				break;
			default:
				break;
			}

		}
			
	}
}

