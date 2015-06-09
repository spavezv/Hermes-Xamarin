
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Hermes.Models;
using Hermes.AndroidViews.Reservations;
using System.Globalization;
using Hermes.AndroidViews.Main;
using Hermes.WebServices;
using Newtonsoft.Json;

namespace Hermes
{

    public class DetailsFragment : Fragment, View.IOnClickListener
    {
        public Block block;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            block = RecyclerAdapter.mReservation;
            View view = inflater.Inflate(Resource.Layout.details_fragment, container, false);

            TextView txtBranche = view.FindViewById<TextView>(Resource.Id.txt_branche);
            TextView txtCourt = view.FindViewById<TextView>(Resource.Id.txt_court_name);
            TextView txtDateTime = view.FindViewById<TextView>(Resource.Id.txt_date_time);
            TextView txtPrice = view.FindViewById<TextView>(Resource.Id.txt_price);
            TextView txtAddress = view.FindViewById<TextView>(Resource.Id.txt_address);
            Button btnBack = view.FindViewById<Button>(Resource.Id.button_back);
            Button btnCancel = view.FindViewById<Button>(Resource.Id.button_cancel);

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

            btnBack.SetOnClickListener(this);
            btnCancel.SetOnClickListener(this);
            return view;
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.button_back:
                    ((HermesActivity)this.Activity).replaceFragment(new UserReservations());
                    break;
                case Resource.Id.button_cancel:

                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.Activity));
                    Android.App.AlertDialog alertDialog = builder.Create();
                    alertDialog.SetTitle("¿Esta seguro?");
                    alertDialog.SetMessage("Esta acción es irreversible.");
                    alertDialog.SetButton("Aceptar", (s, ev) =>
                        { bookCancel(); });
                    alertDialog.SetButton2("Cancelar", (s, ev) => { alertDialog.Dismiss(); });
                    alertDialog.Show();

                    break;
                default:
                    break;
            }

        }

        public async void bookCancel()
        {
            WebService ws = new WebService();
            block.clientId = null;
            string json = JsonConvert.SerializeObject(block);
            string url = GlobalVar.URL + "blocks/" + block.id;
            Console.WriteLine(url);
            json = await ws.PutTask(url, json);

            if (json != null)
            {
                Toast.MakeText((HermesActivity)this.Activity, "Reserva cancelada", ToastLength.Long).Show();
                this.Activity.FragmentManager.PopBackStack();
            }
            else
            {
                Toast.MakeText((HermesActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
            }
        }

    }
}

