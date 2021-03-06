﻿using System;
using Android.App;
using Android.Support.V4.View;
using Android.Views;
using Android.OS;
using System.Collections.Generic;
using Android.Widget;
using Android.Support.V7.App;
using Java.Util;
using Android.Content;

namespace Hermes.AndroidViews.CourtBooking
{
	public class BookingCourtFragment : Fragment, 
	Android.App.DatePickerDialog.IOnDateSetListener


	{
		private CourtTabScrollView mSlidingTabScrollView;
		private ViewPager mViewPager;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.sliding_tab_fragment, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			mSlidingTabScrollView = view.FindViewById<CourtTabScrollView>(Resource.Id.sliding_tabs);
			mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
			mViewPager.Adapter = new SamplePagerAdapter(mSlidingTabScrollView, mViewPager);

			mSlidingTabScrollView.ViewPager = mViewPager;
		}
		public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			var date = new DateTime(year, monthOfYear + 1, dayOfMonth);
			//View.FindViewById<TextView>(Resource.Id.da).Text = "You picked " + date.ToString("yyyy-MMM-dd");
			Console.WriteLine("FECHA SELECCIONADA " + date.ToString("yyyy-MMM-dd"));
		}

		public class SamplePagerAdapter : PagerAdapter
		{
			List<string> items = new List<string>();
			private List<string> courtTypesItems, courtHoursItems, courtBranchesItems;
			private ListView listViewCourtTypes, listViewBranches, listViewHours;
			private DatePicker datePicker;
			CourtTabScrollView msTabScrollView;
			ViewPager mviewPager;

			public SamplePagerAdapter(CourtTabScrollView sTabScrollView, ViewPager vPager) : base()
			{
				items.Add("Cancha");
				items.Add("Fecha");
				items.Add("Recinto");
				items.Add("Hora");
				msTabScrollView= sTabScrollView; mviewPager=vPager;
			}

			public override int Count
			{
				get { return items.Count; }
			}

			public override bool IsViewFromObject(View view, Java.Lang.Object obj)
			{
				return view == obj;
			}

			public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
			{
				View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_list, container, false);
				if (position == 0) {
					view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_list, container, false);
					listViewCourtTypes = view.FindViewById<ListView> (Resource.Id.list_tab);

					courtTypesItems = new List<string>();
					courtTypesItems.Add("Fútbol 7");
					courtTypesItems.Add("Fútbol 11");
					courtTypesItems.Add("Tenis");
					courtTypesItems.Add("Basquetball");
					courtTypesItems.Add("Voleyball");
					CourtTypesAdapter myAdapter= new CourtTypesAdapter((AppCompatActivity)(container.Context), courtTypesItems);

					listViewCourtTypes.Adapter = myAdapter;
					listViewCourtTypes.ItemClick += (sender, e) => 
					{
						msTabScrollView.moveTab(position+1);
					};
					container.AddView(view);

					return view;
				}
				if (position == 1) {
					//Date picker
					view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_date, container, false);
					datePicker = view.FindViewById<DatePicker> (Resource.Id.date_picker);

					datePicker.Init (
						DateTime.Now.Year,
						DateTime.Now.Month,
						DateTime.Now.Day,
						new AndroidDatePickerListener (msTabScrollView, position));

					container.AddView (view);
					return view;
				}
				if (position == 2) {
					view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_list, container, false);
					listViewBranches = view.FindViewById<ListView> (Resource.Id.list_tab);

					courtBranchesItems = new List<string>();
					courtBranchesItems.Add("Talca Soccer");
					courtBranchesItems.Add("Estadio La Granja");
					courtBranchesItems.Add("Canchas Guaiquillo");
					courtBranchesItems.Add("Canchas Zapallar");
					courtBranchesItems.Add("Rauquen");
					BranchListAdapter myAdapter= new BranchListAdapter((AppCompatActivity)(container.Context), courtBranchesItems);

					listViewBranches.Adapter = myAdapter;
					listViewBranches.ItemClick += (sender, e) => 
					{
						msTabScrollView.moveTab(position+1);
					};
					container.AddView(view);

					return view;
				}
				if (position == 3) {
					view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_list, container, false);
					listViewHours = view.FindViewById<ListView> (Resource.Id.list_tab);

					courtHoursItems = new List<string>();
					courtHoursItems.Add("15:00");
					courtHoursItems.Add("16:00");
					courtHoursItems.Add("19:00");
					courtHoursItems.Add("20:00");
					courtHoursItems.Add("21:00");
					HourListAdapter myAdapter= new HourListAdapter((AppCompatActivity)(container.Context), courtHoursItems);

					listViewHours.Adapter = myAdapter;
					listViewHours.ItemClick += (sender, e) => 
					{
						//Dialog
						Android.App.AlertDialog.Builder builder= new Android.App.AlertDialog.Builder(container.Context);
						Android.App.AlertDialog alertDialog = builder.Create();
						alertDialog.SetTitle("¿Realizar reserva?");
						alertDialog.SetMessage("Su reservacion es: ");
						alertDialog.SetButton("Aceptar", (s, ev) => 
							{/*do something*/});
						alertDialog.SetButton2("Cancelar", (s, ev) => {alertDialog.Dismiss();});
						alertDialog.Show();
						
					};
					container.AddView(view);

					return view;
				}
				return view;
			}


			public string GetHeaderTitle (int position)
			{
				return items[position];
			}

			public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
			{
				container.RemoveView((View)obj);
			}

		
	
		}
		public class AndroidDatePickerListener: Java.Lang.Object, DatePicker.IOnDateChangedListener {
			private CourtTabScrollView mSlidingTabScrollView;
			private int position;
			public AndroidDatePickerListener(CourtTabScrollView tabScrollView, int pos){
				mSlidingTabScrollView= tabScrollView;
				position= pos;
			}
			public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
				mSlidingTabScrollView.moveTab(position+1);
			}
		}
	}
}