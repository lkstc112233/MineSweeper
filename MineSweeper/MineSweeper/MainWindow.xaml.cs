using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace MineSweeper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private void AlwaysCanExcute_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public Board board
        {
            get
            {
                return (Board)GetValue(boardProperty);
            }
            set
            {
                SetValue(boardProperty, value);
            }
        }
        public static DependencyProperty boardProperty = DependencyProperty.Register("board", typeof(Board), typeof(MainWindow));

        public void NewGame()
        {
            board = new Board(Properties.Settings.Default.Height, Properties.Settings.Default.Width, Properties.Settings.Default.Mines);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            NewGame();
        }

        private void ClickOnCell(object sender, RoutedEventArgs e)
        {
            board.Reveal(((sender as Button).Tag as Cell).x, ((sender as Button).Tag as Cell).y);
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).ReleaseMouseCapture();
            if ((sender as Button).IsMouseOver)
            {
                board.TryMark(((sender as Button).Tag as Cell).x, ((sender as Button).Tag as Cell).y);
            }
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).CaptureMouse();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }

    public class IsSameStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CellOutlookEnum))
                return Visibility.Collapsed;
            var toCheck = (CellOutlookEnum)value;
            if (parameter is CellOutlookEnum)
            {
                if ((CellOutlookEnum)parameter == toCheck)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
