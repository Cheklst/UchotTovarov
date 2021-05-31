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
        int isAddEdit = 0;
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
                btnSpecial.Content = "Рабочие";
                btnSpecial.Visibility = Visibility.Collapsed;//Доработать
                dg.IsReadOnly = false;
                dg.CanUserAddRows = true;
                dg.CanUserDeleteRows = true;
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
                check.Show();
                this.Close();
            }
            else if (u.idRole == 2)//Кладовщик
            {
                Supplies supplies = new Supplies();
                supplies.Show();
                this.Close();
            }
            else if (u.idRole == 3)//Админ
            {
                //List<Employee> employee = entities.Employee.ToList();
                //List<Goods> goods = entities.Goods.ToList();
                //dg.ItemsSource = null;
                //dg.ItemsSource = employee;
            }
        }

        private void SaveGoods(object sender, DataGridRowEditEndingEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg != null)
            {
                var dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                if (isAddEdit == 0)
                {
                    if (entities.SaveChanges() == 0)
                    {
                        MessageBox.Show("Ошибка", "Ошибка записи", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    var num_id = e.Row.Item as Goods;
                    entities.Goods.Add(new Goods
                    {
                        Name = num_id.Name,
                        Amount = num_id.Amount,
                        Price = num_id.Price,
                        IdType = num_id.IdType,
                    });

                    if (entities.SaveChanges() == 0)
                    {
                        MessageBox.Show("Ошибка", "Ошибка записи", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        isAddEdit = 0;
                        return;
                    }

                    isAddEdit = 0;

                }
            }
        }

        private void PreviewKey(object sender, KeyEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                DataGridRow dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                if (e.Key == Key.Delete && !dgr.IsEditing)
                {
                    var res = dg.SelectedItem as Goods;
                    var ree = MessageBox.Show("Подтверждение удаления",
                        string.Format("Запись \n{0}\n{1}\n будет удалена.", res.Name),
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!(e.Handled = (ree == MessageBoxResult.No)))
                    {
                        if (entities.Goods.Remove(entities.Goods.FirstOrDefault(i => i.IdGoods == res.IdGoods)) != null)
                        {
                            entities.SaveChanges();
                        }
                        else
                            MessageBox.Show("Ошибка", "Ошибка удаления!", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    }
                }
            }
        }
        private void IsAddEdit(object sender, AddingNewItemEventArgs e)
        {
            isAddEdit = 1;
        }
    }
}
