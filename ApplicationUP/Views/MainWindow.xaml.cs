using ApplicationUP.Repositories;
using ApplicationUP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
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

namespace ApplicationUP.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new HomePage();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddPage();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SettingPage();
        }
    }
}

