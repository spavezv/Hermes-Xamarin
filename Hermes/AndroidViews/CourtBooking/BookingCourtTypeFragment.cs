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
	public class BookingCourtTypeFragment: Fragment, View.IOnClickListener
	{
		private List<string> courtTypesItems;
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

			//Aqui deberia tener un json llenando esta lista
			courtTypesItems = new List<string> ();
			courtTypesItems.Add ("Futbol");
			courtTypesItems.Add ("Tenis");
			courtTypesItems.Add ("Voleyball");
			courtTypesItems.Add ("Basquetball");

			CourtTypesAdapter myAdapter= new CourtTypesAdapter((AppCompatActivity)(container.Context), courtTypesItems);
			listViewCourtTypes.Adapter = myAdapter;

			listViewCourtTypes.ItemClick += (sender, e) => 
			{
				//capturar el item
				imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_available);
				imgRight.SetOnClickListener (this);

			};
//


			return view;
		}



		public void OnClick(View v)
		{
			switch (v.Id)
			{
			case Resource.Id.img_arrow_left:
				//Toast.MakeText((MainActivity)this.Activity, "No hay izquierda", ToastLength.Long).Show();
				break;
			case Resource.Id.img_arrow_right:
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtDateFragment());		
				break;
			default:
				break;
			}

		}
			
	}
}

