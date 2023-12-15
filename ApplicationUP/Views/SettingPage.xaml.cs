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

namespace ApplicationUP.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }
        private void SwitchToggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BtnToggle.Toggled1 == true)
            {
                var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwo.jpg", UriKind.Relative)));
                this.Background = imageBrush;
            }
            else
            {
                var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwoDark.jpg", UriKind.Relative)));
                this.Background = imageBrush;
            }
        }
    }
}
