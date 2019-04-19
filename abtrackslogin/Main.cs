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
using Firebase.Database;
using Firebase.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;


namespace abtrackslogin
{
    [Activity(Label = "Main")]
    public class Main : Activity
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
        private Button btnback;
        //.string udi = auth.CurrentUser;
        public static string name = "No Tracker Selected";
          public static string map;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Main);

            //View 
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            circular_progress = FindViewById<ProgressBar>(Resource.Id.circularProgress);
            //input_name = FindViewById<EditText>(Resource.Id.name);
            input_email = FindViewById<EditText>(Resource.Id.email);
            list_data = FindViewById<ListView>(Resource.Id.list_data);
            btnback = FindViewById<Button>(Resource.Id.main_back);

            list_data.ItemClick += (s, e) =>
            {
                Account account = list_users[e.Position];
                selectedAccount = account;
                //input_name.Text = account.name;
                input_email.Text = account.map;
                map = "http://abtracks.co.nf/" + account.map + ".html";
                name = account.name;

                // changing to ViewMap form
                StartActivity(new Intent(this, typeof(ViewMap)));
                // create a toast when changing form to display the form name
                Toast.MakeText(this, "View Map", ToastLength.Short).Show();
                //closing the current form
                Finish();
            };

            await LoadData();

            btnback.Click += BackToMap;

        }

        private void BackToMap(object sender, EventArgs e)
        {
            // changing to ViewMap form
            StartActivity(new Intent(this, typeof(ViewMap)));
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