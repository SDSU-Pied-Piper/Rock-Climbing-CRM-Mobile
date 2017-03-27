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
using SignaturePad;
using System.IO;
using Android.Graphics;
using static Android.Graphics.Bitmap;
using static RockWall.Resource;
using RockWall.Resources.DBMODEL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Net;
namespace RockWall
{
    [Activity(Label = "Waiver")]
    public class Waiver : Activity
    {
        Button mButtonSignUp;
        PatronsDB Pdb;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Pdb = new PatronsDB(); // Create or start local database
            Pdb.createDataBase();
     

            // Create your application here
            SetContentView(Resource.Layout.layout3);

            mButtonSignUp = FindViewById<Button>(Resource.Id.signUpButton);//its the button for the next in sign up page to get to waiver page
            mButtonSignUp.Click += MButtonSignUp_Click;
        }



        private void MButtonSignUp_Click(object sender, EventArgs e)
        {


            ///////////////////////////////////////////////////Convert Image to BitMap
            var sig = new SignaturePadView(this);
            sig = FindViewById<SignaturePadView>(Resource.Id.signaturePadView1);
            sig.BackgroundColor=Color.Transparent;
            var signatureImage = sig.GetImage();

            byte[] bitmapData;
            using (var memStream = new MemoryStream())
            {
                signatureImage.Compress(Bitmap.CompressFormat.Jpeg, 0, memStream);
                bitmapData = memStream.ToArray();
            }

            Bitmap test = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
            ///////////////////////////////////////////////////////////////////////////////////

            Patron newPatron = new Patron();

            Intent intent = new Intent(this, typeof(ConfirmSignUp));
         
           
            newPatron.firstName = Intent.GetStringExtra("FirstName");
            newPatron.lastName = Intent.GetStringExtra("LastName");
            newPatron.email = Intent.GetStringExtra("Email");
            newPatron.studentID = -1;
            newPatron.gender = 1;   ///NEED TO PASS THIS IN YET
            newPatron.isBlayCertified = false;
            newPatron.isLeadCertified = false;
            newPatron.isSuspended = false;
            newPatron.isWaiverSigned = true;
            newPatron.waiverSigned = bitmapData;
            newPatron.dateCreated = DateTime.Now.ToShortDateString();
            newPatron.dateOfBirth = Intent.GetStringExtra("DateofBirth").ToString().Replace('.', '/');
    
            newPatron.lastCheckIn = DateTime.Now.ToShortDateString();

           // Pdb.InsertIntoTablePatron(newPatron);
        
            
          

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                // manipulate UI controls

                Database db = new Database();
                SqlCommand command = new SqlCommand("dbo.InsertPatron", db.con);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10800;
                command.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = newPatron.firstName;
                command.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = newPatron.lastName;
                command.Parameters.Add("@studentID", SqlDbType.Int).Value = newPatron.studentID;
                command.Parameters.Add("@dateOfBirth", SqlDbType.SmallDateTime).Value = newPatron.dateOfBirth;
                command.Parameters.Add("@gender", SqlDbType.SmallInt).Value = newPatron.gender;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = newPatron.email;
                command.Parameters.Add("@isWaiverSigned", SqlDbType.Bit).Value = newPatron.isWaiverSigned;
                command.Parameters.Add("@isBlayCertified", SqlDbType.Bit).Value = newPatron.isBlayCertified;
                command.Parameters.Add("@isLeadCertified", SqlDbType.Bit).Value = newPatron.isLeadCertified;
                command.Parameters.Add("@isSuspended", SqlDbType.Bit).Value = newPatron.isSuspended;
                command.Parameters.Add("@suspensionReason", SqlDbType.NVarChar).Value = newPatron.reasonSuspended;
                command.Parameters.Add("@dateSuspended", SqlDbType.SmallDateTime).Value = newPatron.dateSuspended;
                command.Parameters.Add("@dateUnsuspended", SqlDbType.SmallDateTime).Value = newPatron.dateUnSuspended;
                command.Parameters.Add("@dateOfCreation", SqlDbType.SmallDateTime).Value = newPatron.dateCreated;
                command.Parameters.Add("@lastCheckIn", SqlDbType.SmallDateTime).Value = newPatron.lastCheckIn;
                command.Parameters.Add("@image", SqlDbType.VarBinary).Value = newPatron.waiverSigned;
                command.Parameters.Add("@displayUserID", SqlDbType.Int).Direction = ParameterDirection.Output;


                newPatron.ID = 0;
                while (newPatron.ID == 0)
                {
                    try
                    {
                        if (command.Connection.State == ConnectionState.Closed)
                        {
                            command.Connection.Open();
                        }
                        command.ExecuteNonQuery();
                        newPatron.ID = Convert.ToInt32(command.Parameters["@displayUserID"].Value);
                        Console.WriteLine(newPatron.ID);
                        Console.WriteLine("\n\n\n\n\n");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.ToString()}");
                    }
                    finally
                    {
                    }
                }

            })).Start();

            this.StartActivity(intent);
        }




        Bitmap save(View v)
        {
            Bitmap b = Bitmap.CreateBitmap(v.Width, v.Height, Bitmap.Config.Argb8888);
            Canvas c = new Canvas(b);
            v.Draw(c);
            return b;
        }

    }
}