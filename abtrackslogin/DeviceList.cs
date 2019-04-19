using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using static Android.Views.View;



namespace abtrackslogin
{
    [Activity(Label = "DeviceList")]
    public class DeviceList : Activity, IOnClickListener
    {
        //create a list to store the item
        private List<string> mItems;

        // private ListViewAdapter adapter;
        //create a sa listview
        private ListView list_device;
        //calling the database link
        private const string FirebaseURL = "https://abtrackslogin.firebaseio.com/.json/"; //Firebase Database URL
        //create a button
        Button btn_back;
        //storing value of the map link
        public static string maplink;
        //storing vaule of the name
        public static string name = "No Tracker Selected";



        // Firebase myfirebase;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;
        //create an entry point of the Firebase database 
        DatabaseReference mDatabase;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Display Splash Screen for 4 Sec
            // Set our view from the "DeviceList" StartActivity
            //StartActivity(typeof(DeviceList));
            SetContentView(Resource.Layout.DeviceList);
            //calling the api to the main activity
            auth = FirebaseAuth.GetInstance(MainActivity.app);
            //calling the list to the axml
            list_device = FindViewById<ListView>(Resource.Id.list_device);
            //calling the back button in the axml
            btn_back = FindViewById<Button>(Resource.Id.devicelist_btnback);

            mItems = new List<string>();
            mItems.Add("356940032033323");


            //DatabaseReference peopleReference = FirebaseDatabase.GetInstance(FirebaseURL).GetReference(auth.CurrentUser.Uid);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            list_device.Adapter = adapter;

            list_device.ItemClick += listitemClick;

            btn_back.Click += BckClick;



        }


      
            private void listitemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
        
            maplink = "http://abtracks.co.nf/356940032033323.html";
            name = "Bogart Poe";

            // changing to ViewMap form
            StartActivity(new Intent(this, typeof(ViewMap)));
            // create a toast when changing form to display the form name
            Toast.MakeText(this, "View Map", ToastLength.Short).Show();
            //closing the current form
            Finish();
        }
           

        private void BckClick(object sender, EventArgs e)
        {
            

            // changing to ViewMap form
            StartActivity(new Intent(this, typeof(ViewMap)));
            // create a toast when changing form to display the form name
            Toast.MakeText(this, "View Map", ToastLength.Short).Show();
            //closing the current form
            Finish();
        }

       


        public void OnClick(View v)
        {
            
        }

    }

   
}