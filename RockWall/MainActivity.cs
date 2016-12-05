using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using System;
using System.Threading;
using Android.Content;

namespace RockWall
{
    [Activity(Label = "RockWall", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mButtonCheckIn;
        Button mButtonSignIn;
        Button mButtonSignedWaiver;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            mButtonCheckIn = FindViewById<Button>(Resource.Id.CheckInButton);
            mButtonSignIn = FindViewById<Button>(Resource.Id.CreateAccountButton);
            mButtonSignIn.Click += MButtonSignIn_Click;

            mButtonCheckIn.Click += (object sender, EventArgs args) =>
            {
                //pull up dialog 
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogCheckIn checkinDialog = new DialogCheckIn();
                checkinDialog.Show(transaction, "dialog fragment");

                checkinDialog.mOnCheckInComplete += CheckinDialog_mOnCheckInComplete; //Hey button is clicked and you will recieve the arguments
               
               

            };

        }

        private void MButtonSignIn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SignUpPage1));
            this.StartActivity(intent);
           // this.FinishActivity(1);///When the user clicks "back" then the UI is completely exited and info is erased
        }

        private void CheckinDialog_mOnCheckInComplete(object sender, OnCheckInEventArgs e) //Method to handle arguments from check in
        {

            //Send request to the server to check the ID.

            //Simulated request
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
 

        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);
        }
    }

    
}

