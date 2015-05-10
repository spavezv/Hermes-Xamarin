using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace Hermes.AndroidViews.CourtBooking
{
	public class DatePickerFragment: DialogFragment
	{
		private readonly Context context;
		private  DateTime date;
		private readonly Android.App.DatePickerDialog.IOnDateSetListener listener;

		public DatePickerFragment(Context c, DateTime d, Android.App.DatePickerDialog.IOnDateSetListener l  )
		{
			context = c;
			date = d;
			listener = l;
		}

		public override Dialog OnCreateDialog(Bundle savedState)
		{
			var dialog = new Android.App.DatePickerDialog(context, listener, date.Year, date.Month - 1, date.Day);
			return dialog;
		}
	}
}