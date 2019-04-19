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


namespace abtrackslogin
{
    [Activity(Label = "DashBoard")]
    public class DashBoard : Activity, IOnClickListener, IOnCompleteListener
    {
        //create a text where the email or the user will display
        TextView txtWelcome;
        //create a password input and re input password
        EditText input_new_password,reinput_new_password,confirm_password;
        //create a button to changepass and back                            note: btnlogout is back button to the maindashboard form
        Button btnChangePass, btnLogout;
        //create a main layout
        RelativeLayout activity_dashboard;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "DashBoard" layout resource            note: dashboard is for changing password
            SetContentView(Resource.Layout.DashBoard);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting txt welcome in axml
            txtWelcome = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            //connecting input password in axml
            input_new_password = FindViewById<EditText>(Resource.Id.dashboard_newpassword);
            //connecting reinput password in axml
            reinput_new_password = FindViewById<EditText>(Resource.Id.dashboard_renewpassword);
            //connecting button change password in axml
            btnChangePass = FindViewById<Button>(Resource.Id.dashboard_btn_change_pass);
            //connecting button back in axml
            btnLogout = FindViewById<Button>(Resource.Id.dashboard_btn_logout);
            //connecting layout in axml
            activity_dashboard = FindViewById<RelativeLayout>(Resource.Id.activity_dashboard);

            confirm_password = FindViewById<EditText>(Resource.Id.dashboard_confirmpassword);

            //onclick action
            //create a action to click the change password
            btnChangePass.SetOnClickListener(this);
            //create a action to click the back
            btnLogout.SetOnClickListener(this);


            //Checking session
            //create a funtion that display the email
            if (auth.CurrentUser != null)
                txtWelcome.Text = auth.CurrentUser.Email;
        }

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when reset password button is click
            if (v.Id == Resource.Id.dashboard_btn_change_pass)
            {
                //create sa funtion that change password
                LoginUser(auth.CurrentUser.Email, confirm_password.Text);

                //ChangePassword(input_new_password.Text);

            }
            //action when back button is click
            else if (v.Id == Resource.Id.dashboard_btn_logout)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(MainDashBoard)));
                //closing the current form
                Finish();
            }               
        }

        //create a method to logout the user after changing the password
        private void LogoutUser()
        {
            //closing the session
            auth.SignOut();
            //funtion if the user is log out
            if (auth.CurrentUser == null)
            {
                // changing to MainDashBoard form
                StartActivity(new Intent(this, typeof(MainActivity)));
                //closing the current form
                Finish();
            }
        }
        //confirm password
        private void LoginUser(string email, string password)
        {
            try
            {
                //checking the email is exsisting and if the password matches in the email
                auth.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
            }
            catch (Exception e)
            {
                //create a exception if the email is blank
                Toast.MakeText(this, "Failed", ToastLength.Short).Show();
            }
        }
        
        //create a method that change the password
        private void ChangePassword(string newPassword)
        {
            try
            {
                // create a funtion that the user must re enter the password
                if (input_new_password.Text == reinput_new_password.Text)
                {
                    //create a funtion that change password
                    FirebaseUser user = auth.CurrentUser;
                    user.UpdatePassword(newPassword)
                        .AddOnCompleteListener(this,this);

                    //user.Reauthenticate
                    StartActivity(new Intent(this, typeof(MainActivity)));
                    //create a toast when the user logout
                    Toast.MakeText(this, "Successfully change", ToastLength.Short).Show();
                    //closing the current form
                    Finish();



                }
                else
                {
                    //creeate a funtion that catch is the password is not match
                    Toast.MakeText(this, "Password do not match", ToastLength.Short).Show();
                }               
            }
            catch (Exception e)
            {
                //creeate a exception if the email is exsisting or empty
                Toast.MakeText(this, " Failed", ToastLength.Short).Show();
            }
           
        }

        //IOnCompleteListener
        //create a listener called when a Task completes
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {

                ChangePassword(input_new_password.Text);

                //FirebaseUser user = auth.CurrentUser;
                //user.UpdatePassword(input_new_password.Text)
                //    .AddOnCompleteListener(this,this);

                // changing to MainDashBoard form
              


            }
            else
            {

                Toast.MakeText(this, "Invalid Password", ToastLength.Short).Show();
            }
        }
    }
    

}