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
using Android.Support.V7.App;

namespace Hermes.AndroidViews.Main
{
    class SettingsFragment : Fragment
    {
        ListView settingList;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.settings_fragment, container, false);
            settingList = view.FindViewById<ListView>(Resource.Id.list_settings_fragment);
            //SettingsAdapter myAdapter = new SettingsAdapter((AppCompatActivity)(container.Context));
            //settingList.Adapter = myAdapter;
            
            
            return view;
        }



    }
    

}