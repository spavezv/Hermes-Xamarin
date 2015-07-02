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
    class SettingsAdapter 
    {
        public List<string> items;
        public List<string> headers;
        public AppCompatActivity context;

        public SettingsAdapter(AppCompatActivity c)
            : base() 
		{
			this.context = c;
            items = new List<string>(); poblateItems();
            headers = new List<string>(); poblateHeaders();
           

		}

        private void poblateHeaders()
        {
            headers.Add("General");
            headers.Add("Programador");
        }

        private void poblateItems()
        {
            items.Add("Editar cuenta");
            items.Add("Sobre Hermes");
        }

        public View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.item_listview, null, false);
            }
            
            TextView txtSetting = row.FindViewById<TextView>(Resource.Id.txt_drawer_item);

            txtSetting.Text = items[position];
            return row;
        }

    }
}