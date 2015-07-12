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
using Android.Content;

namespace Hermes
{
    public class BlocksFragment : Fragment, View.IOnClickListener
    {
        private List<Block> lstCourtHours;
        List<List<Block>> superList;
        private ExpandableListView expListViewCourts;
        private ImageView imgLeft, imgRight;
        private TextView txtCategory;
        public HourListAdapter myAdapter;
        public Block selectedBlock;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			View view = inflater.Inflate(Resource.Layout.expandable_list, container, false);
            imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
            imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
            txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
            expListViewCourts = view.FindViewById<ExpandableListView>(Resource.Id.exp_list_fragment);

            txtCategory.SetText(Resource.String.HorasDisponibles);
            imgLeft.SetOnClickListener(this);
            imgRight.SetImageResource(Resource.Drawable.ic_check_disable);
            poblateItemsAdapter(container);

            expListViewCourts.ChildClick += (object sender, ExpandableListView.ChildClickEventArgs e) =>
            {
                superList = myAdapter.superList;
                selectedBlock = superList[e.GroupPosition][e.ChildPosition];
                ((HermesActivity)this.Activity).mBlock = selectedBlock;
                imgRight.SetImageResource(Resource.Drawable.ic_check_available);
                imgRight.SetOnClickListener(this);
            };
            return view;

        }


        private async void poblateItemsAdapter(ViewGroup container)
        {
			WebService ws = new WebService();
            string url = GlobalVar.URL + "blocks/getBlocks/" + ((HermesActivity)this.Activity).TypeSport + "/" + ((HermesActivity)this.Activity).Date + "/" + ((HermesActivity)this.Activity).mBranch.id;
            Console.WriteLine(url);
            JsonValue json = await ws.GetTask(url);
            if (json != null)
            {
                lstCourtHours = JsonConvert.DeserializeObject<List<Block>>(json.ToString());
                if (lstCourtHours.Count != 0)
                {
                    myAdapter = new HourListAdapter((AppCompatActivity)(container.Context), lstCourtHours);
					expListViewCourts.SetAdapter(myAdapter);
                }
                else
                {
                    messageEmptySport();
                }
            }

        }

        private void messageEmptySport()
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.Activity));
            Android.App.AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Sin horas disponibles.");
            alertDialog.SetMessage("No existen horas disponibles en la sucursal escogida.");
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
                    ((HermesActivity)this.Activity).replaceFragment(new BranchFragment(), "");
                    break;
                case Resource.Id.img_arrow_right:
                    String inicio = selectedBlock.start.Substring(11, 5);
                    String termino = selectedBlock.finish.Substring(11, 5);

                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.Activity));
                    Android.App.AlertDialog alertDialog = builder.Create();
                    alertDialog.SetTitle("Detalles de su reserva:");
                    alertDialog.SetMessage("Tipo de cancha: " + ((HermesActivity)this.Activity).TypeSport + "\n"
                        + "Fecha: " + ((HermesActivity)this.Activity).DateEsp + "\n"
              + "Recinto: " + ((HermesActivity)this.Activity).mBranch.businessId.name + "\n"
              + "Horario: " + inicio + " hrs. a " + termino + " hrs.");
                    alertDialog.SetButton("Reservar", (s, ev) =>
                        { bookCourt(); });
                    alertDialog.SetButton2("Cancelar", (s, ev) => { alertDialog.Dismiss(); });
                    alertDialog.Show();
                    break;

                default:
                    break;
            }

        }

        public async void bookCourt()
        {
            ISharedPreferences prefs = this.Activity.GetSharedPreferences(GlobalVar.HERMES_PREFERENCES, Android.Content.FileCreationMode.Private);
            selectedBlock.clientId = new Clients { id = prefs.GetInt(GlobalVar.USER_ID, -1) };

            string json = JsonConvert.SerializeObject(selectedBlock);

            WebService ws = new WebService();
            string url = GlobalVar.URL + "blocks/" + selectedBlock.id;
            json = await ws.PutTask(url, json);

            if (json != null)
            {
                Toast.MakeText((HermesActivity)this.Activity, "Reserva realizada", ToastLength.Long).Show();
                var intent = new Intent((HermesActivity)this.Activity, typeof(HermesActivity));
                StartActivity(intent);

            }
            else
            {
                Toast.MakeText((HermesActivity)this.Activity, "Problemas de conexión. Intente más tarde.", ToastLength.Long).Show();
            }
        }
    }
}

