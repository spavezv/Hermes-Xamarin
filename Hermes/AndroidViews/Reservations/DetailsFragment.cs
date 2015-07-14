
using System;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Hermes.Models;
using Hermes.AndroidViews.Reservations;
using System.Globalization;
using Hermes.AndroidViews.Main;
using Hermes.WebServices;
using Newtonsoft.Json;
using Android.Support.V7.App;
using Android.Content;

namespace Hermes
{
	[Activity(Label = "Hermes", Theme = "@style/MyTheme", ParentActivity = typeof(HermesActivity))]
	public class DetailsFragment : AppCompatActivity
    {

		private SupportToolbar mToolbar;
        public Block block;

		protected override void OnCreate(Bundle bundle)
        {
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.details_fragment);

			mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_reservation_details);
			SetSupportActionBar(mToolbar);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            block = RecyclerAdapter.mReservation;

            TextView txtBranche = FindViewById<TextView>(Resource.Id.txt_branche);
            TextView txtCourt = FindViewById<TextView>(Resource.Id.txt_court_name);
            TextView txtDateTime = FindViewById<TextView>(Resource.Id.txt_date_time);
            TextView txtPrice = FindViewById<TextView>(Resource.Id.txt_price);
            TextView txtAddress = FindViewById<TextView>(Resource.Id.txt_address);

            String inicio = block.start.Substring(11, 5);
            String termino = block.finish.Substring(11, 5);
            DateTime date = DateTime.Parse(block.date);
            string dateReservation = date.ToString("d MMMM",
                CultureInfo.CreateSpecificCulture("es-MX"));

            txtBranche.Text = block.courtId.branchId.businessId.name;
            txtCourt.Text = block.courtId.name;
            txtDateTime.Text = dateReservation + " de " + inicio + " a " + termino + " hrs.";
            txtPrice.Text = "$" + block.price.ToString();
            txtAddress.Text = block.courtId.branchId.street + block.courtId.branchId.number;
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_reservation_details, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Resource.Id.ic_action_delete:
				Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
				Android.App.AlertDialog alertDialog = builder.Create();
				alertDialog.SetTitle("¿Esta seguro?");
				alertDialog.SetMessage("Esta acción es irreversible.");
				alertDialog.SetButton("Aceptar", (s, ev) =>
					{ 
						bookCancel();
						ISharedPreferencesEditor editor = this.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private).Edit();
						editor.PutString(GlobalVar.CURRENT_FRAGMENT, "USER_RESERVATIONS");
						editor.Apply();
						//Finish();
						OnBackPressed();
					});
				alertDialog.SetButton2("Cancelar", (s, ev) => { alertDialog.Dismiss(); });
				alertDialog.Show();
				break;
			}
			return base.OnOptionsItemSelected(item);
		}

        public async void bookCancel()
        {
            WebService ws = new WebService();
            block.clientId = null;
            string json = JsonConvert.SerializeObject(block);
            string url = GlobalVar.URL + "blocks/" + block.id;
            json = await ws.PutTask(url, json);
            

            if (json != null)
            {
                //Toast.MakeText((HermesActivity)this.Activity, "Reserva cancelada", ToastLength.Long).Show();
                //this.Activity.FragmentManager.PopBackStack();
            }
            else
            {
                //Toast.MakeText((HermesActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
            }
        }

    }
}

