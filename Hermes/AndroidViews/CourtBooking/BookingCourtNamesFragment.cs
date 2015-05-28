using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;
using Android.OS;
using Hermes.WebServices;
using System.Json;
using Hermes.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.Content;

namespace Hermes
{
  public class BookingCourtNamesFragment : Fragment, View.IOnClickListener
  {
    public List<Branches> lstBranches;
    public ListView listViewBranchesNames;
    private ImageView imgLeft, imgRight;
    private TextView txtCategory;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {

      View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
      imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
      imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
      txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
      listViewBranchesNames = view.FindViewById<ListView>(Resource.Id.list_booking_fragment);

      txtCategory.SetText(Resource.String.RecintosDisponibles);
      imgLeft.SetOnClickListener(this);
      imgRight.SetImageResource(Resource.Drawable.ic_arrow_right_disable);

      poblateItemsAdapter(container);


      listViewBranchesNames.ItemClick += (sender, e) =>
      {
        Branches branch = lstBranches[e.Position];
        ((HermesActivity)this.Activity).mBranch = branch;
        imgRight.SetImageResource(Resource.Drawable.ic_arrow_right_available);
        imgRight.SetOnClickListener(this);
      };

      return view;
    }

    private async void poblateItemsAdapter(ViewGroup container)
    {
      WebService ws = new WebService();
      string url = GlobalVar.URL + "branches/getBranches/" + ((HermesActivity)this.Activity).TypeSport + "/" + ((HermesActivity)this.Activity).Date;
      JsonValue json = await ws.GetTask(url);

      if (json != null)
      {
        lstBranches = JsonConvert.DeserializeObject<List<Branches>>(json.ToString());

        if (lstBranches.Count != 0)
        {
          BranchListAdapter myAdapter = new BranchListAdapter((AppCompatActivity)(container.Context), lstBranches);
          listViewBranchesNames.Adapter = myAdapter;
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
      alertDialog.SetTitle("Sin sucursales.");
      alertDialog.SetMessage("No existen sucursales cerca de tu ubicación.");
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

