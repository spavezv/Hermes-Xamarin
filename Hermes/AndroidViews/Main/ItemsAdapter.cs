using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Support.V7.App;
using System.Collections;

namespace Hermes.AndroidViews.ActionBarDrawerToggle
{
	public class ItemsAdapter : BaseAdapter<string>
	{
		public List<string> items;
		public AppCompatActivity context;

		public ItemsAdapter(AppCompatActivity c, List<string> items):base() 
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
			ImageView imgNavDrawer = row.FindViewById<ImageView> (Resource.Id.img_drawer_item);
			TextView txtCourtType = row.FindViewById<TextView> (Resource.Id.txt_drawer_item);
			txtCourtType.Text = items [position];
			switch (position) {
			case 0: //reservar cancha
				imgNavDrawer.SetImageResource (Resource.Drawable.ic_court);
				break;
			case 1: //mis reservas
				imgNavDrawer.SetImageResource (Resource.Drawable.ic_reservations);
				break;
			case 2: //configuracion
				imgNavDrawer.SetImageResource (Resource.Drawable.ic_configurations);
				break;
			case 3: //ayuda
				imgNavDrawer.SetImageResource (Resource.Drawable.ic_help);
				break;
			default:
				break;
			}


			return row;
		}
			
	}
}

