using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;

namespace Hermes.AndroidViews.CourtBooking
{
	public class HourListAdapter: BaseAdapter<string>
	{
		public List<string> items;
		public AppCompatActivity context;

		public HourListAdapter(AppCompatActivity c, List<string> items):base() 
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

		public override string this[int position]
		{
			get { return items [position];}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if(row == null)
			{
				row = LayoutInflater.From (context).Inflate(Resource.Layout.item_listview, null, false);
			}
			ImageView imgHour = row.FindViewById<ImageView> (Resource.Id.img_drawer_item);
			TextView txtHour = row.FindViewById<TextView> (Resource.Id.txt_drawer_item);
			//imgHour.SetImageResource (Resource.Drawable.ic_hour_list);
			txtHour.Text = items [position];

			return row;
		}


	}
}

