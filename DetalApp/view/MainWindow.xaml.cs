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
using System.Windows.Threading;

namespace DetalApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            controller.Location._main = this;
            controller.Location.render();
            Loaded += MainWindow_Loaded;
        }

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer_block = new DispatcherTimer();
       

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            out_btn.Visibility = Visibility.Hidden;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            ////////////////////////

            timer_block.Interval = new TimeSpan(0, 0, 0, 1);
            timer_block.Tick += Timer_Tick_Block;
            timer_block.Start();
        }

        public Frame getFrame() => main_frame;

        private void out_click(object sender, RoutedEventArgs e)
        {
            controller.Location.userGlobal = null;
            controller.Location.render("Auth");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (controller.Location.userGlobal != null){
                out_btn.Visibility = Visibility.Visible;
                out_btn.Content = "Выход на главную, "+controller.Location.userGlobal.login;
            }else {
                out_btn.Visibility = Visibility.Hidden;
            }
        }

        private void Timer_Tick_Block(object sender, EventArgs e)
        {
            if (controller.Location.userGlobal != null)
            {
                Console.WriteLine("Прошло секунд " + controller.Location.block_second);
                controller.Location.block_second++;
                if (controller.Location.block_second == 60)
                {
                    Console.WriteLine("Прошла минута");
                    out_click(null, null);
                    controller.Location.block_second = 0;
                }
            }
        }


    }
}
