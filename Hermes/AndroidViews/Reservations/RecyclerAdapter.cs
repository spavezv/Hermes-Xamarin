
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.Reservations;
using System.Collections.Generic;
using System;
using Android.Content;
using Android.Views.Animations;
using Android.Animation;
using Hermes.Models;
using System.Globalization;
using Hermes.AndroidViews.Main;

namespace Hermes.AndroidViews.Reservations
{
  public class RecyclerAdapter : RecyclerView.Adapter
  {
    private static MyList<Block> mReservations;
    private RecyclerView mRecyclerView;
    private Context mContext;
    private int mCurrentPosition = -1;
		public static Block mReservation;
	

    public RecyclerAdapter(MyList<Block> reservations, RecyclerView recyclerView, Context context)
    {
      mReservations = reservations;
      mRecyclerView = recyclerView;
      mContext = context;
    }

    public class MyView : RecyclerView.ViewHolder
    {
      public View mMainView { get; set; }
      public TextView mType { get; set; }
      public TextView mBusiness { get; set; }
      public TextView mAddress { get; set; }
      public TextView mDate { get; set; }


      public MyView(View view)
        : base(view)
      {
        mMainView = view;
      }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
      View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row_reservation, parent, false);
      TextView txtType = row.FindViewById<TextView>(Resource.Id.txt_type);
      TextView txtBusiness = row.FindViewById<TextView>(Resource.Id.txt_business);
      TextView txtAddress = row.FindViewById<TextView>(Resource.Id.txt_address);
      TextView txtDate = row.FindViewById<TextView>(Resource.Id.txt_date);
      MyView view = new MyView(row) { mType = txtType, mBusiness = txtBusiness, mAddress= txtAddress, mDate = txtDate };
      return view;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
      MyView myHolder = holder as MyView;
      //int indexPosition = (mReservations.Count - 1) - position;
      int indexPosition = position;

			DateTime date = DateTime.Parse(mReservations[indexPosition].date);
			string dateReservation = date.ToString ("d MMMM", 
				CultureInfo.CreateSpecificCulture("es-MX"));
			myHolder.mMainView.Click += mMainView_Click;
			myHolder.mType.Text = mReservations[indexPosition].courtId.sport;
			myHolder.mBusiness.Text = mReservations[indexPosition].courtId.branchId.businessId.name;
			myHolder.mAddress.Text = mReservations[indexPosition].courtId.branchId.street + " " +
				mReservations[indexPosition].courtId.branchId.number + " " +
				mReservations[indexPosition].courtId.branchId.commune ;
			myHolder.mDate.Text = dateReservation ;

      if (position > mCurrentPosition)
      {
        int currentAnim;
        if (position % 2 == 0)
        {
          currentAnim = Resource.Animation.slide_left_to_right;

        }
        else
        {
          currentAnim = Resource.Animation.slide_right_to_left;       
        }
        SetAnimation(myHolder.mMainView, currentAnim);
        mCurrentPosition = position;
      }
    }

    private void SetAnimation(View view, int currentAnim)
    {
      //Animator animator = AnimatorInflater.LoadAnimator(mContext, Resource.Animation.flip);
      //animator.SetTarget(view);
      //animator.Start();
      Animation anim = AnimationUtils.LoadAnimation(mContext, currentAnim);
      view.StartAnimation(anim);
    }
		//Aqui esta el click de cada item
    void mMainView_Click(object sender, System.EventArgs e)
    {
      int position = mRecyclerView.GetChildPosition((View)sender);
			mReservation = mReservations[position];
			var intent = new Intent(((HermesActivity)mContext), typeof(DetailsFragment));
			((HermesActivity)mContext).StartActivity(intent);
    }

    public override int ItemCount
    {
      get { return mReservations.Count; }
    }

    public override int GetItemViewType(int position)
    {
      return Resource.Layout.row_reservation;
    }

  }

  public class MyList<Block>
  {
	private List<Block> mItems;
    private RecyclerView.Adapter mAdapter;

    public MyList()
    {
		mItems = new List<Block>();
    }

    public RecyclerView.Adapter Adapter
    {
      get { return mAdapter; }
      set { mAdapter = value; }
    }

	public void Add(Block item)
    {
      mItems.Add(item);
      if (Adapter != null)
      {
        Adapter.NotifyItemInserted(mItems.Count - 1);
      }
    }

    public void Remove(int position)
    {
      mItems.RemoveAt(position);
      if (Adapter != null)
      {
        Adapter.NotifyItemRemoved(position);
      }
    }

	public Block this[int index]
    {
      get { return mItems[index]; }
      set { mItems[index] = value; }
    }

    public int Count
    {
      get { return mItems.Count; }
    }
  }
}