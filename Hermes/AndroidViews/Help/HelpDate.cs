
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
	public class HelpDate : Fragment
	{
		const float ALPHA_DIM_VALUE = 0.1f;
		const float SHOWCASE_KITTEN_SCALE = 1.2f;
		const float SHOWCASE_LIKE_SCALE = 0.5f;
		ShowcaseView.ConfigOptions mOptions = new ShowcaseView.ConfigOptions();
		public DatePicker datePicker;
		ShowcaseViews mViews;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.booking_date_fragment, container, false);
			TextView txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			txtCategory.SetText (Resource.String.FechadeReserva);
			ImageView imgright = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			imgright.SetImageResource (Resource.Drawable.ic_arrow_right_available);
			datePicker = view.FindViewById<DatePicker>(Resource.Id.date_boking_fragment);
			datePicker.MinDate = (long)(DateTime.Today.Millisecond);
			datePicker.FirstDayOfWeek = Java.Util.Calendar.Monday;
			datePicker.UpdateDate(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
			datePicker.MinDate = new Java.Util.Date().Time - 1000;
			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			mViews = new ShowcaseViews(((HermesActivity)this.Activity), new MyShowcaseAcknowledgeListener(((HermesActivity)this.Activity)));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.date_boking_fragment, Resource.String.TtFecha, Resource.String.MessFecha, SHOWCASE_KITTEN_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_left, Resource.String.TtPrev2, Resource.String.MessPrev2, SHOWCASE_LIKE_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_right, Resource.String.TtNext2, Resource.String.MessNext2, SHOWCASE_LIKE_SCALE));
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
				((HermesActivity)this.parent).replaceFragment(new HelpBranch());			
			}

			#endregion
		}
	}

}

