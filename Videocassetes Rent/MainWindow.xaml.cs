using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System;

namespace Videocassetes_Rent
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            WindSwitch.WindPage = 8;
        }

        private void  timer_Tick(object sender, EventArgs e)
        {

            switch (WindSwitch.WindPage)
            {
                case 1:
                    Content.StartWindow obj = new Content.StartWindow();
                    DoskPanel.Children.Add(obj);
                    WindSwitch.WindPage = 0;
                    break;
                case 2:
                    Content.VideoList obj2 = new Content.VideoList();
                    DoskPanel.Children.Add(obj2);
                    WindSwitch.WindPage = 0;
                    break;
                case 3:
                    Content.AddVideocassete obj3 = new Content.AddVideocassete();
                    DoskPanel.Children.Add(obj3);
                    WindSwitch.WindPage = 0;
                    break;
                case 4:
                    Content.RendListV obj4 = new Content.RendListV();
                    DoskPanel.Children.Add(obj4);
                    WindSwitch.WindPage = 0;
                    break;
                case 5:
                    Content.UserList obj5 = new Content.UserList();
                    DoskPanel.Children.Add(obj5);
                    WindSwitch.WindPage = 0;
                    break;
                case 6:
                    Content.AddUser obj6 = new Content.AddUser();
                    DoskPanel.Children.Add(obj6);
                    WindSwitch.WindPage = 0;
                    break;
                case 7:
                    Content.RentAdd obj7 = new Content.RentAdd();
                    DoskPanel.Children.Add(obj7);
                    WindSwitch.WindPage = 0;
                    break;
                case 8:
                    LogINContent.LogIN obj8 = new LogINContent.LogIN();
                    DoskPanel.Children.Add(obj8);
                    WindSwitch.WindPage = 0;
                    break;
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }

        
        private void ExitApp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void GlowButton(object sender, MouseEventArgs e)
        {
            CloseButton.Fill = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
        }

        private void NoGlowButton(object sender, MouseEventArgs e)
        {
            HideButton.Fill = new SolidColorBrush(Color.FromArgb(200, 25, 25, 25));
            CloseButton.Fill = new SolidColorBrush(Color.FromArgb(200, 25, 25, 25));
        }

        private void GlowButtonYellow(object sender, MouseEventArgs e)
        {
            HideButton.Fill = new SolidColorBrush(Color.FromArgb(200, 255, 125, 0));
        }

        private void HideApp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
