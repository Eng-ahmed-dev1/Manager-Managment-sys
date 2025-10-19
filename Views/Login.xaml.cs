using ManagerManagmentsys.Model;
using ManagerManagmentsys.Views;
using System.Linq.Expressions;
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

namespace ManagerManagmentsys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Loginbtn(object sender, RoutedEventArgs e)
        {
      
            try
            {
                using var db = new ManagmentDB();


                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    ErrorMessageLab.Content = "Enter Valied UserName ";
                    txtUsername.Text = "";
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    ErrorMessageLab.Content = "Enter Valied Password ";
                    txtPassword.Password = "";
                    return;
                }

                var user = db.User.FirstOrDefault(x => x.Name == txtUsername.Text);
                if (user == null)
                {
                    ErrorMessageLab.Content = "User Not Found !!";
                    txtUsername.Text = "";
                    txtPassword.Password = "" ;
                    return;
                }
                if (user?.Password != txtPassword.Password)
                {
                    ErrorMessageLab.Content = "Incorrect Password !!";
                    txtPassword.Password = "";
                    return;
                }
                if(user.Role == "Manager")
                {
                    new ManagerDashBoard().Show();
                    this.Close();
                }
                else if (user.Role == "Employee")
                {
                    new UserDashBaord(user.UserID).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Access Denied !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                return;
            }
        }
    }
}