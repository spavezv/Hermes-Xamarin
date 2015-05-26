using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using Android.OS;
using Android.App;
using Android.Support.V7.App;
using Hermes.AndroidViews.WorkshopBooking;
using Hermes.AndroidViews.Main;

namespace Hermes
{
	public class BookingWorkshopHoursFragment: Fragment, View.IOnClickListener
	{
		private List<string> lstWorkHours;
		private ListView listViewWorkHours;
		private ImageView imgLeft, imgRight;
		private TextView txtCategory;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewWorkHours = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.HorasDisponibles);
			imgLeft.SetOnClickListener (this);
			listViewWorkHours.ChoiceMode = ChoiceMode.Single;
			imgRight.SetImageResource (Resource.Drawable.ic_check_disable);

			//Aqui deberia tener un json llenando esta lista
			lstWorkHours = new List<string> ();
			lstWorkHours.Add ("15:00");
			lstWorkHours.Add ("16:00");
			lstWorkHours.Add ("17:00");
			lstWorkHours.Add ("18:00");

			WorkshopTypesAdapter myAdapter= new WorkshopTypesAdapter((AppCompatActivity)(container.Context), lstWorkHours);
			listViewWorkHours.Adapter = myAdapter;

			listViewWorkHours.ItemClick += (sender, e) => 
			{
				var hora = lstWorkHours[e.Position];
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
					+ "Tipo de cancha: " + ((HermesActivity)this.Activity).workshop + "\n"
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

