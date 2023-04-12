using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        public static readonly string Root = Environment.CurrentDirectory;
        public static string query = "Select Состояние from CurrentLocation";
        public static SQLiteConnection connect = new SQLiteConnection("data source=" + Root + @"\VideocasseteDBCore.db");
        public static DataTable dt = new DataTable();
        public static DataSet dst = new DataSet();
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static SQLiteDataAdapter da = new SQLiteDataAdapter();

        public AddUser()
        {
            InitializeComponent();

            //init db 

            dt.Clear();

            connect.Open();

            cmd = new SQLiteCommand(query, connect);
            da = new SQLiteDataAdapter(cmd);

            da.Fill(dt);

            connect.Close();

        }

        private void BackToList(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 5;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void AddtoDB(object sender, MouseButtonEventArgs e)
        {

            DateTime dateTime = DateTime.UtcNow.Date;
            connect.Open();
                da.AcceptChangesDuringFill = false;
                cmd.CommandText = "INSERT INTO Arendators(Фамилия, Имя, Отчество, Номер_Телефона, Место_Проживания, Дата_Регистрации) VALUES('" + Firstl.Text + "','" + Secondl.Text + "','" + Thirdl.Text + "','" + PhoneNum.Text + "','" + LivePlace.Text + "','" + dateTime.ToString("dd-MM-yyyy") + "')";
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            connect.Close();


            WindSwitch.WindPage = 5;
            (this.Parent as DockPanel).Children.Remove(this);
        }
    }
}
