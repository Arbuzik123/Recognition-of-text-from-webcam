using DevExpress.Xpf.Core;
using System.Windows;
using System.Data.SqlClient;
using System;

namespace Diplom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        static string path = Environment.CurrentDirectory + @"\db1.mdf";

        SqlConnection sqlConnection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + path + "';Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        bool comand = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {

            CheckDB(Login.Text, Password.Password);
            if(comand == true)
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SELECT [Id role] FROM [Sotrudniki] WHERE Login = '"+Login.Text+"'",sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    string Role = sqlDataReader["Id role"].ToString();
                    if(Role == "1")
                    {
                        Admin admin = new Admin();
                        admin.Show();
                        Close();
                    }
                    else
                    {
                        Form1 video = new Form1();
                        video.Show();
                        Hide();
                    }
                }
                sqlConnection.Close();
            }
            else
            {
                Login.Text = "";
                Password.Password = "";
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                sqlConnection.Close();
            }
            
        }
        public bool CheckDB(string login, string password) // проверка логина и пароля, не работает
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SELECT Login, Pass FROM [Sotrudniki] ", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            
            while (sqlDataReader.Read())
            {
                string userLogin = sqlDataReader["Login"].ToString();
                string passwordDB = sqlDataReader["Pass"].ToString();
                if (login == userLogin && password == passwordDB)
                {
                    sqlConnection.Close();
                    sqlDataReader.Close();
                    comand = true;
                    return comand;

                }
            }
            return comand;
            
        }
    }
}
