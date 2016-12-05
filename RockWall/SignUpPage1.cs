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

namespace RockWall
{
    [Activity(Label = "SignUpPage1")]
    public class SignUpPage1 : Activity
    {
        Button mButtonNext;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.layout2);
            mButtonNext = FindViewById<Button>(Resource.Id.btnDialogEmail);//its the button for the next in sign up page to get to waiver page
            mButtonNext.Click += MButtonNext_Click;

        }

        private void MButtonNext_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Waiver));
            this.StartActivity(intent);
        }
    }
}