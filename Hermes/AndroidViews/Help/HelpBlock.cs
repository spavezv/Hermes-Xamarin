
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
using Hermes.AndroidViews.CourtBooking;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;
using SharpShowcaseView.Targets;
using Hermes.AndroidViews.Reservations;

namespace Hermes
{
	public class HelpBlock : Fragment
	{
		const float ALPHA_DIM_VALUE = 0.1f;
		const float SHOWCASE_KITTEN_SCALE = 1.2f;
		const float SHOWCASE_LIKE_SCALE = 0.5f;
		List<string> items = new List<string> ();
		ShowcaseViews mViews;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			ImageView imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			ImageView imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			TextView txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			ListView expListViewCourts = view.FindViewById<ListView>(Resource.Id.list_booking_fragment);
			imgRight.SetImageResource(Resource.Drawable.ic_check_available);

			txtCategory.SetText(Resource.String.HorasDisponibles);
			items.Add ("Bloque 1");
			items.Add ("Bloque 2");
			items.Add ("Bloque 3");
			items.Add ("Bloque 4");
			items.Add ("Bloque 5");
			CourtTypesAdapter myAdapter = new CourtTypesAdapter((AppCompatActivity)(container.Context), items);
			expListViewCourts.Adapter = myAdapter;
			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			mViews = new ShowcaseViews(((HermesActivity)this.Activity), new MyShowcaseAcknowledgeListener(((HermesActivity)this.Activity)));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.list_booking_fragment, Resource.String.TtBlock, Resource.String.MessBlock, SHOWCASE_KITTEN_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_left, Resource.String.TtPrev4, Resource.String.MessPrev4, SHOWCASE_LIKE_SCALE));
			mViews.AddView( new ShowcaseViews.ItemViewProperties(Resource.Id.img_arrow_right, Resource.String.TtNext4, Resource.String.MessNext4, SHOWCASE_LIKE_SCALE));
			mViews.Show();
		}

		static void DimView(View view)
		{
			if (IsHoneycombOrAbove())
			{
				view.Alpha = ALPHA_DIM_VALUE;
			}
		}
		public static bool IsHoneycombOrAbove()
		{
			return Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb;
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

				Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(((HermesActivity)this.parent));
				Android.App.AlertDialog alertDialog = builder.Create();
				alertDialog.SetTitle("Has terminado el tutorial");
				alertDialog.SetMessage("Felicitaciones! \n Has terminado el tutorial para conocer como realizar una reserva.");
				alertDialog.SetButton("Ok", (s, ev) =>
					{ ((HermesActivity)this.parent).replaceFragment(new UserReservations(), "");	});
				alertDialog.Show();	

			}
			#endregion
		}
	}
}

	
