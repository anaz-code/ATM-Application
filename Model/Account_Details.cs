using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Model
{
    public class Account_Details
    {
        private int _Acc_No;

        public int Acc_No
        {
            get { return _Acc_No; }
            set { _Acc_No = value; }
        }
        private int _Customer_ID;

        public int Customer_ID
        {
            get { return _Customer_ID; }
            set { _Customer_ID = value; }
        }
        private string _Acc_Type;

        public string Acc_Type
        {
            get { return _Acc_Type; }
            set { _Acc_Type = value; }
        }
        private int _PIN;

        public int PIN
        {
            get { return _PIN; }
            set { _PIN = value; }
        }
        private int _Balance;

        public int Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }


    }
}
