using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Views;
using Hermes.Models;
using Java.Text;
using Java.Util;

namespace Hermes.AndroidViews.CourtBooking
{
	public class HourListAdapter: BaseExpandableListAdapter
	{
		public List<Block> items { get; set;}
		public AppCompatActivity context;
		List<String> parentItems;
		//List<Block> childItems;

		public HourListAdapter(AppCompatActivity c, List<Block> items):base() 
		{
			this.items = items;
			this.context = c;
			parentItems = new List<String>(); fillParents(items);
		}


		void fillParents (List<Block> itemTo)
		{			
			foreach (var item in itemTo) {
				if(!(parentItems.Contains(item.courtId.name)))
				{
					parentItems.Add (item.courtId.name);
				}
			}
		}

		public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			View header = convertView;
			if (header == null) {
				header = context.LayoutInflater.Inflate (Resource.Layout.exp_group_list, null);
			}

			TextView txtCourtName = header.FindViewById<TextView>(Resource.Id.txt_court_name);
			txtCourtName.Text = parentItems[groupPosition];

			return header;
		}

		public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			String inicio = items[childPosition].start.Substring (11, 5);
			String termino = items [childPosition].finish.Substring (11, 5);
			View row = convertView;
			if (row == null) {
				row = context.LayoutInflater.Inflate (Resource.Layout.exp_child_list, null);
			}
			string time = "", price = "";
			GetChildViewHelper (groupPosition, childPosition, out time, out price);
			TextView txtBlockTime = row.FindViewById<TextView>(Resource.Id.txt_block_time);
 			TextView txtBlockPrice = row.FindViewById<TextView> (Resource.Id.txt_block_price);

			txtBlockTime.Text= time;
			txtBlockPrice.Text= price;
			return row;
		}
		public override int GetChildrenCount (int groupPosition)
		{			
			
			List<Block> results = items.FindAll ((Block obj) => obj.courtId.name [0].Equals (parentItems[groupPosition]));
			return results.Count;
		}

		public override int GroupCount {
			get {
				return parentItems.Count;
			}
		}

		private void GetChildViewHelper (int groupPosition, int childPosition, out string time, out string price)
		{
			String inicio = items[childPosition].start.Substring (11, 5);
			String termino = items [childPosition].finish.Substring (11, 5);

			List<Block> results = items.FindAll ((Block obj) => obj.courtId.name [0].Equals (parentItems[groupPosition]));
			time =  inicio + " hrs. a " + termino + " hrs.";
			price = "$" + results [childPosition].price.ToString();
		}

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

	}
}

