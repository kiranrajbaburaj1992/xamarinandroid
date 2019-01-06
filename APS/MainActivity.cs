using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using RestSharp;
using System;
using System.Threading.Tasks;
using APS.Model;
using System.Collections.Generic;
using APS.Adapter;
using System.Threading;

namespace APS
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        CustomListAdapter adapter;
        List<User> mlist;
        private ListView listView;
        private SynchronizationContext sc;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            listView = FindViewById<ListView>(Resource.Id.listView1);

            //Adapter
            mlist = new List<User>();
            adapter = new CustomListAdapter(this, mlist);
            listView.Adapter = adapter;
            sc = SynchronizationContext.Current;
            getJSON();
        }

        // Fetch JSON in the background

        private async void getJSON()
        {
            // PASS URL
            IRestClient client = new RestClient("http://jsonplaceholder.typicode.com");
            IRestRequest request = new RestRequest("users/", Method.GET);

            try
            {
                await Task.Run(() =>
                {
                    IRestResponse<List<User>> response = client.Execute<List<User>>(request);
                    foreach(var user in response.Data)
                    {
                        sc.Post(new SendOrPostCallback(o =>
                        {
                            adapter.Add(o as User);
                        }), user);
                    }
                });

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}