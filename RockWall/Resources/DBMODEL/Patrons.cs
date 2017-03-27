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
using Mono.Data.Sqlite;
using SQLite;


namespace RockWall.Resources.DBMODEL
{
    class PatronsDB
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Patrons.db")))
                {
                    connection.CreateTable<Patron>();
                    return true;

                }
            }
            catch(SQLiteException ex)
            {
                return false;
            }
        }
        public bool InsertIntoTablePatron (Patron patron)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Patrons.db")))
                {
                    connection.Insert(patron);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }

        }
        public List<Patron> selectTablePatron()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Patrons.db")))
                {
                    return connection.Table<Patron>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
        public bool updateTablePatron(Patron patron)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Patrons.db")))
                {
                    connection.Query<Patron>("UPDATE Patron set firstName=?, lastName=?,email=?,dateOfBirth=?", patron.firstName, patron.lastName, patron.email, patron.dateOfBirth,  patron.ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }

        }
        public bool selectQueryTablePatron(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Patrons.db")))
                {
                    connection.Query<Patron>("SELECT * FROM Patron Where ID=?",id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }

        }

    }
}