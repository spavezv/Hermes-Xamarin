
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
using SharpShowcaseView;
using SharpShowcaseView.Targets;
using Hermes.AndroidViews.Main;
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using Java.Lang.Annotation;


namespace Hermes
{
	public class HelpFragment : Fragment
	{
		const float ALPHA_DIM_VALUE = 0.1f;
		const float SHOWCASE_KITTEN_SCALE = 1.2f;
		const float SHOWCASE_LIKE_SCALE = 0.5f;
		ListView lstview;
		List<string> items = new List<string> ();
		ShowcaseViews mViews;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.booking_list_fragment, container, false);
			TextView txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			txtCategory.SetText (Resource.String.TipodeCancha);
			ImageView imgright = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			imgright.SetImageResource (Resource.Drawable.ic_arrow_right_available);
		
			lstview = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			items.Add ("Tipo 1");
			items.Add ("Tipo 2");
			items.Add ("Tipo 3");
			items.Add ("Tipo 4");
			items.Add ("Tipo 5");
			CourtTypesAdapter myAdapter = new CourtTypesAdapter((AppCompatActivity)(container.Context), items);
			lstview.Adapter = myAdapter;

			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			mViews = new ShowcaseViews(((HermesActivity)this.Activity), new MyShowcaseAcknowledgeListener(((HermesActivity)this.Activity)));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.list_booking_fragment, Resource.String.TtTipoDeporte, Resource.String.MessTipoDeporte, SHOWCASE_KITTEN_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_right, Resource.String.TtNext1, Resource.String.MessNext1, SHOWCASE_LIKE_SCALE));
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
				((HermesActivity)this.parent).replaceFragment(new HelpDate());			
			}

			#endregion
		}
	}

}
