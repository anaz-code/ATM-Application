using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Model
{
   public class Customer
    {
        private int _Customer_ID;

        public int Customer_ID
        {
            get { return _Customer_ID; }
            set { _Customer_ID = value; }
        }
        private string _Customer_Name;

        public string Customer_Name
        {
            get { return _Customer_Name; }
            set { _Customer_Name = value; }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private int _Cust_Phone;

        public int Cust_Phone
        {
            get { return _Cust_Phone; }
            set { _Cust_Phone = value; }
        }
        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }





    }
}
