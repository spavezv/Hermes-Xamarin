using System;

using Android.Views;
using Android.OS;
using Android.Widget;
using Hermes.AndroidViews;
using Android.App;
using Hermes.AndroidViews.Main;
using System.Globalization;

namespace Hermes.AndroidViews.CourtBooking
{
	public class BookingCourtDateFragment: Fragment , View.IOnClickListener
	{
		public ImageView imgLeft, imgRight;
		private TextView txtCategory;
		public DatePicker datePicker;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.booking_date_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			datePicker = view.FindViewById<DatePicker> (Resource.Id.date_boking_fragment);

			txtCategory.SetText (Resource.String.FechadeReserva);
			imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_disable);
			imgLeft.SetOnClickListener (this);

			datePicker.Init (
				DateTime.Now.Year,
				DateTime.Now.Month,
				DateTime.Now.Day,
				new AndroidDatePickerListener (this));


			return view;
		}

		public void OnClick(View v)
		{
			
			switch (v.Id)
			{
			case Resource.Id.img_arrow_left:
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtTypeFragment());		
				break;
			case Resource.Id.img_arrow_right:
				((HermesActivity)this.Activity).replaceFragment(new BookingCourtNamesFragment());		
				break;
			default:
				break;
			}

		}


		public class AndroidDatePickerListener: Java.Lang.Object, DatePicker.IOnDateChangedListener {
			BookingCourtDateFragment mBookCourtDateFrag;
			public AndroidDatePickerListener (BookingCourtDateFragment bookingCourtDateFragment)
			{
				mBookCourtDateFrag = bookingCourtDateFragment;
			}
			public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
				
					//capturar la fecha
				mBookCourtDateFrag.imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_available);
				mBookCourtDateFrag.imgRight.SetOnClickListener (mBookCourtDateFrag);

				var date = new DateTime(year, monthOfYear + 1, dayOfMonth);
				((HermesActivity)mBookCourtDateFrag.Activity).DateEsp= date.ToString("d MMMM", 
					CultureInfo.CreateSpecificCulture("es-MX"));
        ((HermesActivity)mBookCourtDateFrag.Activity).Date= date.ToString("yyyy-MM-dd");
			}


		}

		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			


		}
	}
}

