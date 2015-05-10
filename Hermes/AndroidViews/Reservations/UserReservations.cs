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
using Android.Support.V7.Widget;
using Hermes.AndroidViews.Reservations;

namespace Hermes.AndroidViews.Reservations
{
    public class UserReservations : Fragment
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private MyList<Reservation> mReservations;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var view = inflater.Inflate(Resource.Layout.user_reservations, container, false);

			mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rv_reservations);
            mReservations = new MyList<Reservation>();
            mReservations.Add(new Reservation() { Name = "Futbol 7", Subject = "Talca Soccer", Message = "Martes 30 de Julio de 2014" });
            mReservations.Add(new Reservation() { Name = "Futbol 7", Subject = "Talca Soccer", Message = "Martes 30 de Julio de 2014" });
            mReservations.Add(new Reservation() { Name = "Futbol 7", Subject = "Talca Soccer", Message = "Martes 30 de Julio de 2014" });
            mReservations.Add(new Reservation() { Name = "Futbol 7", Subject = "Talca Soccer", Message = "Martes 30 de Julio de 2014" });
            mReservations.Add(new Reservation() { Name = "Futbol 7", Subject = "Talca Soccer", Message = "Martes 30 de Julio de 2014" });

            //Create our layout manager
            mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mReservations, mRecyclerView, Activity);
            mReservations.Adapter = mAdapter;
            mRecyclerView.SetAdapter(mAdapter);
            return view;
        }
    }
}
