using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
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
    [Activity(Label = "AddTracker")]
    public class AddTracker : Activity, IOnClickListener, IOnCompleteListener
    {
        //create a back button
        Button back,add;
        //create a text that display the mail
        TextView txtWelcome;
        //create a input to add the tracker id
        EditText input_add,inputname;
       //create an entry point of the Firebase Authentication
       FirebaseAuth auth;
        //create an entry point of the Firebase database 
        DatabaseReference mDatabase;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "AddTracker" layout resource
            SetContentView(Resource.Layout.AddTracker);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting email in axml
            txtWelcome = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            //connecting button back in axml
            back = FindViewById<Button>(Resource.Id.dashboard_btnback);
            //conneting to add button in axml
            add = FindViewById<Button>(Resource.Id.dashboard_btn_add);
            //conneceting input add in the axml
            input_add = FindViewById<EditText>(Resource.Id.dashboard_renewpassword);
            //create an onlick action on add button
            inputname = FindViewById<EditText>(Resource.Id.dashboard_name);

            add.Click += addOnClick;
            //onclick action

            //create a action to click the back button
            back.SetOnClickListener(this);

            //Checking session
            //create a funtion that display the email
            if (auth.CurrentUser != null)
                txtWelcome.Text = "Do you want to Add a Tracker, " + auth.CurrentUser.Email;
        }

        private async void addOnClick(object sender, EventArgs e)
        {
            //only the registered tracker will be enter
            //note: the value is static
            if (input_add.Text == "haabi37wgcv" || input_add.Text == "i43ds6y4954")
            {
                //creating a id to the database
                mDatabase = FirebaseDatabase.GetInstance("https://abtrackslogin.firebaseio.com/").GetReference(auth.CurrentUser.Uid);
                //createing tracker id to the database
                var resp = mDatabase.Child(input_add.Text + "/Map");
                //saving the map information to the database
                await resp.SetValueAsync(input_add.Text);
                //saving the name into firebase
                resp = mDatabase.Child(input_add.Text + "/Name");
                await resp.SetValueAsync(inputname.Text);

                //changing to the maindashboard form
                StartActivity(new Android.Content.Intent(this, typeof(MainDashBoard)));
                //closing the current form
                Finish();
                Toast.MakeText(this, "Successfully Registered", ToastLength.Short).Show();
            }
            else
            {
                //blank the tracker id if invalid
                input_add.Text = "";
                Toast.MakeText(this, "Invalid TrackerID", ToastLength.Short).Show();
            }         
        }

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when back button is click
            if (v.Id == Resource.Id.dashboard_btnback)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(MainDashBoard)));
                //closing the current form
                Finish();
            }
         
        }

        public void OnComplete(Task task)
        {
            throw new NotImplementedException();
        }

    }
}