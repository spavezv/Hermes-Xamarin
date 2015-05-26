using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;
using Android.OS;

namespace Hermes
{
	public class BookingCourtNamesFragment: Fragment, View.IOnClickListener
	{
			private List<string> lstCourtNames;
			public ListView listViewCourtNames;
			private ImageView imgLeft, imgRight;
			private TextView txtCategory;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewCourtNames = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.RecintosDisponibles);
			imgLeft.SetOnClickListener (this);
			imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_disable);

			//Aqui deberia tener un json llenando esta lista
			lstCourtNames = new List<string> ();
			lstCourtNames.Add ("Cancha 1");
			lstCourtNames.Add ("Cancha 2");
			lstCourtNames.Add ("Cancha 3");
			lstCourtNames.Add ("Cancha 4");

			BranchListAdapter myAdapter= new BranchListAdapter((AppCompatActivity)(container.Context), lstCourtNames);
			listViewCourtNames.Adapter = myAdapter;

			listViewCourtNames.ItemClick += (sender, e) => 
			{
				var recinto = lstCourtNames[e.Position];
				((HermesActivity)this.Activity).place = recinto;
				imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_available);
				imgRight.SetOnClickListener (this);
			};

			return view;
		}

		public void OnClick(View v)
		{
			switch (v.Id)
			{
			case Resource.Id.img_arrow_left:
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtDateFragment());	
				break;
			case Resource.Id.img_arrow_right:
				//Dialog
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtHoursFragment());
				break;
			
			default:
				break;
			}

		}
	}
}

