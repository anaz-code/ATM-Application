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
    public class ChangePinViewModel:ViewModelPropertyChanged
    {
        OracleConnection con = null;
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        OracleCommand cmd = null;
        public int Id { get; set; }
        public ChangePinViewModel(int id)
        {
            Id = id;
            
        }
        private int _OldPin;

        public int OldPin
        {
            get { return _OldPin; }
            set { _OldPin = value;
                OnPropertyChange("OldPin");
            }
        }
        private int _NewPin;

        public int NewPin
        {
            get { return _NewPin; }
            set { _NewPin = value;
                OnPropertyChange("NewPin");
           }
        }
        private int _ConfirmNewPin;

        public int ConfirmNewPin
        {
            get { return _ConfirmNewPin; }
            set { _ConfirmNewPin = value; }
        }

        public ICommand CmdChangePin
        {
            get
            {
                return new DelegateCommand<Object>(UpdatePin);
            }
        }

        private void UpdatePin(object obj)
        {
            try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader = null;
                cmd = new OracleCommand($"Select PIN from Account_Details where PIN='{Convert.ToInt32(OldPin)}'",con);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    if (NewPin == ConfirmNewPin)
                    {
                        cmd = new OracleCommand($"Update Account_Details set PIN='{Convert.ToInt32(ConfirmNewPin)}'", con);
                        int n = cmd.ExecuteNonQuery();
                        if (n > 0)
                        {
                            MessageBox.Show("PIN Updated");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Confirmation PIN does not match the New PIN");
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
        }
    }
}
