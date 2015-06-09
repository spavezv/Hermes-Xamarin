
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
using Hermes.Models;
using Hermes.AndroidViews.Reservations;
using System.Globalization;
using Hermes.AndroidViews.Main;

namespace Hermes
{
	
	public class DetailsFragment : Fragment,  View.IOnClickListener
	{
		public Block bSelected;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			bSelected = RecyclerAdapter.mReservation;
			View view = inflater.Inflate(Resource.Layout.details_fragment, container, false);

			TextView txtBranche = view.FindViewById<TextView> (Resource.Id.txt_branche);
			TextView txtCourt = view.FindViewById<TextView> (Resource.Id.txt_court_name);
			TextView txtDateTime = view.FindViewById<TextView> (Resource.Id.txt_date_time);
			TextView txtPrice = view.FindViewById<TextView> (Resource.Id.txt_price);
			TextView txtAddress = view.FindViewById<TextView> (Resource.Id.txt_address);
			TextView txtCommune = view.FindViewById<TextView> (Resource.Id.txt_commune);
			Button btnBack = view.FindViewById<Button> (Resource.Id.button_back);

			String inicio =bSelected.start.Substring (11, 5);
			String termino = bSelected.finish.Substring (11, 5);
			DateTime date = DateTime.Parse(bSelected.date);
			string dateReservation = date.ToString ("d MMMM", 
				CultureInfo.CreateSpecificCulture("es-MX"));
			
			txtBranche.Text = bSelected.courtId.branchId.businessId.name;
			txtCourt.Text = bSelected.courtId.name;
			txtDateTime.Text = dateReservation + " de " + inicio + " a " + termino + " hrs.";
			txtPrice.Text = "$" + bSelected.price.ToString(); 
			txtAddress.Text = bSelected.courtId.branchId.street + bSelected.courtId.branchId.number;
			txtCommune.Text = bSelected.courtId.branchId.commune;

			btnBack.SetOnClickListener (this);
			return view;
		}

		public void OnClick(View v)
		{
			switch (v.Id)
			{
			case Resource.Id.button_back:
				((HermesActivity)this.Activity).replaceFragment(new UserReservations());	
				break;
			default:
				break;
			}

		}

	}
}

