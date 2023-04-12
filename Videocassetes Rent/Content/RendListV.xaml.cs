using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для RendListV.xaml
    /// </summary>
    public partial class RendListV : UserControl
    {
        public static readonly string Root = Environment.CurrentDirectory;
        public static string query = "Select id_Договора AS [ID], Arendators.Фамилия || ' ' || Arendators.Имя || ' ' || Arendators.Отчество AS [ФИО арендатора], VideoCList.Название_Видеокассеты AS [Название видеокассеты], Дата_Выдачи AS [Дата выдачи], Дата_Возврата AS [Дата возврата] from VideoCasseteRent INNER JOIN Arendators ON VideoCasseteRent.ФИО_Арендатора = Arendators.id_Арендатора INNER JOIN VideoCList ON VideoCasseteRent.Название_Арендов_Видеокассеты = VideoCList.id_Видеокассеты";
        public static SQLiteConnection connect = new SQLiteConnection("data source=" + Root + @"\VideocasseteDBCore.db");
        public static DataTable dt = new DataTable();
        public static DataSet dst = new DataSet();
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static SQLiteDataAdapter da = new SQLiteDataAdapter();
        public static string[] temprazdels = { "Arendators.Фамилия || ' ' || Arendators.Имя || ' ' || Arendators.Отчество", "VideoCList.Название_Видеокассеты" };

        public RendListV()
        {
            InitializeComponent();

            UpdateInfo();

            string[] razdels = { "ФИО арендатора", "Название видеокассеты"};
            for (int j = 0; j <= razdels.Length - 1; j++)
            {
                SearchTypes.Items.Add(razdels[j]);
            }

        }

        private void BackToMenu(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 1;
            (this.Parent as DockPanel).Children.Remove(this);
            connect.Close();
        }

        private void UpdateInfo()
        {
            dt.Clear();

            connect.Open();

            cmd = new SQLiteCommand(query, connect);
            da = new SQLiteDataAdapter(cmd);

            da.Fill(dt);

            VideoListGrid.ItemsSource = dt.DefaultView;
            connect.Close();
        }

        private void SearchFunc(object sender, TextChangedEventArgs e)
        {
            dt.Clear();

            string searchquery = query + " where " + temprazdels[SearchTypes.SelectedIndex] + " like '%" + SearchBox.Text + "%'";

            cmd = new SQLiteCommand(searchquery, connect);
            da = new SQLiteDataAdapter(cmd);

            connect.Open();

            da.Fill(dt);

            VideoListGrid.ItemsSource = dt.DefaultView;

            if (VideoListGrid.Items.Count == 0)
            {
                NotFoundText.Visibility = Visibility.Visible;
            }
            else
            {
                NotFoundText.Visibility = Visibility.Hidden;
            }

            connect.Close();
        }

        private void HideText(object sender, MouseButtonEventArgs e)
        {
            HintText.Visibility = Visibility.Hidden;
        }

        private void UpdateDB(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 7;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void DeleteContentDB(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int selint = VideoListGrid.SelectedIndex;
                var idvid = dt.Rows[selint][0];
                connect.Open();
                string delquery = "Delete from VideoCasseteRent where id_Договора = '" + idvid + "'";
                cmd = new SQLiteCommand(delquery, connect);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "update VideoCList set Текущее_Состояние = '1' where id_Видеокассеты = '" + idvid + "'";
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                connect.Close();
                UpdateInfo();
            }
            catch (System.IndexOutOfRangeException)
            {
                NotFoundText.Visibility = Visibility.Visible;
                NotFoundText.Content = "ОШИБКА: Была произведена неудачная попытка удалить ячейку";
            }

        }

        private void CheckToHide(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
        }

        private void ClearText(object sender, SelectionChangedEventArgs e)
        {
            SearchBox.Text = "";
        }
    }
}
