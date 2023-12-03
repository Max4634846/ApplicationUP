using MaterialDesignThemes.Wpf;
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

namespace ApplicationUP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwo.jpg", UriKind.Relative)));
            this.Background = imageBrush;
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);

                var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwo.jpg", UriKind.Relative)));
                this.Background = imageBrush;

            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);

                var imageBrush = new ImageBrush(new BitmapImage(new Uri("D:\\Windows Forms Visual Studio\\ApplicationUP\\ApplicationUP\\image\\FoneTwoDark.jpg", UriKind.Relative)));
                this.Background = imageBrush;

            }

            paletteHelper.SetTheme(theme);
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

