using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для RentAdd.xaml
    /// </summary>
    public partial class RentAdd : UserControl
    {
        public static readonly string Root = Environment.CurrentDirectory;
        public static string query1 = "Select id_Арендатора, Фамилия || ' ' || Имя || ' ' || Отчество AS ФИО from Arendators";
        public static string query2 = "Select id_Видеокассеты, Название_Видеокассеты, Текущее_Состояние from VideoCList where Текущее_Состояние != '3'";
        public static SQLiteConnection connect = new SQLiteConnection("data source=" + Root + @"\VideocasseteDBCore.db");
        public static DataTable dtusers = new DataTable();
        public static DataTable dcassetes = new DataTable();
        public static DataTable dt3 = new DataTable();
        public static DataSet dst = new DataSet();
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static SQLiteDataAdapter da = new SQLiteDataAdapter();
        public RentAdd()
        {
                InitializeComponent();

                connect.Open();

            cmd = new SQLiteCommand(query1, connect);
            da = new SQLiteDataAdapter(cmd);
            da.Fill(dtusers);


            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                FIOBox.Items.Add((string)reader["ФИО"]);         

            cmd = new SQLiteCommand(query2, connect);
            da = new SQLiteDataAdapter(cmd);
            da.Fill(dcassetes);
            DbDataReader reader2 = cmd.ExecuteReader();
            while (reader2.Read())
                VideoBox.Items.Add((string)reader2["Название_Видеокассеты"]);         
            connect.Close();
            }

            private void BackToList(object sender, MouseButtonEventArgs e)
            {
                WindSwitch.WindPage = 4;
                (this.Parent as DockPanel).Children.Remove(this);
            }

            private void AddtoDB(object sender, MouseButtonEventArgs e)
            {

            var IDUser = dtusers.Rows[FIOBox.SelectedIndex][0];
            var IDCassete = dcassetes.Rows[VideoBox.SelectedIndex][0];
            Panel.Content = IDCassete;

            DateTime timete = (DateTime)StartT.SelectedDate;
            DateTime endete = (DateTime)EndT.SelectedDate;

            connect.Open();
            da.AcceptChangesDuringFill = false;
            cmd.CommandText = "INSERT INTO VideoCasseteRent(ФИО_Арендатора, Название_Арендов_Видеокассеты, Дата_Выдачи, Дата_Возврата) VALUES('" + IDUser + "','" + IDCassete + "','" + timete.ToString("dd-MM-yyyy") + "','" + endete.ToString("dd-MM-yyyy") + "')";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update VideoCList set Текущее_Состояние = '3' where id_Видеокассеты = '" + IDCassete + "'";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            connect.Close();
            WindSwitch.WindPage = 4;
            (this.Parent as DockPanel).Children.Remove(this);

            }

        }
    }