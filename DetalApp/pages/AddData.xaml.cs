using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для AddData.xaml
    /// </summary>
    public partial class AddData : Page
    {
        public AddData()
        {
            InitializeComponent();
            Loaded += AddData_Loaded;
        }

        model.detalDbEntities db = new model.detalDbEntities();
        model.TipDetali tipdetal = new model.TipDetali();
        model.Master master = new model.Master();
        model.Detal detal = new model.Detal();
        model.Rabota rabota = new model.Rabota();
        model.Brak brak = new model.Brak();
    
        //загрузка моделей форм и данных в комбобоксы
        private void AddData_Loaded(object sender, RoutedEventArgs e)
        {
            tip.DataContext = tipdetal;
            masterForm.DataContext = master;
            detalForm.DataContext = detal;
            rabotaForm.DataContext = rabota;
            brakForm.DataContext = brak;

            var listTip = db.TipDetali.ToList();
            cmbTip.ItemsSource = listTip;

            var listMaster = db.Master.ToList();
            cmbMaster.ItemsSource = listMaster;

            var listDetal = db.Detal.ToList();
            cmbDetal.ItemsSource = listDetal;


            //left join
            var listRabota = (from r in db.Rabota
                             join b in db.Brak on r.ID_rabota equals b.ID_rabota into gj
                             from sub in gj.DefaultIfEmpty()
                             join d in db.Detal on r.ID_Detal equals d.ID_Detal
                             join m in db.Master on r.ID_Master equals m.ID_Master
                             where sub.ID_rabota == null
                             select new {
                                ID_rabota = r.ID_rabota,
                                NameDetal = d.NameDetal,
                                F_Master = m.F_Master,
                                I_Master = m.I_Master,
                                O_Master = m.O_Master
                             }).ToList();
            cmbRabota.ItemsSource = listRabota;
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            controller.Location._main.getFrame().GoBack();
        }

        //добавление нового типа деталей
        private void addTip(object sender, RoutedEventArgs e)
        {
            try
            {
                db.TipDetali.Add(tipdetal);
                db.SaveChanges();
                MessageBox.Show("Зaпись успешно добавлена");
                tipdetal = new model.TipDetali();
                tip.DataContext = tipdetal;
            }
            catch {
                MessageBox.Show("Проверьте правильность вводимых данных");
            }
           
        }


        //добавление мастера
        private void addMaster(object sender, RoutedEventArgs e)
        {
            /**Type myType = master.GetType();
            foreach (FieldInfo field in myType.GetFields(BindingFlags.NonPublic |  BindingFlags.Instance)) {          
                Console.WriteLine("Поле: {0}  Значение: {1}  Тип: {2}", field.Name, field.GetValue(master), field.FieldType);

            }**/
            db.Master.Add(master);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            master = new model.Master();
            masterForm.DataContext = master;
        }

        //добавление детали
        private void addDetal(object sender, RoutedEventArgs e)
        {
            db.Detal.Add(detal);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            detal = new model.Detal();
            detalForm.DataContext = detal;
        }

        //добавление работы
        private void addRabota(object sender, RoutedEventArgs e)
        {
            db.Rabota.Add(rabota);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            rabota = new model.Rabota();
            rabotaForm.DataContext = rabota;
        }

        //добавить брак
        private void addBrak(object sender, RoutedEventArgs e)
        {
            db.Brak.Add(brak);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            brak = new model.Brak();
            brakForm.DataContext = brak;
        }

        //обновление данных при выборе вкладки
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddData_Loaded(sender, e);
        }
    }
}
