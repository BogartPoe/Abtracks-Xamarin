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
using Firebase.Database;
using Firebase.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Threading.Tasks;
using Android.Support.V7.Widget;


namespace abtrackslogin
{
    [Activity(Label = "ManageTracker")]
    public class ManageTracker : Activity
    {
        private EditText input_name, input_email;
        private ListView list_data;
        private ProgressBar circular_progress;
        private List<Account> list_users = new List<Account>();
        private ListViewAdapter adapter;
        private Account selectedAccount;
        private const string FirebaseURL = "https://abtrackslogin.firebaseio.com/"; //Firebase Database URL
        DatabaseReference mDatabase;
        FirebaseAuth auth;
        private Button btnback,btndelete;
        //.string udi = auth.CurrentUser;
        public static string name = "No Tracker Selected";
        public static string map;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ManageTracker);

            //View 
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            circular_progress = FindViewById<ProgressBar>(Resource.Id.circularProgress);
            input_name = FindViewById<EditText>(Resource.Id.name);
            input_email = FindViewById<EditText>(Resource.Id.email);
            list_data = FindViewById<ListView>(Resource.Id.list_data);
            btnback = FindViewById<Button>(Resource.Id.manageTracker_back);
            btndelete = FindViewById<Button>(Resource.Id.manageTracker_delete);

            list_data.ItemClick += (s, e) =>
            {
                Account account = list_users[e.Position];
                selectedAccount = account;
                input_name.Text = account.name;
                input_email.Text = account.map;
               
            };

            await LoadData();

            btnback.Click += BackToMap;
            btndelete.Click += deleteDb;

        }

        private void deleteDb(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetTitle("Delete Tracker");
            alertDialog.SetMessage("Do you like to delete this tracker?");
            alertDialog.SetNeutralButton("OK", delegate {


                try
                {
                    DeleteUserAsync(selectedAccount.uid);
                }
                catch (Exception i)
                {
                    Toast.MakeText(this, "No Tracker Selected", ToastLength.Short).Show();
                }

            });

            alertDialog.SetNegativeButton("Cancel", delegate
            {
                alertDialog.Dispose();
            });

            alertDialog.Show();
        
        }

        private async void DeleteUserAsync(string uid)
        {
        
          
            var firebase = new FirebaseClient(FirebaseURL);
            await firebase.Child(auth.CurrentUser.Uid).Child(uid).DeleteAsync();
            await LoadData();
            input_name.Text = "";
            input_email.Text = "";
            Main.map = "";
            Main.name = "No Tracker Selected";
            Toast.MakeText(this, "Tracker Deleted", ToastLength.Short).Show();



        }

        private void BackToMap(object sender, EventArgs e)
        {
            // changing to ViewMap form
            StartActivity(new Intent(this, typeof(MainDashBoard)));
            // create a toast when changing form to display the form name
            Toast.MakeText(this, "View Map", ToastLength.Short).Show();
            //closing the current form
            Finish();
        }

        private async Task LoadData()
        {

            var firebase = new FirebaseClient(FirebaseURL);
            var items = await firebase
                .Child(auth.CurrentUser.Uid)
                .OnceAsync<Account>();

            list_users.Clear();
            adapter = null;

            foreach (var item in items)
            {
                Account account = new Account();
                account.uid = item.Key;
                account.name = item.Object.name;
                account.map = item.Object.map;

                list_users.Add(account);
            }

            adapter = new ListViewAdapter(this, list_users);
            adapter.NotifyDataSetChanged();
            list_data.Adapter = adapter;
        }
    }
}