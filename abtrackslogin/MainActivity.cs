using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Views;
using Firebase;
using System;


namespace abtrackslogin
{
    [Activity(Label = "Abtracks", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        //create a button for logging in
        Button btnLogin;
        //create a email and password input
        EditText input_email, input_password;
        //create a clickable text button for signup and forgot password
        TextView btnSignUp, btnForgotPassword;
        //create a main layout
        RelativeLayout activity_main;
        //create an entry point for firebase sdk
        public static FirebaseApp app;
        //create an entry point of the Firebase Authentication
        FirebaseAuth auth;
        //create a login counter
        public static int count;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "activity_main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //methods
            //Init Firebase
            InitFirebaseAuth();

            //connectors
            //connecting button login in axml
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            //connecting input email in axml
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            //connecting input password in axml
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            //connecting button signup in axml
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            //connecting button forgot password in axml
            btnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            //connecting layout in axml
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);

            //onclick action
            //create a action to click the sign up button
            btnSignUp.SetOnClickListener(this);
            //create a action to click the login button
            btnLogin.SetOnClickListener(this);
            //create a action to click the forgotpassword button
            btnForgotPassword.SetOnClickListener(this);

        }

        //create a method that connects in firebase
        private void InitFirebaseAuth()
        {
            //create a configurable firebase options
            var options = new FirebaseOptions.Builder()
            //get the firebase id
            .SetApplicationId("1:392122600866:android:8049a416716eb8fa")
            //get the firebase apikey
            .SetApiKey("AIzaSyC2iFZaUMyhpZBnkgkIx0GH-xn469ZANMU")
            .Build();

            //create listener/Observer to get the userid for every auth state change
            if (app == null)                        
                    app = FirebaseApp.InitializeApp(this, options);
                    auth = FirebaseAuth.GetInstance(app);
               
             
            
              
        }
        

        //IOnClickListener
        //create a interface definition for a callback to be invoked when a view is clicked
        public void OnClick(View v)
        {
           //action when login button is click
            if (v.Id == Resource.Id.login_btn_login)
            {
                //calling a method that authenticates sa email and password
                LoginUser(input_email.Text, input_password.Text);

            }
            //action when forgotpassword button is click
            else if (v.Id == Resource.Id.login_btn_forgot_password)
            {
                //changing to forgot password form
                StartActivity(new Android.Content.Intent(this, typeof(ForgotPassword)));
                //closing the current form
                Finish();
            }
            //action when signup button is click
            else if (v.Id == Resource.Id.login_btn_sign_up)
            {
                //changing to sign up form
                StartActivity(new Android.Content.Intent(this, typeof(SignUp)));
                //closing the current form
                Finish();
            }
        }

        //creaTE a method to authenticate the email and password
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
                Toast.MakeText(this, "Cannot found email or password ", ToastLength.Short).Show(); 
            }                              
        }

        //IOnCompleteListener
        //create a listener called when a Task completes.
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                
                try
                {
                    //getting the user logging in
                    FirebaseUser user = auth.CurrentUser;
                    //checking if the user is verify
                    if (user.IsEmailVerified)
                    {
                        //changing to the maindashboard form
                        StartActivity(new Android.Content.Intent(this, typeof(MainDashBoard)));
                        //closing the current form
                        Finish();
                        //create a toast when the password is rigth
                        Toast.MakeText(this, "Successfully Login", ToastLength.Short).Show();
                    }
                    else
                    {
                        //sending email verification to the email
                       // user.SendEmailVerification()
                        //  .AddOnCompleteListener(this, this);
                        //create a toast when the email is no verify
                       Toast.MakeText(this, "Please verify your email", ToastLength.Short).Show();
                       // Finish();


                    }
                }
                catch(Exception a)
                {
                    Toast.MakeText(this, "Cannot found email or password", ToastLength.Short).Show();
                }
               
            }
            else
            {
                /**
                //create a function if 3 attempts it will sent an reset password
                if (count == 3)
                {
                    FirebaseUser user = auth.CurrentUser;

                    
                    //reset password
                    auth.SendPasswordResetEmail(input_email.Text)
                    .AddOnCompleteListener(this, this);
                    //toast the indicate the sending of reset of password
                    Toast.MakeText(this, "Reset Password Send", ToastLength.Short).Show();
                    //reset the login attemp to 0
                    //count ++;
                    count = 0;


                }
                
                else if (count == 4)
                {
                    Toast.MakeText(this, "3 more attempts", ToastLength.Short).Show();
                    count++;
                }
                else if (count == 5)
                {
                    Toast.MakeText(this, "2 more attempts", ToastLength.Short).Show();
                    count++;
                }
                else if (count == 6)
                {
                    Toast.MakeText(this, "Last attempt", ToastLength.Short).Show();
                    count++;
                }
                else if (count == 7)
                {
                    FirebaseUser user = auth.CurrentUser;
                  //  user.UpdateProfile(status);

                    

                    Toast.MakeText(this, "Account Disable", ToastLength.Short).Show();
                    count=0;
                    input_email.Text = "";
                    input_password.Text = "";
                }*/
               
                    //create a toast when the password is wrong
                    Toast.MakeText(this, "Cannot found email or password", ToastLength.Short).Show();
                    //increment the login attemt
                    count++;
                
               
            }
        }
    }
}

