using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using Hermes.AndroidViews.WorkshopBooking;
using Android.Support.V7.App;
using Hermes.AndroidViews.Main;
using Android.OS;

namespace Hermes
{
	public class BookngWorkshopTypeFragment: Fragment, View.IOnClickListener
	{
		private List<string> workshopTypesItems;
		private ListView listViewWorkshopTypes;
		private ImageView imgLeft, imgRight;
		private TextView txtCategory;


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			View view = inflater.Inflate(Resource.Layout.booking_list_fragment, container, false);
			imgLeft = view.FindViewById<ImageView>(Resource.Id.img_arrow_left);
			imgRight = view.FindViewById<ImageView>(Resource.Id.img_arrow_right);
			txtCategory = view.FindViewById<TextView>(Resource.Id.txt_book_header);
			listViewWorkshopTypes = view.FindViewById<ListView> (Resource.Id.list_booking_fragment);

			txtCategory.SetText (Resource.String.TipodeTaller);
			imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_disable);
			imgLeft.SetImageResource (Resource.Drawable.ic_arrow_left_disable);

			listViewWorkshopTypes.ChoiceMode = ChoiceMode.Single;

			//Aqui deberia tener un json llenando esta lista
			workshopTypesItems = new List<string> ();
			workshopTypesItems.Add ("Crossfit");
			workshopTypesItems.Add ("Spinning");
			workshopTypesItems.Add ("Zumba");
			workshopTypesItems.Add ("Pilates");

			WorkshopTypesAdapter myAdapter= new WorkshopTypesAdapter((AppCompatActivity)(container.Context), workshopTypesItems);
			listViewWorkshopTypes.Adapter = myAdapter;

			listViewWorkshopTypes.ItemClick += (sender, e) => 
			{
				var tipoTaller = workshopTypesItems[e.Position];
				((HermesActivity)this.Activity).workshop = tipoTaller;
				imgRight.SetImageResource (Resource.Drawable.ic_arrow_right_available);
				imgRight.SetOnClickListener (this);

			};
			//


			return view;
		}



		public void OnClick(View v)
		{
			switch (v.Id)
			{
			case Resource.Id.img_arrow_left:
				//Toast.MakeText((MainActivity)this.Activity, "No hay izquierda", ToastLength.Long).Show();
				break;
			case Resource.Id.img_arrow_right:
				((HermesActivity)this.Activity).replaceFragment(new BookngWorkshopDateFragment());		
				break;
			default:
				break;
			}

		}

	}
}

