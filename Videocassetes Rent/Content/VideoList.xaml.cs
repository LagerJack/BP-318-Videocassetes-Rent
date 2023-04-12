using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.Data;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для VideoList.xaml
    /// </summary>
    public partial class VideoList : UserControl
    {
        public static readonly string Root = Environment.CurrentDirectory;
        public static string query = "Select id_Видеокассеты AS [ID], Название_Видеокассеты AS [Название видеокассеты], Год, Жанр, Длительность, CurrentLocation.Состояние AS Состояние from VideoCList INNER JOIN CurrentLocation ON VideoCList.Текущее_Состояние = CurrentLocation.id_Состояния";
        public static SQLiteConnection connect = new SQLiteConnection("data source=" + Root + @"\VideocasseteDBCore.db");
        public static DataTable dt = new DataTable();
        public static DataSet dst = new DataSet();
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static SQLiteDataAdapter da = new SQLiteDataAdapter();
        public static string[] temprazdels = { "Название_Видеокассеты", "Год", "Жанр", "Состояние"};

        public VideoList()
        {
            InitializeComponent();

            UpdateInfo();

            string[] razdels = {"Название видеокассеты","Год","Жанр", "Состояние"};
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
            WindSwitch.WindPage = 3;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void DeleteContentDB(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int selint = VideoListGrid.SelectedIndex;
                var idvid = dt.Rows[selint][0];
                connect.Open();
                string delquery = "Delete from VideoCList where id_Видеокассеты = '" + idvid + "'";
                cmd = new SQLiteCommand(delquery, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
                UpdateInfo();
            }
            catch(System.IndexOutOfRangeException)
            {
                NotFoundText.Visibility = Visibility.Visible;
                NotFoundText.Content = "ОШИБКА: Была произведена попытка удалить текст";
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
