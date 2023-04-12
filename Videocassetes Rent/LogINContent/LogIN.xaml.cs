using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Videocassetes_Rent.LogINContent
{
    /// <summary>
    /// Логика взаимодействия для LogIN.xaml
    /// </summary>
    public partial class LogIN : UserControl
    {
        //Login info
        public static string Login = "Administrator";
        public static string Password = "xxXX1234";

        public static int tryes = 3;

        public LogIN()
        {
            InitializeComponent();
        }

        private void CheckToLogin(object sender, MouseButtonEventArgs e)
        {
            if(LogBox.Text == Login && PassBox.Password == Password)
            {
                WindSwitch.WindPage = 1;
                (this.Parent as DockPanel).Children.Remove(this);
            }
            else
            {
              if(tryes != 1)
                {
                    Warning.Visibility = Visibility.Visible;
                    tryes--;
                }
              else
                {
                    WarBox.Visibility = Visibility.Visible;
                    WarBut.Visibility = Visibility.Visible;
                    WarEx.Visibility = Visibility.Visible;
                    WarTXT.Visibility = Visibility.Visible;
                    WarTXT2.Visibility = Visibility.Visible;
                }
            }
        }

        private void HideWarning(object sender, TextChangedEventArgs e)
        {
            Warning.Visibility = Visibility.Collapsed;
        }

        private void CheckToEx(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void HideWarning(object sender, RoutedEventArgs e)
        {

        }
    }
}
