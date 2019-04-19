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
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;


namespace abtrackslogin
{
    [Activity(Label = "ForgotPassword")]
    public class ForgotPassword : Activity, IOnClickListener, IOnCompleteListener
    {
        //create an input email
        private EditText input_email;
        //create a button for reset password
        private Button btnResetPass;
        //create a button for back
        private TextView btnBack;
        //create a main layout
        private RelativeLayout activity_forgot;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "ForgotPassword" layout resource
            SetContentView(Resource.Layout.ForgotPassword);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting input email in axml
            input_email = FindViewById<EditText>(Resource.Id.forgot_email);
            //connecting button reset in axml
            btnResetPass = FindViewById<Button>(Resource.Id.forgot_btn_reset);
            //connecting button back in axml
            btnBack = FindViewById<TextView>(Resource.Id.forgot_btn_back);
            //connecting layout in axml
            activity_forgot = FindViewById<RelativeLayout>(Resource.Id.activity_forgot);

            //onclick action
            //create a action to click the reset password button
            btnResetPass.SetOnClickListener(this);
            //create a action to click the back button
            btnBack.SetOnClickListener(this);
        }

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when back button is click
            if (v.Id == Resource.Id.forgot_btn_back)
            {
                //changing to MainActivity form
                StartActivity(new Intent(this, typeof(MainActivity)));
                //closing the current form
                Finish();
            }
            //action when reset button is click
            else if (v.Id == Resource.Id.forgot_btn_reset)
            {
                //sending an email to reset the password
                ResetPassword(input_email.Text);
            }
        }

        //creeate a method that send an email to reset the password
        private void ResetPassword(string email)
        {
            try
            {
                //create a funtion that send reset password to the email
                auth.SendPasswordResetEmail(email)
               .AddOnCompleteListener(this, this);
            }
            catch (Exception e)
            {
                 //create exception if the email is blank
                Toast.MakeText(this, "Failed", ToastLength.Short).Show();
            }
        }

        //IOnCompleteListener
        //create a listener called when a Task completes
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                //changing the form to maintactivity
                StartActivity(new Intent(this, typeof(MainActivity)));
                //close the current form
                Finish();
                //create a toast if the send is success
                Toast.MakeText(this, "Successfully Send", ToastLength.Short).Show();
            }
            else
            {
                //create a toast if the sending failed
                Toast.MakeText(this, "Failed to Send", ToastLength.Short).Show();
            }
        }
    }
}