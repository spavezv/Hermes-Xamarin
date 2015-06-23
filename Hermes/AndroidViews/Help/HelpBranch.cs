
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using SharpShowcaseView;
using Hermes.AndroidViews.Main;

namespace Hermes
{
	public class HelpBranch : Fragment
	{
		const float ALPHA_DIM_VALUE = 0.1f;
		const float SHOWCASE_KITTEN_SCALE = 1.2f;
		const float SHOWCASE_LIKE_SCALE = 0.5f;
		List<string> items = new List<string> ();
		ShowcaseViews mViews;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.booking_list_fragment, container, false);
			ImageView imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			ImageView imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			TextView txtCtgory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			ListView listViewBranchesNames = view.FindViewById<ListView>(Resource.Id.list_booking_fragment);
			txtCtgory.SetText(Resource.String.RecintosDisponibles);
			imgRight.SetImageResource(Resource.Drawable.ic_arrow_right_available);

			items.Add ("Sucursal 1");
			items.Add ("Sucursal 2");
			items.Add ("Sucursal 3");
			items.Add ("Sucursal 4");
			items.Add ("Sucursal 5");
			CourtTypesAdapter myAdapter = new CourtTypesAdapter((AppCompatActivity)(container.Context), items);
			listViewBranchesNames.Adapter = myAdapter;

			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			mViews = new ShowcaseViews(((HermesActivity)this.Activity), new MyShowcaseAcknowledgeListener(((HermesActivity)this.Activity)));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.list_booking_fragment, Resource.String.TtBranch, Resource.String.MessBranch, SHOWCASE_KITTEN_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_left, Resource.String.TtPrev3, Resource.String.MessPrev3, SHOWCASE_LIKE_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_right, Resource.String.TtNext3, Resource.String.MessNext3, SHOWCASE_LIKE_SCALE));
			mViews.Show();
		}


		class MyShowcaseAcknowledgeListener : ShowcaseViews.IOnShowcaseAcknowledged
		{
			readonly Activity parent;

			public MyShowcaseAcknowledgeListener(Activity parent)
			{
				this.parent = parent;
			}

			#region IOnShowcaseAcknowledged implementation

			public void OnShowCaseAcknowledged(ShowcaseView showcaseView)
			{
				//aqui deberia cambiar de fragment
				((HermesActivity)this.parent).replaceFragment(new HelpBlock());			
			}

			#endregion
		}
	}

}
