using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Data.SqlClient;
using userDataBase_ADO;

namespace UserVerify_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();        
      
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        DatabaseConnection db = new DatabaseConnection();

        if(db.CheckData(LoginTextbox.Text, PassTextbox.Text))
        {
            MessageBox.Show("Login Success");
            ShowUser userWindow = new ShowUser(LoginTextbox.Text, PassTextbox.Text, db.UserId);
            userWindow.Show();
        } else
        {
            //MessageBox.Show("Login Failed");
            
            //при неудачной авторизации открываю окно регистрации
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}