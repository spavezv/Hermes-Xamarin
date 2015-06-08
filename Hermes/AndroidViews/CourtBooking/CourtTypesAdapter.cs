using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;

namespace Hermes.AndroidViews.CourtBooking
{
	public class CourtTypesAdapter: BaseAdapter<string>
	{
		public List<string> items;
		public AppCompatActivity context;
		public ImageView imgType;

		public CourtTypesAdapter(AppCompatActivity c, List<string> items):base() 
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
				row = LayoutInflater.From (context).Inflate(Resource.Layout.item_type_lstv, null, false);
			}
			TextView txtCourtType = row.FindViewById<TextView> (Resource.Id.txt_type_item);
			imgType = row.FindViewById<ImageView> (Resource.Id.img_type_item);

			txtCourtType.Text = items [position];
			setImage (items[position]);

			return row;
		}


		void setImage (string str)
		{
			if (str.Contains ("Futbol") || str.Contains ("futbol")) {
				imgType.SetImageResource (Resource.Drawable.ic_futbol);
			} 
			if (str.Contains ("Tenis") || str.Contains ("tenis")) {
				imgType.SetImageResource (Resource.Drawable.ic_tennis);
			} 
			if (str.Contains ("Basquetball") || str.Contains ("basquetball")) {
				imgType.SetImageResource (Resource.Drawable.ic_basketball);
			}
			if (str.Contains ("Voleyball") || str.Contains ("voleyball")) {
				imgType.SetImageResource (Resource.Drawable.ic_volleyball);
			}
		}
	}
}

