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

namespace DetalApp.pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        //страница с добавлением
        private void addAction(object sender, RoutedEventArgs e)
        {
            controller.Location.render("AddData");
        }

        //страница отчетов
        private void reportAction(object sender, RoutedEventArgs e)
        {
            controller.Location.render("Reports");
        }
    }
}
