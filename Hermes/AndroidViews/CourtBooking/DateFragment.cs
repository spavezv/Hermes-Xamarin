using System;

using Android.Views;
using Android.OS;
using Android.Widget;
using Hermes.AndroidViews;
using Android.App;
using Hermes.AndroidViews.Main;
using System.Globalization;
using Java.Util;

namespace Hermes.AndroidViews.CourtBooking
{
    public class DateFragment : Fragment, View.IOnClickListener
    {
        public ImageView imgLeft, imgRight;
        private TextView txtCategory;
        public DatePicker datePicker;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.booking_date_fragment, container, false);
            imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
            imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
            txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
            datePicker = view.FindViewById<DatePicker>(Resource.Id.date_boking_fragment);
            txtCategory.SetText(Resource.String.FechadeReserva);
            imgRight.SetImageResource(Resource.Drawable.ic_arrow_right_disable);
            imgLeft.SetOnClickListener(this);
            imgRight.SetImageResource(Resource.Drawable.ic_arrow_right_available);
            imgRight.SetOnClickListener(this);

            datePicker.MinDate = (long)(DateTime.Today.Millisecond);
            datePicker.Init(
                DateTime.Now.Year,
                DateTime.Now.Month - 1,
                DateTime.Now.Day,
                new AndroidDatePickerListener(this));
            datePicker.FirstDayOfWeek = Java.Util.Calendar.Monday;
            datePicker.UpdateDate(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
            datePicker.MinDate = new Java.Util.Date().Time - 1000;
            return view;
        }

        public void OnClick(View v)
        {

            switch (v.Id)
            {
                case Resource.Id.img_arrow_left:
                    ((HermesActivity)this.Activity).replaceFragment(new TypeFragment());
                    break;
                case Resource.Id.img_arrow_right:
                    ((HermesActivity)this.Activity).replaceFragment(new BranchFragment());
                    break;
                default:
                    break;
            }

        }


        public class AndroidDatePickerListener : Java.Lang.Object, DatePicker.IOnDateChangedListener
        {
            DateFragment mBookCourtDateFrag;
            public AndroidDatePickerListener(DateFragment bookingCourtDateFragment)
            {
                mBookCourtDateFrag = bookingCourtDateFragment;
            }
            public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                //capturar la fecha
                var date = new DateTime(year, monthOfYear + 1, dayOfMonth);
                ((HermesActivity)mBookCourtDateFrag.Activity).DateEsp = date.ToString("d MMMM",
                    CultureInfo.CreateSpecificCulture("es-CL"));
                ((HermesActivity)mBookCourtDateFrag.Activity).Date = date.ToString("yyyy-MM-dd");
            }
        }
    }
}

