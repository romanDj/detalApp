using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Threading;

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

        DispatcherTimer timer = new DispatcherTimer();

        model.detalDbEntities db = new model.detalDbEntities();
        model.TipDetali tipdetal = new model.TipDetali();
        model.Master master = new model.Master();
        model.Detal detal = new model.Detal();
        model.Rabota rabota = new model.Rabota();
        model.Brak brak = new model.Brak();

        //загрузка моделей форм и данных в комбобоксы
        private void AddData_Loaded(object sender, RoutedEventArgs e)
        {
            //обновление данных в гридах
            if (controller.Location.userGlobal.role == "a")
            {
                timer.Interval = new TimeSpan(0, 0, 0, 1);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else {
                see_data.Visibility = Visibility.Hidden;

            }
            

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

        private void Timer_Tick(object sender, EventArgs e)
        {
            loadDataOnGrid(@"SELECT TOP 1000 [ID_Brak]
                                              ,[ID_rabota]
                                              ,[Prichina]
                                          FROM [detalDb].[dbo].[Brak]", brak_list);
            loadDataOnGrid(@"SELECT TOP 1000 [ID_Detal]
                                              ,[NameDetal]
                                              ,[HarakteristiciDetali]
                                              ,[VremyaNaIzgotovlenie]
                                              ,[ID_TD]
                                              ,[Price]
                                          FROM [detalDb].[dbo].[Detal]", detal_list);
            loadDataOnGrid(@"SELECT TOP 1000 [ID_Master]
                                              ,[F_Master]
                                              ,[I_Master]
                                              ,[O_Master]
                                              ,[DataPriemaNaRabotu]
                                              ,[BDate]
                                          FROM [detalDb].[dbo].[Master]", master_list);
            loadDataOnGrid(@"SELECT TOP 1000 [ID_rabota]
                                              ,[DataNachalo]
                                              ,[DataKonca]
                                              ,[VremyaNachalo]
                                              ,[VremyaKonca]
                                              ,[ID_Detal]
                                              ,[ID_Master]
                                          FROM [detalDb].[dbo].[Rabota]", rabota_list);
            loadDataOnGrid(@"SELECT TOP 1000 [ID_TD]
                                              ,[NameTD]
                                          FROM [detalDb].[dbo].[TipDetali]", tip_list);


        }

        public void loadDataOnGrid(string command, DataGrid dg){
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.myconn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader read = cmd.ExecuteReader();

                dt.Clear();
                dt.Load(read);
                dg.ItemsSource = dt.DefaultView;
            }
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            controller.Location._main.getFrame().GoBack();
            controller.Location.tackingAction();
        }

        //добавление нового типа деталей
        private void addTip(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.Location.tackingAction();
                db.TipDetali.Add(tipdetal);
                db.SaveChanges();
                controller.Location.protocolAction("Добавление типа деталей");
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
            controller.Location.tackingAction();
            db.Master.Add(master);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            controller.Location.protocolAction("Добавление мастера");
            master = new model.Master();
            masterForm.DataContext = master;
        }

        //добавление детали
        private void addDetal(object sender, RoutedEventArgs e)
        {
            controller.Location.tackingAction();
            db.Detal.Add(detal);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            controller.Location.protocolAction("Добавление детали");
            detal = new model.Detal();
            detalForm.DataContext = detal;
        }

        //добавление работы
        private void addRabota(object sender, RoutedEventArgs e)
        {
            controller.Location.tackingAction();
            db.Rabota.Add(rabota);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            controller.Location.protocolAction("Добавление работы");
            rabota = new model.Rabota();
            rabotaForm.DataContext = rabota;
        }

        //добавить брак
        private void addBrak(object sender, RoutedEventArgs e)
        {
            controller.Location.tackingAction();
            db.Brak.Add(brak);
            db.SaveChanges();
            MessageBox.Show("Зaпись успешно добавлена");
            controller.Location.protocolAction("Добавление детали в брак");
            brak = new model.Brak();
            brakForm.DataContext = brak;
        }

        //обновление данных при выборе вкладки
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controller.Location.tackingAction();
            AddData_Loaded(sender, e);
        }
    }
}
