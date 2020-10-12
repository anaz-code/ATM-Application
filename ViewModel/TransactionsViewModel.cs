using ATMApp.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace ATMApp.ViewModel
{
    public class TransactionsViewModel:ViewModelPropertyChanged

    {
        OracleConnection con = null;
        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        OracleCommand cmd = null;
        OracleDataAdapter adapter;
        DataSet ds;
        public int Id { get; set; }
        public Nullable<double> DebitT_Amount { get; set; }
        public Nullable<double> CreditT_Amount { get; set; }
        public ICollectionView collectionView { get; set; }
        public ICollectionView collection { get; set; }

        public TransactionsViewModel(int id)
        {
            Id = id;
           
            displayTransactions();
            collectionView = CollectionViewSource.GetDefaultView(transactions);
            ministatement();
            collection = CollectionViewSource.GetDefaultView(mini);
            
            
        }
        private ObservableCollection<Transactions> _transactions;

        public ObservableCollection<Transactions> transactions
        {
            get { return _transactions; }
            set { _transactions = value;
                OnPropertyChange("transactions");
            }
        }
        private ObservableCollection<Transactions> _mini;

        public ObservableCollection<Transactions> mini
        {
            get { return _mini; }
            set
            {
                _mini = value;
                OnPropertyChange("mini");
            }
        }
        private int _T_Id;

        public int T_Id
        {
            get { return _T_Id; }
            set { _T_Id = value;
                OnPropertyChange("T_Id");
            }
        }
        private int _Customer_Id;

        public int Customer_Id
        {
            get { return _Customer_Id; }
            set { _Customer_Id = value;
                OnPropertyChange("Customer_ID");
            }
        }
        private int _Acc_No;

        public int Acc_No
        {
            get { return _Acc_No; }
            set { _Acc_No = value;
                OnPropertyChange("Acc_No");
            }
        }
        private string _T_Type;

        public string T_Type
        {
            get { return _T_Type; }
            set { _T_Type = value;
                OnPropertyChange("T_Type");
            }
        }
        private int _T_Amount;

        public int T_Amount
        {
            get { return _T_Amount; }
            set { _T_Amount = value;
                OnPropertyChange("T_Amount");
            }
        }
        private DateTime _T_DateTime;

        public DateTime T_DateTime
        {
            get { return _T_DateTime; }
            set { _T_DateTime = value;
                OnPropertyChange("T_DateTime");
            }
        }
        public void displayTransactions()
        {
            try
            {
                con = new OracleConnection(connectionString);
                
                    con.Open();
                    using (var cmd=new OracleCommand($"Select T_Id,Customer_ID,T_Type,T_Amount,T_DateTime from Transactions where Customer_Id='{Id}'",con))
                    using (adapter = new OracleDataAdapter(cmd))
                    {
                      
                        ds = new DataSet();
                        adapter.Fill(ds, "Transactions");

                        if (transactions == null)
                            transactions = new ObservableCollection<Transactions>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            transactions.Add(new Transactions
                            {
                                T_Id=Convert.ToInt32(dr[0]),
                                Customer_Id=Convert.ToInt32(dr[1]),
                                T_Type=dr[2].ToString(),
                                T_Amount=Convert.ToInt32(dr[3]),
                                T_DateTime=Convert.ToDateTime(dr[4])
                            });
                        }
                    }
               

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
               
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void ministatement()
        {
            try
            {
                con = new OracleConnection(connectionString);
                
                    con.Open();
                    using (var cmd = new OracleCommand($"Select T_Type,T_Amount,T_DateTime from Transactions where Customer_Id='{Id}' and Rownum <= 5", con))
                    using (adapter = new OracleDataAdapter(cmd))
                    {

                        ds = new DataSet();
                        adapter.Fill(ds, "Transactions");
                    if (mini == null)
                        mini = new ObservableCollection<Transactions>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            if (dr[0].ToString() == "Debit")
                            {
                                mini.Add(new Transactions
                                {
                                    T_DateTime = Convert.ToDateTime(dr[2]),
                                    DebitT_Amount = Convert.ToDouble(dr[1]),
                                    CreditT_Amount = null
                                }
                                );
                            }
                            else if (dr[0].ToString() == "Credit")
                            {
                                mini.Add(new Transactions
                                {
                                    CreditT_Amount = Convert.ToDouble(dr[1]),
                                    T_DateTime = Convert.ToDateTime(dr[2]),
                                    DebitT_Amount = null
                                });
                            }
                        }
                    }
                

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

    }

   
}
