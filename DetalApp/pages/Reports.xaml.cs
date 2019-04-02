using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
            Loaded += Reports_Loaded; 
        }

        DataTable dt;

        private void Reports_Loaded(object sender, RoutedEventArgs e)
        {
            GetReport(nav.Children.OfType<Button>().Where(x=> Convert.ToInt32(x.Tag) == 1  ).First(), e);
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            controller.Location._main.getFrame().GoBack();
        }

        //получить отчет
        private void GetReport(object sender, RoutedEventArgs e)
        {

            //визуально определить действующий отчет
            Button btn = (sender as Button) as Button;

            foreach (var obj in nav.Children.OfType<Button>()) {
                var bc = new BrushConverter();
                obj.Background = (Brush)bc.ConvertFrom("#FF1D83C9");
            }

            btn.Background = (Brush)new BrushConverter().ConvertFrom("#FF11A225");

            //////////////////////////////////

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.myconn)) {
                conn.Open();

                dt = new DataTable();

                SqlCommand cmd = null;

                switch (btn.Tag.ToString()) {
                    case "1": cmd = new SqlCommand("SELECT ID_Master as id, F_Master as 'Фамилия', I_Master as 'Имя', O_Master as 'Отчество', FORMAT(DataPriemaNaRabotu, 'yyyy.MM.dd') as 'Дата приема на рарботу', FORMAT(BDate, 'yyyy.MM.dd') as 'Дата рождения' FROM Master WHERE DATEDIFF(year, BDate, GETDATE()) > 20", conn);  break;
                    case "2": cmd = new SqlCommand("SELECT * FROM Master m WHERE m.F_Master IN(SELECT m.F_Master  FROM Master m join Rabota r on m.ID_Master = r.ID_Master join Detal d on r.ID_Detal = d.ID_Detal WHERE  DATEDIFF(MINUTE, CAST(r.DataNachalo as datetime) + CAST(r.VremyaNachalo as datetime), CAST(r.DataKonca as datetime) + CAST(r.VremyaKonca as datetime)) > DATEDIFF(minute, '', d.VremyaNaIzgotovlenie)) ", conn);  break;
                    case "3": cmd = new SqlCommand("SELECT TOP(3) COUNT(t.NameTD) as col, t.NameTD as 'Тип детали'   FROM Rabota r join Brak b on r.ID_rabota = b.ID_rabota join Detal d on r.ID_Detal= d.ID_Detal join TipDetali t on t.ID_TD = d.ID_TD GROUP BY t.NameTD Order BY col DESC", conn);  break;
                    case "4": cmd = new SqlCommand("", conn);  break;
                    case "5": cmd = new SqlCommand("SELECT * FROM Rabota r join Detal d on r.ID_Detal = d.ID_Detal left join Brak b on r.ID_rabota = b.ID_rabota WHERE  b.ID_rabota is null AND r.DataNachalo between '2019-01-04' and '2019-02-02'", conn);  break;
                }


                SqlDataReader read = cmd.ExecuteReader();
               
                dt.Clear();
                dt.Load(read);
                dgReport.ItemsSource = dt.DefaultView;

               
            }

        }

        //экспорт в excel
        private void exportExcel(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Application.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets["Лист1"];

            int x = 1;
            int y = 1;

            foreach (DataRow r in dt.Rows) {
               
                foreach (DataColumn c in dt.Columns) {
                    ws.Cells[x, y] = r[c].ToString();
                    y++;
                }

                y = 1;
                x++;

            }

            wb.Close();
            excelApp.Quit();
        }

        private void deleteThis(object sender, RoutedEventArgs e)
        {
            Button btn = sender  as Button;
            MessageBox.Show("Покажи id: " + btn.Tag.ToString());
        }
    }
}
