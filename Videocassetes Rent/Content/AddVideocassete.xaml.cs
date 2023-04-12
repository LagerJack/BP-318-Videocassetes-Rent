using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для AddVideocassete.xaml
    /// </summary>
    public partial class AddVideocassete : UserControl
    {
        public static readonly string Root = Environment.CurrentDirectory;
        public static string query = "Select id_Состояния, Состояние from CurrentLocation";
        public static SQLiteConnection connect = new SQLiteConnection("data source=" + Root + @"\VideocasseteDBCore.db");
        public static DataTable dt = new DataTable();
        public static DataSet dst = new DataSet();
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static SQLiteDataAdapter da = new SQLiteDataAdapter();

        public AddVideocassete()
        {
            InitializeComponent(); 

            dt.Clear();

            connect.Open();

            cmd = new SQLiteCommand(query, connect);
            da = new SQLiteDataAdapter(cmd);

            da.Fill(dt);

            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                LocBox.Items.Add((string)reader["Состояние"]);        
            connect.Close();

            int year = 2077;
            int dur = 100;
            for (int j = 1980; j <= year; j++)
            {
                YearBox.Items.Add(j);
                
            }
            for (int j = 1; j <= dur; j++)
            {
                Duration.Items.Add(j);
            }
        }

        private void BackToList(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 2;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void AddtoDB(object sender, MouseButtonEventArgs e)
        {
            var LocInt = dt.Rows[LocBox.SelectedIndex][0];
            int repeati = Convert.ToInt32(Duration.Text);
            connect.Open();
            for (int i = 1; i <= repeati; i++)
            {
            da.AcceptChangesDuringFill = false;
            cmd.CommandText = "INSERT INTO VideoCList(Название_Видеокассеты, Год, Жанр, Длительность, Текущее_Состояние) VALUES('" + VideoCNameB.Text + "','" + YearBox.Text + "','" + TypeB.Text + "','" + TimeB.Text + "','" + LocInt + "')";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            }
            connect.Close();


            WindSwitch.WindPage = 2;
            (this.Parent as DockPanel).Children.Remove(this);
        }
    }
}
