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
using Android.Support.V7.App;

namespace Hermes.AndroidViews.Main
{
	class SettingsAdapter: BaseAdapter<string>
    {
        public List<string> items;
        public AppCompatActivity context;

		public SettingsAdapter(AppCompatActivity c, List<string> items)
            : base() 
		{
			this.context = c;
			this.items = items;
           
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
				row = LayoutInflater.From (context).Inflate(Resource.Layout.item_setting, null, false);
			}
			TextView txtCourtType = row.FindViewById<TextView> (Resource.Id.txt_item_setting);
			ImageView imgArrow = row.FindViewById<ImageView> (Resource.Id.img_arrow_setting);

			txtCourtType.Text = items [position];


			return row;
		}



	}
}

