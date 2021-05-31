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
    public partial class Check : Window
    {
        Entities entities = new Entities();
        List<Good> Tovars = new List<Good>();
        public Check()
        {
            InitializeComponent();
        }

        void PriceSum()
        {
            decimal a = 0;
            for (int i = 0; i < Tovars.Count; i++)
            {
                a += Tovars[i].Price * Tovars[i].Amount;
            }
            lPrice2.Content = a;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> goods = entities.Goods.Select(i => i.Name).ToList();
            cbNameGoods.ItemsSource = goods;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace workSpace = new WorkSpace();
            workSpace.Show();
            this.Close();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Tovars.Count; i++)
            {
                string idName = Tovars[i].Name;
                Goods goods = entities.Goods.Where(j => j.Name == idName).FirstOrDefault();
                int quantity = Convert.ToInt32(goods.Amount - Tovars[i].Amount);
                goods.Amount = quantity;
                entities.SaveChanges();
            }
            WorkSpace workSpace = new WorkSpace();
            workSpace.Show();
            this.Close();
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            dgCheck.Visibility = Visibility.Hidden;
            btnAddGoods.Visibility = Visibility.Hidden;
            btnMinusGoods.Visibility = Visibility.Hidden;
            lPrice1.Visibility = Visibility.Hidden;
            lPrice2.Visibility = Visibility.Hidden;
            btnEnter.Visibility = Visibility.Hidden;

            cbNameGoods.Visibility = Visibility.Visible;
            lName.Visibility = Visibility.Visible;
            lAmount.Visibility = Visibility.Visible;
            tbAmount.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            cbNameGoods.Visibility = Visibility.Hidden;
            lName.Visibility = Visibility.Hidden;
            lAmount.Visibility = Visibility.Hidden;
            tbAmount.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Hidden;

            dgCheck.Visibility = Visibility.Visible;
            btnAddGoods.Visibility = Visibility.Visible;
            btnMinusGoods.Visibility = Visibility.Visible;
            lPrice1.Visibility = Visibility.Visible;
            lPrice2.Visibility = Visibility.Visible;
            btnEnter.Visibility = Visibility.Visible;

            if (cbNameGoods.SelectedIndex != -1 || tbAmount.Text != "")
            {
                string nameGood = cbNameGoods.SelectedItem.ToString();
                try
                {
                    int amount = Convert.ToInt32(tbAmount.Text);

                    Goods one = entities.Goods.Where(i => i.Name == nameGood).FirstOrDefault();

                    if (amount <= one.Amount && one.Amount != 0)
                    {
                        Good good = new Good
                        {
                            Name = nameGood,
                            Amount = amount,
                            Price = Convert.ToDecimal(one.Price),
                        };

                        Tovars.Add(good);
                        PriceSum();
                        dgCheck.Items.Add(good);
                    }
                    else
                    {
                        MessageBox.Show("Столько товара нет на складе!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некорректные данные");
                }
            }
            else
            {
                MessageBox.Show("Введены некорректные данные");
            }
        }

        private void btnMinusGoods_Click(object sender, RoutedEventArgs e)
        {
            Good good = (Good)dgCheck.SelectedItem;
            if (good != null)
            {
                Tovars.Remove(good);
                dgCheck.Items.Remove(good);
                PriceSum();
            }
        }


        class Good
        {
            public string Name { get; set; }

            public int Amount { get; set; }

            public decimal Price { get; set; }
        }
    }
}
