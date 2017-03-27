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
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using RockWall.Resources.DBMODEL;
using Android.Text;
using Android.Graphics.Drawables;

namespace RockWall
{
    class Database
    {
        private const string connectionString = "Data Source = sqlpiedpiper.database.windows.net; Initial Catalog=RockWallSDSU; Persist Security Info = False; User ID = devadmin; Password = P@ssword;Connection Timeout=30";

        public SqlConnection con { get; private set; }
        private SqlDataAdapter adapt;

        public Database()
        {
            con = new SqlConnection(connectionString);
        }



        public DataSet sendSelectCommand(SqlCommand command)

        {

            DataSet returnedData = new DataSet();

            con.Open();

            adapt = new SqlDataAdapter(command);

            adapt.SelectCommand = command;

            adapt.Fill(returnedData);

            con.Close();

            return returnedData;

        }



        public void sendInsertCommand(SqlCommand command)

        {

            command.Connection = con;
            command.CommandType = CommandType.Text;
            try
            {

                con.Open();

                int recordsAffected = command.ExecuteNonQuery();

            }

            catch (Exception ex)
            { Console.WriteLine($"{ex.ToString()}"); }

            finally
            {
                con.Close();
            }
            return;
        }
        


        public void sendDeleteCommand(SqlCommand command)

        {

            command.Connection = con;
            command.CommandType = CommandType.Text;

            try

            {
                con.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }

            catch (SqlException ex)

            {
            }

            finally
            {
                con.Close();
            }
            return;
        }



        public void sendUpdateCommand(SqlCommand command)

        {

            command.Connection = con;

            command.CommandType = CommandType.Text;
            bool good = true;
            while (good)
            { 

                try

                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int recordsAffected = command.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        good=false;
                    }

                }

                catch (SqlException ex)

                {
                    Console.WriteLine($"{ex.ToString()}");
                }

                finally

                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        good = false;
                    }

                }
            }
            return;

        }

    }//End of database class

    public class CreateUserEventArgs : EventArgs
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string DateOfBirth { set; get; }
        public string StudentID { set; get; }
        public string PhoneNum { set; get; }

        public CreateUserEventArgs(string name, string email, string address, string dateofbirth, string studentid, string phonenum)
        {
            Name = name;
            Email = email;
            Address = address;
            DateOfBirth = dateofbirth;
            StudentID = studentid;
            PhoneNum = phonenum;
        }


    }



    [Activity(Label = "SignUpPage1")]
    public class SignUpPage1 : Activity
    {
        EditText txtFirstName;
        EditText txtLastName;
        EditText txtEmail;
        EditText txtDateOfBirth;
        EditText txtStudentID;
        Button mButtonNext;
        PatronsDB Pdb;
        List<Patron> firstList = new List<Patron>();

        Resources.DBMODEL.Patron tempPatron = new Patron();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Pdb = new PatronsDB(); // Create or start local database
            Pdb.createDataBase();

            SetContentView(Resource.Layout.layout2);
            mButtonNext = FindViewById<Button>(Resource.Id.btnDialogEmail);//its the button for the next in sign up page to get to waiver page
            txtFirstName = FindViewById<EditText>(Resource.Id.txtFirstName);
            txtLastName = FindViewById<EditText>(Resource.Id.txtLastName);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtDateOfBirth = FindViewById<EditText>(Resource.Id.txtUserDOB);
            txtStudentID = FindViewById<EditText>(Resource.Id.txtUserStudentID);
            

            EditText Test = (EditText)FindViewById(Resource.Id.txtFirstName);
          

            mButtonNext.Click += MButtonNext_Click;

        }


        private void MButtonNext_Click(object sender, EventArgs e)
        {
            if (txtFirstName != null)
                tempPatron.firstName = txtFirstName.Text.ToString();
            tempPatron.lastName = txtLastName.Text.ToString();
            tempPatron.dateOfBirth = DateTime.Parse(txtDateOfBirth.Text).ToString();
            tempPatron.email = txtEmail.ToString();
          //  Pdb.InsertIntoTablePatron(tempPatron);

     


            //Go to next page
            Intent intent = new Intent(this, typeof(Waiver));
            intent.PutExtra("FirstName", tempPatron.firstName);   ///Pass Args to next Activity
            intent.PutExtra("LastName", tempPatron.lastName);
            intent.PutExtra("DateofBirth", txtDateOfBirth.Text.ToString());
            intent.PutExtra("Email", tempPatron.email);
            this.StartActivity(intent);

        }
    }
}
