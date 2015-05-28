using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Hermes.Models;

namespace Hermes.AndroidViews.CourtBooking
{
	public class HourListAdapter: BaseAdapter
	{
		public List<Block> items;
		public AppCompatActivity context;

		public HourListAdapter(AppCompatActivity c, List<Block> items):base() 
		{
			this.items = items;
			this.context = c;

		}
		public override int Count {
			get {
				return items.Count;
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}

    public override Java.Lang.Object GetItem(int position)
    {
      // could wrap a Contact in a Java.Lang.Object
      // to return it here if needed
      return null;
    }

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if(row == null)
			{
				row = LayoutInflater.From (context).Inflate(Resource.Layout.item_block_listview, null, false);
			}
      TextView txtBlockTime = row.FindViewById<TextView>(Resource.Id.txt_block_time);
			TextView txtBlockPrice = row.FindViewById<TextView> (Resource.Id.txt_block_price);
			//imgHour.SetImageResource (Resource.Drawable.ic_hour_list);
      txtBlockTime.Text = items[position].start + " hrs. a " + items[position].finish + " hrs.";
      txtBlockPrice.Text = "$" + items[position].price;

			return row;
		}


	}
}

