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
    /// Interaction logic for Select_point.xaml
    /// </summary>
    public partial class Select_point : ThemedWindow
    {
        static string path = Environment.CurrentDirectory + @"\db1.mdf";

        SqlConnection sqlConnection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + path + "';Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        public Select_point()
        {
            InitializeComponent();
            DataSet dataset = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [Sotrudniki]", sqlConnection);
            sqlDataAdapter.Fill(dataset, "Sotrudniki");
            Cars.ItemsSource = dataset.Tables["Sotrudniki"].DefaultView;
            sqlConnection.Close();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection.Open();
            int ea = 0;
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [Sotrudniki]", sqlConnection);
            ea = (int)sqlCommand.ExecuteScalar();

            sqlConnection.Close();
            DataTable dt1 = ((DataView)Cars.ItemsSource).ToTable();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt1);
            int b = 0;
            string Id = "", Login = "", Pass = "", FIO = "",Role = "";
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

                                Login = cell.ToString();
                            }
                            if (b == 2)
                            {

                                Pass = cell.ToString();
                            }
                            if (b == 3)
                            {

                                FIO = cell.ToString();
                            }
                            if(b == 4)
                            {
                                Role = cell.ToString();
                            }
                            b++;
                        }
                        b = 0;
                        if (int.Parse(Id) > ea)
                        {
                            try
                            {
                                sqlConnection.Open();
                                sqlCommand = new SqlCommand("INSERT INTO [Sotrudniki]([Id],[Login],[Pass],[FIO],[Role]) VALUES('" + Id + "','" + Login + "','" + Pass.ToString() + "','" + FIO.ToString() + "','" + Role.ToString() + "')", sqlConnection);
                                sqlCommand.ExecuteNonQuery();
                                MessageBox.Show("Успешно");
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
                            sqlCommand = new SqlCommand("UPDATE [Sotrudniki] SET [Login] = '" + Login + "', [Pass] = '" + Pass.ToString() + "', [FIO] = '" + FIO.ToString() + "',[Role] = '"+Role.ToString()+ "' WHERE [Id] = '" + Id + "'", sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                            MessageBox.Show("Успешно");
                            sqlConnection.Close();

                        }



                    }

                }

            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PersonalButton_Click(object sender, RoutedEventArgs e)
        {
            DXWindow1 dXWindow1 = new DXWindow1();
            dXWindow1.Show();
            Close();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Close();
        }

        private void PersonalButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }
    }
}
