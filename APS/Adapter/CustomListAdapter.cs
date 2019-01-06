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
using APS.Model;

namespace APS.Adapter
{
    public class CustomListAdapter : BaseAdapter<User>
    {
        public List<User> sList;
        private Context sContext;
        public CustomListAdapter(Context context, List<User> list)
        {
            sList = list;
            sContext = context;
        }
        public void Add(User user)
        {
            sList.Add(user);
            this.NotifyDataSetChanged();
        }
        public override User this[int position]
        {
            get
            {
                return sList[position];
            }
        }
        public override int Count
        {
            get
            {
                return sList.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            try
            {
                if (row == null)
                {
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.layout_user, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.Name);
                txtName.Text = sList[position].email;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }
    }
}