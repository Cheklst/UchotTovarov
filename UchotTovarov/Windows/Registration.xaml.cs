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

namespace UchotTovarov
{
    public partial class Registration : Window
    {
        Entities entities = new Entities();
        public Registration()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbLogin.Text == " " || tbPassword.Text == " " || tbLogin.Text == "" || tbPassword.Text == "")
                {
                    MessageBox.Show("Вы не ввели данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Employee u = entities.Employee.Where(i => i.Login == tbLogin.Text).FirstOrDefault();
                    if (u != null)
                    {
                        if (u.Password == tbPassword.Text)
                        {
                            AppData.idEmployee = entities.Employee.Where(i => i.Login == tbLogin.Text).Select(j => j.idEmployee).FirstOrDefault();
                            Windows.WorkSpace workSpace = new Windows.WorkSpace();
                            this.Close();
                            workSpace.Show();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так: "+ ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
