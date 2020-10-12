using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ATMApp.Command;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows;
using ATMApp.View;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ATMApp.ViewModel
{
    public class HomeScreenViewModel: ViewModelPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HomeScreenViewModel(string name,int id)
        {
            Id = id;
            Name = name;
        }
        
        OracleConnection con = null;
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        OracleCommand cmd,sql = null;
        private string _Pin;

        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value;
                OnPropertyChange("Pin");
            }
        }
        private int _Amount;

        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; 
            OnPropertyChange("Amount");
            }
        }

        public ICommand CmdCheckBalance
        {
            get
            {
                return new DelegateCommand<object>(CheckBalance);
            }
        }
        public ICommand CmdWithdraw
        {
            get
            {
                return new DelegateCommand<object>(Withdraw);
            }
        }
        public ICommand CmdDeposit
        {
            get
            {
                return new DelegateCommand<object>(Deposit);
            }
        }
        public ICommand CmdDetails
        {
            get
            {
                return new DelegateCommand<object>(ChangeDetail);
            }
        }
        public ICommand CmdCheckPin
        {
            get
            {
                return new DelegateCommand<object>(ValidatePin);
            }
        }
        public ICommand CmdClear
        {
            get
            {
                return new DelegateCommand<object>(Clear);
            }
        }
        public ICommand CmdChangePin
        {
            get
            {
                return new DelegateCommand<object>(ChangePin);
            }
        }
        public ICommand CmdTransactions
        {
            get
            {
                return new DelegateCommand<object>(Transactions);
            }
        }

        

        private void Transactions(object obj)
        {
            Transactions tr = new Transactions();
            TransactionsViewModel trvm = new TransactionsViewModel(Id);
            tr.DataContext = trvm;
            tr.Show();
        }

        private void ChangePin(object obj)
        {
            ChangePin cp = new ChangePin();
            ChangePinViewModel cpvm = new ChangePinViewModel(Id);
            cp.DataContext = cpvm;
            cp.Show();
        }

        private void Clear(object obj)
        {
            Pin = null;
        }

        

        private void ValidatePin(object obj)
        {
            Pin += obj.ToString();
        }


        private void ChangeDetail(object obj)
        {
            ChangeDetails cd = new ChangeDetails();
            ChangeDetailsViewModel cdvm = new ChangeDetailsViewModel(Id);
            cd.DataContext = cdvm;
            cd.Show();
        }

        private void CheckBalance(object obj)
        {
             try
             { 
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader;
                cmd = new OracleCommand($"Select Balance from Account_Details where Customer_ID='{Id}' and PIN='{Convert.ToInt32(Pin)}'", con);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    MessageBox.Show("Current Balance:"+reader.GetValue(0));
                }
                else
                {
                    MessageBox.Show("Incorrect PIN");
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
        private void Withdraw(object obj)
        {

            try
            {
                using (var con= new OracleConnection(connectionString))
                {
                    con.Open();
                    using (var cmd = new OracleCommand($"Select Acc_No,Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Incorrect PIN");
                            return;
                        }

                        if (Amount <= 0 || Convert.ToInt32(reader.GetValue(1)) <= Amount)
                        {
                            MessageBox.Show("Insufficient balance");
                            return;
                        }
                        sql = new OracleCommand($"Update Account_Details SET Balance=Balance -'{Amount}' where Customer_ID='{Id}' and PIN='{Pin}'", con);
                        int n = sql.ExecuteNonQuery();
                        if (n > 0)
                        {
                            
                            sql = new OracleCommand($"Select Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                            OracleDataReader read = sql.ExecuteReader();
                            MessageBox.Show("Remaining Balance:" + read.GetValue(0));
                           
                            Amount = 0;

                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
                }
            finally
            {
               // con.Close();
                con.Dispose();
            }




           /** try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader;
                cmd = new OracleCommand($"Select Acc_No,Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    if((Amount>0) && (Convert.ToInt32(reader.GetValue(1))>Amount))
                    { 
                        cmd = new OracleCommand($"Update Account_Details SET Balance=Balance -'{Amount}' where Customer_ID='{Id}' and PIN='{Pin}'", con);
                        int n=cmd.ExecuteNonQuery();
                        if (n > 0)
                        {
                            cmd = new OracleCommand($"Select Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                            reader = cmd.ExecuteReader();
                            MessageBox.Show("Remaining Balance:" + reader.GetValue(0));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Insufficient balance");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect PIN");
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
            }**/

        }
        private void Deposit(object obj)
        {
            try
            {
                using (var con = new OracleConnection(connectionString))
                {
                    con.Open();
                    using (var cmd = new OracleCommand($"Select Acc_No,Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Incorrect PIN");
                            return;
                        }

                        if (Amount <= 0 || Convert.ToInt32(reader.GetValue(1)) <= Amount)
                        {
                            MessageBox.Show("Insufficient balance");
                            return;
                        }
                        sql = new OracleCommand($"Update Account_Details SET Balance=Balance +'{Amount}' where Customer_ID='{Id}' and PIN='{Pin}'", con);
                        int n = sql.ExecuteNonQuery();
                        if (n > 0)
                        {
                            sql = new OracleCommand($"Select Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                            OracleDataReader read = sql.ExecuteReader();
                            MessageBox.Show("Remaining Balance:" + read.GetValue(0));
                            
                        }
                    }
                }

            }
           /** try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader;  
                if (Amount > 0 && Pin!=null) 
                { 
                    cmd = new OracleCommand($"Update Account_Details SET Balance=Balance +'{Amount}' where Customer_ID='{Id}' and PIN='{Pin}'", con);
                    int n=cmd.ExecuteNonQuery();
                    if(n>0)
                    {
                       cmd = new OracleCommand($"Select Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                       reader = cmd.ExecuteReader();
                       MessageBox.Show("Current Balance:" +reader.GetValue(0));
                        

                    }
                    else
                    {
                      MessageBox.Show("Incorrect PIN");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid amount");
                }
            }**/
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













/**
try
{
    using (var con = new OracleConnection(connectionString))
    {
        con.Open();
        using (var cmd = new OracleCommand($"Select Acc_No,Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con))
        using (var reader = cmd.ExecuteReader())
        {
            if (!reader.Read())
            {
                MessageBox.Show("Incorrect PIN");
                return;
            }

            if (Amount <= 0 || Convert.ToInt32(reader.GetValue(1)) <= Amount)
            {
                MessageBox.Show("Insufficient balance");
                return;
            }

            cmd = new OracleCommand($"Update Account_Details SET Balance=Balance -'{Amount}' where Customer_ID='{Id}' and PIN='{Pin}'", con);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                cmd = new OracleCommand($"Select Balance from Account_Details where PIN='{Convert.ToInt32(Pin)}'", con);
                reader = cmd.ExecuteReader();
                MessageBox.Show("Remaining Balance:" + reader.GetValue(0));
            }
        }
    }
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message.ToString());

}**/