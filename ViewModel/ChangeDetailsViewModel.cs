using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMApp.Command;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace ATMApp.ViewModel
{
    public class ChangeDetailsViewModel:ViewModelPropertyChanged
    {
        OracleConnection con = null;
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        OracleCommand cmd = null;
        public int Id { get; set; }
        public ChangeDetailsViewModel(int id)
        {
            Id = id;
            DisplayData();
        }

       
        private int _Acc_No;

        public int Acc_No
        {
            get { return _Acc_No; }
            set { _Acc_No = value;
                OnPropertyChange("Acc_No");
            }
        }
        private string _Acc_Type;

        public string Acc_Type
        {
            get { return _Acc_Type; }
            set { _Acc_Type = value;
                OnPropertyChange("Acc_Type");
            }
        }
        private string _Customer_Name;

        public string Customer_Name
        {
            get { return _Customer_Name; }
            set { _Customer_Name = value;
                OnPropertyChange("Customer_Name");
            }
        }
        private double _Cust_Phone;

        public double Cust_Phone
        {
            get { return _Cust_Phone; }
            set { _Cust_Phone = value;
                OnPropertyChange("Cust_Phone");
            }
        }
        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value;
                OnPropertyChange("Address");
            }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value;
                OnPropertyChange("Password");
            }
        }
        public ICommand CmdUpdate
        {
            get
            {
                return new DelegateCommand<object>(UpdateDetails);
            }
        }

        

        private void DisplayData()
        {
            try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader = null;
                cmd = new OracleCommand($"Select Acc_No,Acc_Type,Customer_Name,Cust_Phone,Address,Password from Customer c inner join Account_Details a on c.Customer_ID=a.Customer_ID where c.Customer_ID='{Id}'",con);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    Acc_No = Convert.ToInt32(reader.GetValue(0));
                    Acc_Type =reader.GetValue(1).ToString();
                    Customer_Name = reader.GetValue(2).ToString();
                    Cust_Phone = Convert.ToDouble(reader.GetValue(3));
                    Address = reader.GetValue(4).ToString();
                    Password = reader.GetValue(5).ToString(); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private void UpdateDetails(object obj)
        {
            try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                //OracleDataReader reader = null;
                cmd = new OracleCommand($"Update Customer set Customer_ Name='{Customer_Name.ToString()}', Cust_Phone='{Cust_Phone}',Address='{Address.ToString()}',Password='{Password.ToString()}' where Customer_ID='{Id}'");
                int n= cmd.ExecuteNonQuery();
                if(n>0)
                {
                    DisplayData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }



    }
}
