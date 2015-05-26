using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Android.Support.V7.App;
using Hermes.AndroidViews.WorkshopBooking;
using Hermes.AndroidViews.Main;
using Android.OS;

namespace Hermes
{
	public class BookingWorkshopNamesFragment: Fragment, View.IOnClickListener
	{
		private List<string> lstWorkNames;
		public ListView listViewWorkNames;
		private ImageView imgLeft, imgRight;
		private TextView txtCategory;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewWorkNames = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.RecintosDisponibles);
			imgLeft.SetOnClickListener (this);
			imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_disable);

			//Aqui deberia tener un json llenando esta lista
			lstWorkNames = new List<string> ();
			lstWorkNames.Add ("Gym 1");
			lstWorkNames.Add ("Gym 2");
			lstWorkNames.Add ("Gym 3");
			lstWorkNames.Add ("Gym 4");

			WorkshopTypesAdapter myAdapter= new WorkshopTypesAdapter((AppCompatActivity)(container.Context), lstWorkNames);
			listViewWorkNames.Adapter = myAdapter;

			listViewWorkNames.ItemClick += (sender, e) => 
			{
				var recinto = lstWorkNames[e.Position];
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
				((HermesActivity)this.Activity).replaceFragment(new BookngWorkshopDateFragment());	
				break;
			case Resource.Id.img_arrow_right:
				//Dialog
				((HermesActivity)this.Activity).replaceFragment(new BookingWorkshopHoursFragment());
				break;

			default:
				break;
			}

		}
	}
}

