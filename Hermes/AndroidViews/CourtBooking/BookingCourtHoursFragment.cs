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
	public class BookingCourtHoursFragment: Fragment, View.IOnClickListener
	{
		private List<string> lstCourtHours;
		private ListView listViewCourtHours;
		private ImageView imgLeft, imgRight;
		private TextView txtCategory;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewCourtHours = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.HorasDisponibles);
			imgLeft.SetOnClickListener (this);
			listViewCourtHours.ChoiceMode = ChoiceMode.Single;
			imgRight.SetImageResource (Resource.Drawable.ic_check_disable);

			//Aqui deberia tener un json llenando esta lista
			lstCourtHours = new List<string> ();
			lstCourtHours.Add ("15:00");
			lstCourtHours.Add ("16:00");
			lstCourtHours.Add ("17:00");
			lstCourtHours.Add ("18:00");

			HourListAdapter myAdapter= new HourListAdapter((AppCompatActivity)(container.Context), lstCourtHours);
			listViewCourtHours.Adapter = myAdapter;

			listViewCourtHours.ItemClick += (sender, e) => 
			{
				var hora = lstCourtHours[e.Position];
				((HermesActivity)this.Activity).block = hora;
				imgRight.SetImageResource (Resource.Drawable.ic_check_available);
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
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtNamesFragment());	
				break;
			case Resource.Id.img_arrow_right:
				Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder (((HermesActivity)this.Activity));
				Android.App.AlertDialog alertDialog = builder.Create ();
				alertDialog.SetTitle ("¿Realizar reserva?");
				alertDialog.SetMessage ("Su reservacion es: \n"
				+ "Tipo de cancha: " + ((HermesActivity)this.Activity).court + "\n"
					+ "Fecha: " + ((HermesActivity)this.Activity).date + "\n"
					+ "Recinto: " + ((HermesActivity)this.Activity).place + "\n"
					+ "Hora: " + ((HermesActivity)this.Activity).block);
				alertDialog.SetButton("Aceptar", (s, ev) => 
					{/*do something*/});
				alertDialog.SetButton2("Cancelar", (s, ev) => {alertDialog.Dismiss();});
				alertDialog.Show();
				break;

			default:
				break;
			}

		}
	}
}

