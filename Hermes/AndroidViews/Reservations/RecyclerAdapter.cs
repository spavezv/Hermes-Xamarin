
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Hermes.AndroidViews.Reservations;
using System.Collections.Generic;
using System;
using Android.Content;
using Android.Views.Animations;
using Android.Animation;

namespace Hermes.AndroidViews.Reservations
{
  public class RecyclerAdapter : RecyclerView.Adapter
  {
    private MyList<Reservation> mReservations;
    private RecyclerView mRecyclerView;
    private Context mContext;
    private int mCurrentPosition = -1;
    public RecyclerAdapter(MyList<Reservation> reservations, RecyclerView recyclerView, Context context)
    {
      mReservations = reservations;
      mRecyclerView = recyclerView;
      mContext = context;
    }

    public class MyView : RecyclerView.ViewHolder
    {
      public View mMainView { get; set; }
      public TextView mName { get; set; }
      public TextView mSubject { get; set; }
      public TextView mMessage { get; set; }

      public MyView(View view)
        : base(view)
      {
        mMainView = view;
      }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
      View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row_reservation, parent, false);
      TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
      TextView txtSubject = row.FindViewById<TextView>(Resource.Id.txtSubject);
      TextView txtMessage = row.FindViewById<TextView>(Resource.Id.txtMessage);
      MyView view = new MyView(row) { mName = txtName, mSubject = txtSubject, mMessage = txtMessage };
      return view;
    }

    /**
     * Añadir elemento a la lista
     * mReservations.Add(new Reservation() {Name = "name", Subject = "subject, Message = "message"});
    */

    /**
     * Remover elemento de la lista
     * mReservations.Remove(mReservations.Count - 1);
    */

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
      MyView myHolder = holder as MyView;
      //int indexPosition = (mReservations.Count - 1) - position;
      int indexPosition = position;
      myHolder.mMainView.Click += mMainView_Click;
      myHolder.mName.Text = mReservations[indexPosition].Name;
      myHolder.mSubject.Text = mReservations[indexPosition].Subject;
      myHolder.mMessage.Text = mReservations[indexPosition].Message;

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
      Animator animator = AnimatorInflater.LoadAnimator(mContext, Resource.Animation.flip);
      animator.SetTarget(view);
      animator.Start();
      //Animation anim = AnimationUtils.LoadAnimation(mContext, currentAnim);
      //view.StartAnimation(anim);
    }

    void mMainView_Click(object sender, System.EventArgs e)
    {
      int position = mRecyclerView.GetChildPosition((View)sender);
      //int indexPosition = (mReservations.Count - 1) - position;
      int indexPosition = position;
      mReservations.Add(new Reservation() {Name = indexPosition.ToString(), Subject = "subject", Message = "message"});
      Console.WriteLine(mReservations[indexPosition].Name);
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

  public class MyList<T>
  {
    private List<T> mItems;
    private RecyclerView.Adapter mAdapter;

    public MyList()
    {
      mItems = new List<T>();
    }

    public RecyclerView.Adapter Adapter
    {
      get { return mAdapter; }
      set { mAdapter = value; }
    }

    public void Add(T item)
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

    public T this[int index]
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