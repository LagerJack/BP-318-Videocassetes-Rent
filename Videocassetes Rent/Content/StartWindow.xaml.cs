using System.Windows.Controls;
using System;
using System.Windows.Input;
using System.Windows;

namespace Videocassetes_Rent.Content
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : UserControl
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 2;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void FastToCreate(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 3;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void RentEnt(object sender, MouseButtonEventArgs e)
        {
            
            WindSwitch.WindPage = 4;
            (this.Parent as DockPanel).Children.Remove(this);
        }

        private void UserList(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 5;
            (this.Parent as DockPanel).Children.Remove(this);
        }
        private void AddUserL(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 6;
            (this.Parent as DockPanel).Children.Remove(this);
        }
        private void RendList(object sender, MouseButtonEventArgs e)
        {
            WindSwitch.WindPage = 7;
            (this.Parent as DockPanel).Children.Remove(this);
        }
    }
}
