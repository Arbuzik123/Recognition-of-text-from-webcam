using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Diplom
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : ThemedWindow
    {
        static string path = Environment.CurrentDirectory + @"\db1.mdf";

        SqlConnection sqlConnection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + path + "';Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();

        public Admin()
        {
            InitializeComponent();
            
            DataSet dataset = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [Cars]", sqlConnection);
            sqlDataAdapter.Fill(dataset, "Cars");
            Cars.ItemsSource = dataset.Tables["Cars"].DefaultView;        
            sqlConnection.Close();
            

        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection.Open();
            int ea = 0;
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [Cars]", sqlConnection);
            ea = (int)sqlCommand.ExecuteScalar();
            
            sqlConnection.Close();
            DataTable dt1 = ((DataView)Cars.ItemsSource).ToTable();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt1);
            int b = 0;
            string Id = "", Nimer = "", arrival = "", Dep = "";
            foreach (DataTable dt in dataset.Tables)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        var cells = row.ItemArray;
                        foreach (object cell in cells)
                        {


                            if (b == 0)
                            {
                                
                                Id = cell.ToString();
                            }
                            if (b == 1)
                            {
                                
                                Nimer = cell.ToString();
                            }
                            if (b == 2)
                            {
                                
                                arrival = cell.ToString();
                            }
                            if (b == 3)
                            {
                                
                                Dep = cell.ToString();
                            }
                            b++;
                        }
                        b = 0;
                        if (int.Parse(Id) > ea)
                        {
                            try
                            {
                                sqlConnection.Open();
                                sqlCommand = new SqlCommand("INSERT INTO [Cars]([Id],[Nomer],[last Arrival date],[last Date of depature]) VALUES('" + Id + "','" + Nimer + "','" + arrival.ToString() + "','" + Dep.ToString() + "')", sqlConnection);
                                sqlCommand.ExecuteNonQuery();
                                sqlConnection.Close();
                            }
                            catch
                            {
                                sqlConnection.Close();
                            }
                            
                        }
                        else
                        {
                            sqlConnection.Open();
                            sqlCommand = new SqlCommand("UPDATE [Cars] SET [Nomer] = '" + Nimer + "', [last Arrival date] = '" + arrival.ToString() + "', [last Date of depature] = '" + Dep.ToString() + "' WHERE [Id] = '" + Id + "'", sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();

                        }



                    }

                }

            }
            
        }

        private void PersonalButton_Click(object sender, RoutedEventArgs e)
        {
            Select_point select_Point = new Select_point();
            select_Point.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PersonalButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            DXWindow1 dX = new DXWindow1();
            dX.Show();
            Close();
        }

        private void PersonalButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }
    }
}
