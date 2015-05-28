using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;
using Android.OS;
using Hermes.Models;
using Hermes.WebServices;
using System.Json;
using Newtonsoft.Json;

namespace Hermes
{
	public class BookingCourtHoursFragment: Fragment, View.IOnClickListener
	{
		private List<Block> lstCourtHours;
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

      poblateItemsAdapter(container);
			listViewCourtHours.ItemClick += (sender, e) => 
			{
        ((HermesActivity)this.Activity).mBlock = lstCourtHours[e.Position];

				imgRight.SetImageResource (Resource.Drawable.ic_check_available);
				imgRight.SetOnClickListener (this);
			};
			//


			return view;
		}

    private async void poblateItemsAdapter(ViewGroup container)
    {
      WebService ws = new WebService();
      string url = GlobalVar.URL + "blocks/getBlocks/" + ((HermesActivity)this.Activity).TypeSport + "/" + ((HermesActivity)this.Activity).Date + "/" + ((HermesActivity)this.Activity).mBranch.id;
      JsonValue json = await ws.GetTask(url);

      if (json != null)
      {
        lstCourtHours = JsonConvert.DeserializeObject<List<Block>>(json.ToString());

        if (lstCourtHours.Count != 0)
        {
          HourListAdapter myAdapter = new HourListAdapter((AppCompatActivity)(container.Context), lstCourtHours);
          listViewCourtHours.Adapter = myAdapter;
        }
        else
        {
          messageEmptySport();
        }
      }

    }

    private void messageEmptySport()
    {
      throw new NotImplementedException();
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
				+ "Tipo de cancha: " + ((HermesActivity)this.Activity).TypeSport + "\n"
					+ "Fecha: " + ((HermesActivity)this.Activity).DateEsp + "\n"
          + "Recinto: " + ((HermesActivity)this.Activity).mBranch.businessId.name + "\n"
          + "Hora: " + ((HermesActivity)this.Activity).mBlock.start + " hrs. a " + ((HermesActivity)this.Activity).mBlock.finish + " hrs." );
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

