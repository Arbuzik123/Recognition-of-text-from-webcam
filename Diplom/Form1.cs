using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Drawing.Imaging;
namespace Diplom
{
    
    public partial class Form1 : Form
    {
        static string path = Environment.CurrentDirectory + @"\db1.mdf";
        
        SqlConnection sqlConnection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='"+path+"';Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        bool comand = false;
        public Form1()
        {
            InitializeComponent();
            PE.CommonSettings();
            sqlConnection.Open();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            /*HDevEngine dev = new HDevEngine();
            dev.SetProcedurePath($@"C:\Users\Dmitry\Desktop\MyDiplom");
            HObject Image, Characters;

            HDevProcedure proc = new HDevProcedure("DADWA");
            HDevProcedureCall call = new HDevProcedureCall(proc);
            call.Execute();
            
            Image = call.GetOutputIconicParamImage("Image");*/
            
            


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckDB(textBox1.Text);
            if(comand == true)
            {
                label3.Text = "Разрешен";
                label3.BackColor = Color.Green;
                string Idvod = "",FIO = "",Nomer = "";
                sqlCommand = new SqlCommand("SELECT [Idvod] FROM Cars WHERE Nomer = '" + textBox1.Text+"'");
                SqlDataReader rd = sqlCommand.ExecuteReader();
                while (rd.Read()){
                     Idvod = rd["Idvod"].ToString();
                }
                sqlCommand = new SqlCommand("SELECT [FIO],[Nomer] FROM Voditeli WHERE Id = '" + Idvod + "'");
                while (rd.Read())
                {
                    FIO = rd["FIO"].ToString();
                    Nomer = rd["Nomer"].ToString();
                }
                sqlCommand = new SqlCommand("UPDATE [Cars] SET [last Arrival date] = '" + DateTime.Now + "', [last Date of depature] = '" + DateTime.Now + "', WHERE [Id] = '"+Idvod+"'");
                label5.Text = FIO.ToString();
                label7.Text = Nomer.ToString();
            }
            else
            {
                
            }
        }
        public bool CheckDB(string login) 
        {
            
            sqlCommand = new SqlCommand("SELECT [Nomer] FROM [Cars] ", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();


            while (sqlDataReader.Read())
            {
                string Nomer = sqlDataReader["Nomer"].ToString();

                if (login == Nomer)
                {
                    
                    sqlDataReader.Close();
                    comand = true;
                    return comand;

                }
                else
                {
                   
                }
            }
            return comand;

        }

        private void bMakeSingleShot_Click(object sender, EventArgs e)
        {
            PE.Image = cameraControl1.TakeSnapshot();
            PE.Image.Save(@"C:\Users\Dmitry\source\repos\Diplom\Diplom\bin\Debug\1.png", ImageFormat.Png);
            HDevEngine dev = new HDevEngine();
            dev.SetProcedurePath($@"C:\Users\Dmitry\Desktop\MyDiplom");
            HDevProcedure proc = new HDevProcedure("Diplomall");
            HDevProcedureCall proccall = new HDevProcedureCall(proc);
            string path = @"C:\Users\Dmitry\source\repos\Diplom\Diplom\bin\Debug\1.png";
            proccall.SetInputCtrlParamTuple("ImagePath", new HTuple(path));
            proccall.Execute();
            HTuple symbols = proccall.GetOutputCtrlParamTuple("Recognized");
            List<string> symbolsList = symbols.SArr.ToList();
            string result = "";
            symbolsList.ForEach(t => result += t);
            textBox1.Text = result;





        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    public static class PictureEditManager
    {
        public static void CommonSettings(this PictureEdit PicEdit)
        {
            PicEdit.Margin = new Padding(0);

            PicEdit.Properties.AllowScrollOnMouseWheel = DefaultBoolean.True;
            PicEdit.Properties.AllowScrollViaMouseDrag = true;
            PicEdit.Properties.AllowZoomOnMouseWheel = DefaultBoolean.True;
            PicEdit.Properties.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            PicEdit.Properties.PictureStoreMode = PictureStoreMode.Image;
            PicEdit.Properties.ShowCameraMenuItem = CameraMenuItemVisibility.Always;
            PicEdit.Properties.ShowZoomSubMenu = DefaultBoolean.True;
            PicEdit.Properties.ZoomingOperationMode = ZoomingOperationMode.MouseWheel;
            PicEdit.Properties.ShowEditMenuItem = DefaultBoolean.True;
            PicEdit.Properties.ShowScrollBars = true;
            PicEdit.Properties.SizeMode = PictureSizeMode.Clip;
        }
    }
}
