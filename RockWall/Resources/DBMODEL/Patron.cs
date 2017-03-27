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
using SQLite;

namespace RockWall.Resources.DBMODEL
{
    class Patron
    {
      
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int studentID { get; set; }
        public string dateOfBirth { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public bool isWaiverSigned { get; set; }
        public byte[] waiverSigned { get; set; }
        public bool isBlayCertified { get; set; }
        public bool isLeadCertified { get; set; }
        public bool isSuspended { get; set; }
        public string reasonSuspended { get; set; }
        public string dateSuspended { get; set; }
        public string dateUnSuspended { get; set; }
        public string dateCreated { get; set; }
        public string lastCheckIn { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

    }
}