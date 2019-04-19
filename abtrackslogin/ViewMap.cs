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
    [Activity(Label = "ViewMap")]
    public class ViewMap : Activity, IOnClickListener
    {
        //creata a platform to view the map
        WebView webView;
        //create a back button
        Button btnback,btnlog,btnaddlog;
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
            SetContentView(Resource.Layout.ViewMap);
            // Create your application here

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting webview in axml
            webView = FindViewById<WebView>(Resource.Id.webView1);
            //connenting back buttion in axml
            btnback = FindViewById<Button>(Resource.Id.button1);
            btnlog = FindViewById<Button>(Resource.Id.btnlogging);
            btnaddlog = FindViewById<Button>(Resource.Id.button2);
            //connecting text email to the axml
            txtemail = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            //setting webview
            webView.SetWebViewClient(new ExtenWebViewClient());
           
            
            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;

            txtemail.Click += EmailOnclick;

            //Checking session

            
              
            MaplinkFirebase();
           
            //onclick action
            //create a action to click the back button
            btnback.SetOnClickListener(this);
            btnlog.SetOnClickListener(this);
            btnaddlog.SetOnClickListener(this);
        }
        private async void MaplinkFirebase()
        {
            string maplink = Main.map;
            txtemail.Text = Main.name;
            webView.LoadUrl(maplink);

        }
    
        private void EmailOnclick(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(Main)));
            //closing the current form
            Finish();

            Toast.MakeText(this, " Select Tracker", ToastLength.Short).Show();
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

                Main.map = "";
                Main.name = "No Tracker Selected";
            }

            else if (v.Id == Resource.Id.btnlogging)
            {
                StartActivity(new Intent(this, typeof(Devicelistlog)));
                //closing the current form
                Finish();
                Toast.MakeText(this, "Tracker Log", ToastLength.Short).Show();

            }

            else if (v.Id == Resource.Id.button2)
            {
                StartActivity(new Intent(this, typeof(ViewMap)));
                //closing the current form
                Finish();
                Toast.MakeText(this, "Added to Log", ToastLength.Short).Show();

            }
        }
    }
    //creeate a class that holds the url of ther webview
    internal class ExtenWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            //load the url
            view.LoadUrl(url);
            return true;
        }
    }
}