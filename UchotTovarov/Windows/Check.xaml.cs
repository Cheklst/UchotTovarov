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
        List<Good> Tovars;
        public Check()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> goods = entities.Goods.Select(i => i.Name).ToList();
            cbNameGoods.ItemsSource = goods;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            entities.SaveChanges();
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            dgCheck.Visibility = Visibility.Hidden;
            btnAddGoods.Visibility = Visibility.Hidden;
            btnMinusGoods.Visibility = Visibility.Hidden;
            lPrice1.Visibility = Visibility.Hidden;
            lPrice2.Visibility = Visibility.Hidden;

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

            string nameGood = cbNameGoods.SelectedItem.ToString();
            string amount = tbAmount.Text;

            List<Goods> goods = new List<Goods>();
            goods = entities.Goods.ToList();

            Goods one = entities.Goods.Where(i => i.Name == nameGood).FirstOrDefault();
            one.Amount = one.Amount - Convert.ToInt32(amount);


            Good good = new Good
            {
                Name = nameGood,
                Amount = amount,
                Price = Convert.ToDecimal(one.Price),
            };

            Tovars.Add(good);
        }

       
        class Good
        {
            public string Name { get; set; }

            public string Amount { get; set; }

            public decimal Price { get; set; }
        }
    }
}
