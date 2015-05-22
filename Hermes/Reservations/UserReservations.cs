using Android.App;
using Android.OS;
using Android.Support.V7.Widget;

namespace Hermes.Reservations
{
    public class UserReservations : Fragment
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}