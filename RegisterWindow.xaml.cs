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
using userDataBase_ADO;

namespace UserVerify_WPF
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnection db = new DatabaseConnection();
            if(LoginRegWin.Text != "" && PasswordRegWin.Text!="")
            { 
                //MessageBox.Show("Login is not empty");
                db.InsertRow(LoginRegWin.Text, PasswordRegWin.Text);
                MessageBox.Show("Пользователь успешно создан");
                this.Close();
            } else
            {
                MessageBox.Show("Login or Password is empty!!!");
            }
        }
    }
}
