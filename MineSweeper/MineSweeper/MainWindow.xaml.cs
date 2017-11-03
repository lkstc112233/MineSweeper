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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            board = new Board(9,9,10);
        }

        private void ClickOnCell(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("X: " + ((sender as Button).Tag as Cell).x + ", Y: " + ((sender as Button).Tag as Cell).y);
        }

        private void RevealCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Left Click:\nX: " + (e.Parameter as Cell).x + ", Y: " + (e.Parameter as Cell).y);
        }

        private void MarkCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Right Click:\nX: " + (e.Parameter as Cell).x + ", Y: " + (e.Parameter as Cell).y);
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).ReleaseMouseCapture();
            if ((sender as Button).IsMouseOver)
                MessageBox.Show("Right Click:\nX: " + ((sender as Button).Tag as Cell).x + ", Y: " + ((sender as Button).Tag as Cell).y);
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).CaptureMouse();
        }
    }
}
