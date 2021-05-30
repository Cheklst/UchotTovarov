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
using System.Windows.Shapes;

namespace UchotTovarov.Windows
{
    public partial class WorkSpace : Window
    {
        Entities entities = new Entities();
        public WorkSpace()
        {
            InitializeComponent();
        }

        public void LoadDG(List<Goods> goods)
        {
            dg.Items.Clear();
            for (int i = 0; i < goods.Count(); i++)
            {
                dg.Items.Add(goods[i]);
            }
            type.ItemsSource = entities.Type.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Employee u = entities.Employee.Where(i => i.idEmployee == AppData.idEmployee).FirstOrDefault();
            Computers computers = entities.Computers.Where(i => i.idEmployee == AppData.idEmployee).FirstOrDefault();
            if (u.idRole == 1)
            {
                lArea.Content = computers.Place;
                lArea.Foreground = Brushes.Red;
                btnSpecial.Content = "Создать чек";
                btnSpecial.Background = Brushes.Red;
            }
            else if (u.idRole == 2)
            {
                lArea.Content = computers.Place;
                lArea.Foreground = Brushes.DarkGreen;
                btnSpecial.Content = "Добавить товары";
                btnSpecial.Background = Brushes.DarkGreen;
            }
            else if (u.idRole == 3)
            {
                lArea.Content = computers.Place;
                lArea.Foreground = Brushes.Aquamarine;
                btnSpecial.Content = "Просмотр транзакций";
            }
            List<Goods> goods = entities.Goods.ToList();
            List<string> types = entities.Type.Select(i => i.Type1).ToList();
            cbtype.ItemsSource = types;
            LoadDG(goods);
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnPoisk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Goods> goods = new List<Goods>();
                goods = entities.Goods.ToList();
                if (tbname.Text != "")
                {
                    goods = goods.FindAll(i => i.Name == tbname.Text);
                }
                if (tbamount.Text != "")
                {
                    goods = goods.FindAll(i => i.Amount >= Convert.ToInt32(tbamount.Text));
                }
                if (tbpricemin.Text != "")
                {
                    goods = goods.FindAll(i => i.Price >= Convert.ToInt32(tbpricemin.Text));
                }
                if (tbpricemax.Text != "")
                {
                    goods = goods.FindAll(i => i.Price <= Convert.ToInt32(tbpricemax.Text));
                }
                if (cbtype.SelectedIndex != -1)
                {
                    goods = goods.FindAll(i => i.IdType == (Convert.ToInt32(cbtype.SelectedIndex) + 1));
                }
                LoadDG(goods);            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            tbname.Text = "";
            tbamount.Text = "";
            tbpricemin.Text = "";
            tbpricemax.Text = "";
            cbtype.SelectedIndex = -1;
            List<Goods> goods = new List<Goods>();
            goods = entities.Goods.ToList();
            LoadDG(goods);
        }

        private void btnSpecial_Click(object sender, RoutedEventArgs e)
        {
            Employee u = entities.Employee.Where(i => i.idEmployee == AppData.idEmployee).FirstOrDefault();
            if (u.idRole == 1)//Кассир
            {
                Check check = new Check();
                check.ShowDialog();
            }
            else if (u.idRole == 2)//Кладовщик
            {

            }
            else if (u.idRole == 3)//Админ
            {

            }
        }
    }
}
