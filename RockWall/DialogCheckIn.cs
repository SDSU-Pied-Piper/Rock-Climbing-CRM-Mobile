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
using Android.Database.Sqlite;
using System.Data;
using System.Data.SqlClient;
    

namespace RockWall
{
    public class OnCheckInEventArgs : EventArgs
    {
        private string mCheckInID;

        public string CheckID
        {
            get { return mCheckInID; }
            set { mCheckInID = value; }

        }

        public OnCheckInEventArgs(string ID):base()
        {
            mCheckInID= ID;
        }
    }

    class DialogCheckIn :DialogFragment
    {
        private EditText checkIn;
        private Button mBtnCheckIn;
        public event EventHandler<OnCheckInEventArgs> mOnCheckInComplete;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.CheckIn, container, false);
            checkIn=view.FindViewById<EditText>(Resource.Id.editText1);
           mBtnCheckIn=view.FindViewById<Button>(Resource.Id.CheckInButton);


          mBtnCheckIn.Click += MBtnCheckIn_Click; //Create an event kinda

            return view;
        }

       private void MBtnCheckIn_Click(object sender, EventArgs e) // When User Clicks Check in Method. Uses Created Class 
        {
            
            mOnCheckInComplete.Invoke(this, new OnCheckInEventArgs(checkIn.Text)); //broadcast that it accepts the check in text. Invokes event
            this.Dismiss();//Makes the dialog go away. 

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
           // Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}