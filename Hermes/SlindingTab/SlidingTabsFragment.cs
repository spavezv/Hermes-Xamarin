using System;
using Android.App;
using Android.Support.V4.View;
using Android.Views;
using Android.OS;
using System.Collections.Generic;
using Android.Widget;
using Android.Support.V7.App;

namespace Hermes
{
	public class SlidingTabsFragment : Fragment
	{
		private SlidingTabScrollView mSlidingTabScrollView;
		private ViewPager mViewPager;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.sliding_tab_fragment, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
			mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
			mViewPager.Adapter = new SamplePagerAdapter();

			mSlidingTabScrollView.ViewPager = mViewPager;
		}

		public class SamplePagerAdapter : PagerAdapter
		{
			List<string> items = new List<string>();
			private List<string> courtTypesItems;
			private ListView listViewCourtTypes;
			private DatePickerFragment datePicker;

			public SamplePagerAdapter() : base()
			{
				items.Add("Cancha");
				items.Add("Fecha");
				items.Add("Recinto");
				items.Add("Hora");
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
					ItemsAdapter myAdapter= new ItemsAdapter((AppCompatActivity)(container.Context), courtTypesItems);

					listViewCourtTypes.Adapter = myAdapter;
					container.AddView(view);

					return view;
				}
				if (position == 1) {
					//Date picker
//					view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.tab_date, container, false);
//					datePicker = new DatePickerFragment((container.Context), DateTime.Now, this);
//					container.AddView (view);
//					return view;
					Toast.MakeText (container.Context, "3", ToastLength.Short).Show ();
				}
				if (position == 2) {
					Toast.MakeText (container.Context, "2", ToastLength.Short).Show ();
				}
				if (position == 3) {
					Toast.MakeText (container.Context, "3", ToastLength.Short).Show ();
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
	}
}