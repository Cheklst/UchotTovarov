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
    public partial class ChangeGoods : Window
    {
        Entities entities = new Entities();
        public ChangeGoods()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> types = entities.Type.Select(i => i.Type1).ToList();
            cbType.ItemsSource = types;
            MessageBox.Show("Выберите название товара");
        }

        private void btnPoisk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Goods> goods = new List<Goods>();
                goods = entities.Goods.ToList();
                Goods good = goods.First(i => i.Name == tbName.Text);

                AppData.idGoods = good.IdGoods;

                tbAmount.Text = good.Amount.ToString();
                tbPrice.Text = good.Price.ToString();
                cbType.SelectedIndex = Convert.ToInt32(good.IdType) - 1;

                lAmount.Visibility = Visibility.Visible;
                tbAmount.Visibility = Visibility.Visible;
                lPrice.Visibility = Visibility.Visible;
                tbPrice.Visibility = Visibility.Visible;
                lType.Visibility = Visibility.Visible;
                cbType.Visibility = Visibility.Visible;
                btnEnter.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;

            }
            catch (Exception)
            {
                MessageBox.Show("Такого товара нет!");
            }
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace workSpace = new WorkSpace();
            workSpace.Show();
            this.Close();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != "")
            {
                List<Goods> goods = new List<Goods>();
                goods = entities.Goods.ToList();
                Goods good = goods.FirstOrDefault(i => i.IdGoods == AppData.idGoods);
                try
                {
                    string name = tbName.Text;
                    int amount = Convert.ToInt32(tbAmount.Text);
                    decimal price = Convert.ToDecimal(tbPrice.Text);
                    int type = Convert.ToInt32(cbType.SelectedIndex) + 1;

                    if (amount > 0 && price > 0 && name != "")
                    {
                        good.Name = name;
                        good.Amount = amount;
                        good.Price = price;
                        good.IdType = type;

                        entities.SaveChanges();
                        MessageBox.Show("Успешно сохранено!");
                        
                        lAmount.Visibility = Visibility.Hidden;
                        tbAmount.Visibility = Visibility.Hidden;
                        lPrice.Visibility = Visibility.Hidden;
                        tbPrice.Visibility = Visibility.Hidden;
                        lType.Visibility = Visibility.Hidden;
                        cbType.Visibility = Visibility.Hidden;
                        btnEnter.Visibility = Visibility.Hidden;
                        btnDelete.Visibility = Visibility.Hidden;

                        tbName.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Колличество и цена должны быть больше нуля!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите корректные данные!");
                }
                
            }
            else
            {
                MessageBox.Show("У товара должно быть название");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Goods> goods = new List<Goods>();
                goods = entities.Goods.ToList();
                Goods good = goods.First(i => i.Name == tbName.Text);
                entities.Goods.Remove(goods.First(i => i.IdGoods == AppData.idGoods));

                entities.SaveChanges();
                MessageBox.Show("Товар успешно удален!");

                lAmount.Visibility = Visibility.Hidden;
                tbAmount.Visibility = Visibility.Hidden;
                lPrice.Visibility = Visibility.Hidden;
                tbPrice.Visibility = Visibility.Hidden;
                lType.Visibility = Visibility.Hidden;
                cbType.Visibility = Visibility.Hidden;
                btnEnter.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;

                tbName.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!");
            }
           
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.Visibility = Visibility.Hidden;
        }
    }
}
