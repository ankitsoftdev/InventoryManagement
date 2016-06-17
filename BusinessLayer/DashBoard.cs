using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDMEntity
{
    public class DashBoard
    {
        #region  Property for DashBoard Data
        public int Employee_Id { get; set; }
        public int Hotel_Id { get; set; }
        public int Branch_Id { get; set; }
        public int Employees { get; set; }
        public String Email { get; set; }
        public int Branches { get; set; }
        public String[] Roles { get; set; }
        public int Orders { get; set; }
        public int Pending_Booking { get; set; }
        public String Title { get; set; }
        public String HotelName { get; set; }
        public String BranchName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String Employee_Name { get; set; }
        public byte[] File { get; set; }
        public DateTime _Last_Access_Time { get; set; }



        public String TotalVisits { get; set; }
        public String NewVisits { get; set; }
        public String PageViews { get; set; }
        public String CheckInSeries { get; set; }
        public String CheckOutSeries { get; set; }
        public String AmountSeries { get; set; }
        public String DbConnectionString { get; set; }
        #endregion

    }

}
