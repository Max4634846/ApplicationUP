using ApplicationUP.Repositories;
using ApplicationUP.ViewModels;
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
using System.Windows.Shapes;

namespace ApplicationUP.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            MainFrame.Content = new LoginPage();
        }

        public bool IsDarkTheme { get; set; }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            //if (MainFrame.CanGoBack)
            //{
            //    SinqUp.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    LoginBorder.Visibility = Visibility.Hidden;
            //}
        }
        private void Maximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
                var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwo.jpg", UriKind.Relative)));
                this.Background = imageBrush;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwoDark.jpg", UriKind.Relative)));
            this.Background = imageBrush;
        }
    }
}
