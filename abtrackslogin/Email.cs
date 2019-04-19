using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.Support.V7.App;
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Firebase.Database;


namespace abtrackslogin
{
    [Activity(Label = "Email")]
    public class Email : Activity, IOnClickListener
    {
        //creata a platform to view the map
        WebView webView;
        //create a back button
        Button btnback, btnlog, btnaddlog;
        //create a text that display the current user login
        TextView txtemail;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;
        //create an entry point of the Firebase database 
        DatabaseReference mDatabase;

        DeviceList dl = new DeviceList();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "ViewMap" layout resource
            SetContentView(Resource.Layout.Email);
            // Create your application here

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting webview in axml
            webView = FindViewById<WebView>(Resource.Id.webView1);
            //connenting back buttion in axml
            btnback = FindViewById<Button>(Resource.Id.button1);
     
            webView.SetWebViewClient(new ExtenWebViewClient());


            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;

          //  txtemail.Click += EmailOnclick;

            //Checking session



            MaplinkFirebase();

            //onclick action
            //create a action to click the back button
            btnback.SetOnClickListener(this);
           
        }
        private async void MaplinkFirebase()
        {
            string maplink = "http://mail.google.com";
            txtemail.Text = Main.name;
            webView.LoadUrl(maplink);

        }

       

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when back button is click                              note: button1 is back button
            if (v.Id == Resource.Id.button1)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(MainDashBoard)));
                //closing the current form
                Finish();
              
            }

           
        }
    }

 
}