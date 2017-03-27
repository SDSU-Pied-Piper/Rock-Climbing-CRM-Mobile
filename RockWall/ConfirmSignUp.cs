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
using Android.Graphics;

namespace RockWall
{
    [Activity(Label = "ConfirmSignUp")]
    public class ConfirmSignUp : Activity
    {

        ImageView waiverImg;
        Button mButtonNext;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TestOutput);
            mButtonNext = FindViewById<Button>(Resource.Id.btnDialogEmail);//its the button for the next in sign up page to get to waiver page
            waiverImg = FindViewById<ImageView>(Resource.Id.imageView1);

           Bitmap temp = (Bitmap)Intent.GetParcelableExtra("Waiver");
            waiverImg.SetImageBitmap(temp);

          //  mButtonNext.Click += MButtonNext_Click;

        }

        private void MButtonNext_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}