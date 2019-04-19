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
using Android.Views;
using static Android.Views.View;
using Firebase;
using Firebase.Auth;
using Firebase.Provider;
using Android.Gms.Tasks;


namespace abtrackslogin
{
    [Activity(Label = "SignUp")]
    public class SignUp : Activity,  IOnClickListener, IOnCompleteListener
    {
        //create a button for logging in
        Button btnSignup;
        //create a clickable text button for login and forgot password
        TextView btnLogin, btnForgotPass;
        //create a email and password input and re input password
        EditText input_email, input_password, reinput_password;
        //create a sign up button layout
        RelativeLayout activity_sign_up;
        //create a main layout
        RelativeLayout activity_main;
        //create an entry point for firebase sdk
        public static FirebaseApp app;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "SignUp" layout resource
            SetContentView(Resource.Layout.SignUp);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //connectors
            //connecting button singup in axml
            btnSignup = FindViewById<Button>(Resource.Id.signup_btn_register);
            //connecting button login in axml
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            //connecting input email in axml
            input_email = FindViewById<EditText>(Resource.Id.signup_email);
            //connecting input password in axml
            input_password = FindViewById<EditText>(Resource.Id.signup_password);
            //connecting re input password in axml
            reinput_password = FindViewById<EditText>(Resource.Id.signup_repassword);
            //connecting button singup in axml
            activity_sign_up = FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);

            //onclick action
            //create a action to click the login button
            btnLogin.SetOnClickListener(this);
            //create a action to click the sign up button
            btnSignup.SetOnClickListener(this);
        }

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
            //action when login button is click
            if (v.Id == Resource.Id.signup_btn_login)
            {
                //changing to forgot password form
                StartActivity(new Intent(this, typeof(MainActivity)));
                //closing the current form
                Finish();
            }
            //action when regiter button is click
            else if (v.Id == Resource.Id.signup_btn_register)
            {
                //calling email and password to register
                LoginUser(input_email.Text, input_password.Text);
            }
        }


        //create a method that register email and password
        private void LoginUser(string email, string password)
        {
            try
            {
                //create a funtion that the user must re enter the password
                if (input_password.Text == reinput_password.Text)
                {
                    //create a funtion that save email and password
                    auth.CreateUserWithEmailAndPassword(email, password)
                            .AddOnCompleteListener(this, this);
                    
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
                Toast.MakeText(this, "Failed to Register", ToastLength.Short).Show();
            }
        }

        public void sendVerificationCode()
        {
          
        }

        //IOnCompleteListener
        //create a listener called when a Task completes
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                //get the current user register
                FirebaseUser user = auth.CurrentUser;
                //sending email verification to the email
                user.SendEmailVerification()
                  .AddOnCompleteListener(this, this);
                //changing the form to maintactivity


                var uri = Android.Net.Uri.Parse("https://mail.google.com");
                var intent = new Intent(Intent.ActionView,uri);
                StartActivity(intent);
                //StartActivity(new Intent(this, typeof(MainActivity)));
                //close the current form
                Finish();

         
                //create a toast if the register is success
                Toast.MakeText(this, "Successfully Registered please verify your account", ToastLength.Short).Show();
            }
            else
            {
                //create a toast if the registration failed
                Toast.MakeText(this, "Failed Register", ToastLength.Short).Show();
            }
        }
    }
}