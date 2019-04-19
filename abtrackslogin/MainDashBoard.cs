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
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Firebase.Database;


namespace abtrackslogin
{
    [Activity(Label = "MainDashBoard")]
    public class MainDashBoard : Activity, IOnClickListener
    {
        //create a text where the email or the user will display
        TextView txtWelcome;
        //create a button to changepass,logout,viewmap and addtracker     
        Button btnChangePass, btnLogout,btnViewMap,btnaddtracker,btnmanagetracker;
        //create a main layout
        RelativeLayout activity_dashboard;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "MainDashBoard" layout resource 
            SetContentView(Resource.Layout.MainDashBoard);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting txt welcome in axml
            txtWelcome = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            //connecting button change map in axml
            btnViewMap = FindViewById<Button>(Resource.Id.dashboard_btn_change_map);
            //connecting button addtracker in axml
            btnaddtracker = FindViewById<Button>(Resource.Id.dashboard_btn_addtracker);
            //connecting button change password in axml
            btnChangePass = FindViewById<Button>(Resource.Id.dashboard_btn_change_pass);
            //connecting button logout in axml
            btnLogout = FindViewById<Button>(Resource.Id.dashboard_btn_logout);

            btnmanagetracker = FindViewById<Button>(Resource.Id.dashboard_btn_editTracker);
            //connecting layout in axml
            activity_dashboard = FindViewById<RelativeLayout>(Resource.Id.activity_dashboard);

            //onclick action
            //create a action to click the change password
            btnChangePass.SetOnClickListener(this);
            //create a action to click the logout
            btnLogout.SetOnClickListener(this);
            //create a action to click the viewmap
            btnViewMap.SetOnClickListener(this);
            //create a action to click the addtracker
            btnaddtracker.SetOnClickListener(this);

            btnmanagetracker.SetOnClickListener(this);

            //Checking session
            //create a funtion that display the email
            if (auth.CurrentUser != null)
                txtWelcome.Text = "Welcome , " + auth.CurrentUser.Email;
        }

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when change password button is click
            if (v.Id == Resource.Id.dashboard_btn_change_pass)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(DashBoard)));
                //create a toast when changing form to display the form name
                Toast.MakeText(this, "Changing Password", ToastLength.Short).Show();
                //closing the current form
                Finish();
            }
            //action when change mapbutton is click
            else if (v.Id == Resource.Id.dashboard_btn_change_map)
            {
                // changing to ViewMap form
                StartActivity(new Intent(this, typeof(ViewMap)));
                // create a toast when changing form to display the form name
                Toast.MakeText(this, "View Map", ToastLength.Short).Show();
                //closing the current form
                Finish();
            }
            //action when addtracker button is click
            else if (v.Id == Resource.Id.dashboard_btn_addtracker)
            {
                // changing to AddTracker form
                StartActivity(new Intent(this, typeof(AddTracker)));
                // create a toast when changing form to display the form name
                Toast.MakeText(this, "Add Tracker", ToastLength.Short).Show();
                //closing the current form
                Finish();
            }
            else if (v.Id == Resource.Id.dashboard_btn_editTracker)
            {
                // changing to AddTracker form
                StartActivity(new Intent(this, typeof(ManageTracker)));
                // create a toast when changing form to display the form name
                Toast.MakeText(this, "Delete Tracker", ToastLength.Short).Show();
                //closing the current form
                Finish();
            }
            //action when logout button is click
            else if (v.Id == Resource.Id.dashboard_btn_logout)
            {
                LogoutUser();
                Main.map = "";
                Main.name = "No Tracker Selected";
            }
        }

        //create a method to logout the user
        private void LogoutUser()
        {
            //closing the session
            auth.SignOut();
            //funtion if the user is log out
            if (auth.CurrentUser == null)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(MainActivity)));
                //create a toast when the user logout
                Toast.MakeText(this, "Logout", ToastLength.Short).Show();
                //closing the current form
                Finish();
            }
        }     
    }
}