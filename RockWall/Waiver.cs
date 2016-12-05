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
    [Activity(Label = "Waiver")]
    public class Waiver : Activity
    {
        Button mButtonSignUp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout3);

            mButtonSignUp = FindViewById<Button>(Resource.Id.signUpButton);//its the button for the next in sign up page to get to waiver page
            mButtonSignUp.Click += MButtonSignUp_Click;
        }

        private void MButtonSignUp_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
        }
    }
}