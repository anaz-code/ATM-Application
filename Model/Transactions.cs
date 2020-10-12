using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Model
{
    public class Transactions
    {
        private int _T_Id;

        public int T_Id
        {
            get { return _T_Id; }
            set { _T_Id = value; }
        }
        private int _Customer_Id;

        public int Customer_Id
        {
            get { return _Customer_Id; }
            set { _Customer_Id = value; }
        }
        private int _Acc_No;

        public int Acc_No
        {
            get { return _Acc_No; }
            set { _Acc_No = value; }
        }
        private string _T_Type;

        public string T_Type
        {
            get { return _T_Type; }
            set { _T_Type = value; }
        }
        private int _T_Amount;

        public int T_Amount
        {
            get { return _T_Amount; }
            set { _T_Amount = value; }
        }
        private DateTime _T_DateTime;

        public DateTime T_DateTime
        {
            get { return _T_DateTime; }
            set { _T_DateTime = value; }
        }

        public Nullable<double> DebitT_Amount { get; internal set; }
        public Nullable<double> CreditT_Amount { get; internal set; }
    }
}
