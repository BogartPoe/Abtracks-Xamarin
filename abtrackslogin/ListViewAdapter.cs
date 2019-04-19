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

namespace abtrackslogin
{
    class ListViewAdapter : BaseAdapter
    {
        //calling the class activity
        Activity activity;
        //creeate a list
        List<Account> lstAccounts;
        //craete an inflater
        LayoutInflater inflater;

        public ListViewAdapter(Activity activity, List<Account> lstAccounts)
        {
            this.activity = activity;
            this.lstAccounts = lstAccounts;
        }
        public override int Count
        {
            get { return lstAccounts.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //setting the inflater into the class
            inflater = (LayoutInflater)activity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            //calling list viewe vaule
            View itemView = inflater.Inflate(Resource.Layout.list_Item, null);
            //calling name value
            var txtuser = itemView.FindViewById<TextView>(Resource.Id.list_name);
            //calling map link
            var txtemail = itemView.FindViewById<TextView>(Resource.Id.list_email);
            //listing the value
            if (lstAccounts.Count > 0)
            {
                txtuser.Text = lstAccounts[position].name;
                txtemail.Text = lstAccounts[position].map;
            }
            //returning the value
            return itemView;
        }

    }

    class ListViewAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}