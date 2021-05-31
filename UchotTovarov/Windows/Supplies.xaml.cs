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
    /// <summary>
    /// Логика взаимодействия для Supplies.xaml
    /// </summary>
    public partial class Supplies : Window
    {
        Entities entities = new Entities();
        public Supplies()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> goods = entities.Goods.Select(i => i.Name).ToList();
            List<string> types = entities.Type.Select(i => i.Type1).ToList();
            cbName.ItemsSource = goods;
            cbType.ItemsSource = types;
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace workSpace = new WorkSpace();
            workSpace.Show();
            this.Close();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (cbName.SelectedIndex != -1 || tbAmount.Text != "")
            {
                string name = cbName.Text;
                try
                {
                    int amount = Convert.ToInt32(tbAmount.Text);
                    Goods one = entities.Goods.Where(i => i.Name == name).FirstOrDefault();

                    if (amount > 0)
                    {
                        int quantity = Convert.ToInt32(one.Amount + amount);
                        one.Amount = quantity;
                        entities.SaveChanges();
                        MessageBox.Show("Успешно оформлено!");
                        cbName.SelectedIndex = -1;
                        tbAmount.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Введены некорректные данные!");
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

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            lName.Visibility = Visibility.Hidden;
            cbName.Visibility = Visibility.Hidden;
            lAmount.Visibility = Visibility.Hidden;
            tbAmount.Visibility = Visibility.Hidden;
            btnEnter.Visibility = Visibility.Hidden;
            btnAddNew.Visibility = Visibility.Hidden;

            lName2.Visibility = Visibility.Visible;
            lAmount2.Visibility = Visibility.Visible;
            lPrice.Visibility = Visibility.Visible;
            lType.Visibility = Visibility.Visible;

            tbName2.Visibility = Visibility.Visible;
            tbAmount2.Visibility = Visibility.Visible;
            tbPrice.Visibility = Visibility.Visible;
            cbType.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Visible;

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbName2.Text != "" && tbAmount2.Text != "" && tbPrice.Text != "" && cbType.SelectedIndex != -1)
            {
                try
                {
                    string AddName = tbName2.Text;
                    int AddAmount = Convert.ToInt32(tbAmount2.Text);
                    int AddPrice = Convert.ToInt32(tbPrice.Text);
                    int AddType = Convert.ToInt32(cbType.SelectedIndex) + 1;

                    Goods one = entities.Goods.Where(i => i.Name == AddName).FirstOrDefault();

                    if (one == null)
                    {
                        Goods goods = new Goods
                        {
                            Name = AddName,
                            Amount = AddAmount,
                            Price = AddPrice,
                            IdType = AddType
                        };
                        entities.Goods.Add(goods);
                        entities.SaveChanges();

                        MessageBox.Show("Товар успешно добавлен!");

                        tbName2.Text = "";
                        tbAmount2.Text = "";
                        tbPrice.Text = "";
                        cbType.SelectedIndex = -1;

                        lName.Visibility = Visibility.Visible;
                        cbName.Visibility = Visibility.Visible;
                        lAmount.Visibility = Visibility.Visible;
                        tbAmount.Visibility = Visibility.Visible;
                        btnEnter.Visibility = Visibility.Visible;
                        btnAddNew.Visibility = Visibility.Visible;

                        lName2.Visibility = Visibility.Hidden;
                        lAmount2.Visibility = Visibility.Hidden;
                        lPrice.Visibility = Visibility.Hidden;
                        lType.Visibility = Visibility.Hidden;

                        tbName2.Visibility = Visibility.Hidden;
                        tbAmount2.Visibility = Visibility.Hidden;
                        tbPrice.Visibility = Visibility.Hidden;
                        cbType.Visibility = Visibility.Hidden;
                        BtnAdd.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Такой товар уже есть!");
                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверные введенные параметры!");
                }
                
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
