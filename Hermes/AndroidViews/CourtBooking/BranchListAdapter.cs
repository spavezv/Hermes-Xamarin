using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Hermes.Models;

namespace Hermes.AndroidViews.CourtBooking
{
  public class BranchListAdapter : BaseAdapter
	{
    public List<Branches> mListBranches;
		public AppCompatActivity context;

    public BranchListAdapter(AppCompatActivity c, List<Branches> lstBranches)
		{
      mListBranches = lstBranches;
      context = c;
		}
    public override int Count
    {
      get
      {
        return mListBranches.Count;
      }
    }

    public override Java.Lang.Object GetItem(int position)
    {
      // could wrap a Contact in a Java.Lang.Object
      // to return it here if needed
      return null;
    }


    public override long GetItemId(int position)
    {
      return mListBranches[position].id;
    }


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if(row == null)
			{
				row = LayoutInflater.From (context).Inflate(Resource.Layout.item_branch_listview, null, false);
			}
      ImageView imgBusiness = row.FindViewById<ImageView>(Resource.Id.img_branch_item);
      TextView txtBusinessName = row.FindViewById<TextView>(Resource.Id.txt_business_name);
      TextView txtBranchAddress = row.FindViewById<TextView>(Resource.Id.txt_branche_address);
      TextView txtBranchePlace = row.FindViewById<TextView>(Resource.Id.txt_branche_city);

      txtBusinessName.Text = mListBranches[position].businessId.name;
      txtBranchAddress.Text = mListBranches[position].street + " #" + mListBranches[position].number;
      txtBranchePlace.Text = mListBranches[position].commune + " - " + mListBranches[position].city;

			return row;
		}


	}
}

