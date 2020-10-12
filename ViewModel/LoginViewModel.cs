
using System;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows.Input;
using ATMApp.Command;
using System.Windows;
using ATMApp.View;
//using System.Data.OracleClient;

namespace ATMApp.ViewModel
{
    public class LoginViewModel:ViewModelPropertyChanged
    {
        
        OracleConnection con = null;
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        OracleCommand cmd = null;
        public LoginViewModel()
        {
            
        }
        
        private int _Customer_ID;

        public int Customer_ID
        {
            get { return _Customer_ID; }
            set { _Customer_ID = value;
                OnPropertyChange("Customer_ID");

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


        public ICommand CmdLogin
        {
            get
            {
                return new DelegateCommand<object>(Login);
            }
        }
       
        private void Login(object obj)
        {
            try
            {
                con = new OracleConnection(connectionString);
                con.Open();
                OracleDataReader reader;
                cmd = new OracleCommand($"Select *from Customer where Customer_ID='{Convert.ToInt32(Customer_ID)}' and Password='{Password.ToString()}'", con);
                reader=cmd.ExecuteReader();
                if (reader.Read())
                {
                    

                    //MessageBox.Show(Convert.ToInt32(reader.GetValue(0)) + " , " + reader.GetString(1));
                    Application.Current.Properties["Cust_Name"] = reader.GetValue(2);

                    MessageBox.Show("Login Successful");
                    //It should be xaml not view model
                    HomeScreen hs = new HomeScreen();
                    HomeScreenViewModel hsvm = new HomeScreenViewModel(reader.GetValue(2).ToString(),Customer_ID);
                    hs.DataContext = hsvm;
                    hs.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Id or Password");
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
