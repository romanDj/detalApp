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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }

        model.detalDbEntities db = new model.detalDbEntities();
      
        private void authorization(object sender, RoutedEventArgs e)
        {
            try{
                var us = db.user.FirstOrDefault(x => x.login == frmLogin.Text && x.password == frmPassword.Password);

                if (us != null) {
                    controller.Location.render("Menu");
                }
                else {
                    MessageBox.Show("Проверьте вводимые данные!");
                }
            }
            catch{
                MessageBox.Show("Что то пошло не так, повторите позже");
            }
            
        }
    }
}
