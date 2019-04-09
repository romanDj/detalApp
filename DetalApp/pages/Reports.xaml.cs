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
            controller.Location.tackingAction();
            controller.Location._main.getFrame().GoBack();
        }

        //получить отчет
        private void GetReport(object sender, RoutedEventArgs e)
        {
            controller.Location.tackingAction();
            controller.Location.protocolAction("Получение отчетов");

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
                    case "1": cmd = new SqlCommand("SELECT ID_Master as '#', F_Master as 'Фамилия', I_Master as 'Имя', O_Master as 'Отчество', FORMAT(DataPriemaNaRabotu, 'dd.MM.yyyy') as 'Дата приема на рарботу', FORMAT(BDate, 'dd.MM.yyyy') as 'Дата рождения' FROM Master WHERE DATEDIFF(year, BDate, GETDATE()) > 20", conn);  break;
                    case "2": cmd = new SqlCommand("SELECT ID_Master AS [#], F_Master AS Фамилия, I_Master AS Имя, O_Master AS Отчетсво FROM Master AS m WHERE (F_Master IN (SELECT m.F_Master FROM            Master AS m INNER JOIN Rabota AS r ON m.ID_Master = r.ID_Master INNER JOIN Detal AS d ON r.ID_Detal = d.ID_Detal AND DATEDIFF(MINUTE, CAST(r.DataNachalo AS datetime) + CAST(r.VremyaNachalo AS datetime), CAST(r.DataKonca AS datetime) + CAST(r.VremyaKonca AS datetime)) > DATEDIFF(minute, '', d.VremyaNaIzgotovlenie)))", conn);  break;
                    case "3": cmd = new SqlCommand("SELECT TOP (3) t.NameTD AS 'Тип детали', COUNT(t.NameTD) AS [Количество попаданий в брак]  FROM Rabota AS r INNER JOIN Brak AS b ON r.ID_rabota = b.ID_rabota INNER JOIN Detal AS d ON r.ID_Detal = d.ID_Detal INNER JOIN TipDetali AS t ON t.ID_TD = d.ID_TD GROUP BY t.NameTD ORDER BY[Количество попаданий в брак] DESC", conn);  break;
                    case "4": cmd = new SqlCommand("SELECT SUM(Detal.Price) AS [Сумма потерянных денег (руб.)] FROM Brak INNER JOIN Rabota ON Brak.ID_rabota = Rabota.ID_rabota INNER JOIN Detal ON Rabota.ID_Detal = Detal.ID_Detal", conn);  break;
                    case "5": cmd = new SqlCommand("SELECT FORMAT(r.DataNachalo, 'dd.MM.yyyy') AS [Дата начала работы], d.ID_Detal AS [# детали], d.NameDetal AS [Название детали], r.ID_rabota AS [# работы] FROM            Rabota AS r INNER JOIN Detal AS d ON r.ID_Detal = d.ID_Detal LEFT OUTER JOIN Brak AS b ON r.ID_rabota = b.ID_rabota WHERE(b.ID_rabota IS NULL) AND(r.DataNachalo BETWEEN '2019-01-04' and '2019-02-02')", conn);  break;
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
            controller.Location.tackingAction();
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Application.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets["Лист1"];

            int x = 1;
            int y = 1;

            foreach (DataRow r in dt.Rows) {

                if (x == 1)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        ws.Cells[x, y] = c.ColumnName;
                        y++;
                    }
                    y = 1;
                    x++;
                }
               
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
