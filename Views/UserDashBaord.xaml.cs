using ManagerManagmentsys.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagerManagmentsys.Views
{
    /// <summary>
    /// Interaction logic for UserDashBaord.xaml
    /// </summary>
    public partial class UserDashBaord : Window
    {
        private int _tUserId;

        public UserDashBaord(int UserId)
        {
            InitializeComponent();
            _tUserId = UserId;
            LoadUserdataPending();
            LoadUserdataCompelete();
        }
        private void LoadUserdataPending()
        {
            try
            {
                using var db = new ManagmentDB();
                var users = db.Tasks.Where(x => x.Userid == _tUserId)
                .Include(y => y.User!.tasks)
                .Select(x => new
                {
                    x.Status,
                    x.TaskId,
                    x.Description,
                    x.Title
                }).ToList().Where(z => z.Status == "Pending" || z.Status == "In Progress").ToList();
                PenPor.ItemsSource = users;
                var n = db.User.FirstOrDefault(x => x.UserID == _tUserId);
                if (n != null)
                {
                    labName.Content = n.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void LoadUserdataCompelete()
        {
            try
            {
                using var db = new ManagmentDB();
                var users = db.Tasks.Where(x => x.Userid == _tUserId)
                .Include(y => y.User!.tasks)
                .Select(x => new
                {
                    x.Status,
                    x.TaskId,
                    x.Description,
                    x.Title
                }).ToList().Where(z => z.Status == "Completed").ToList();
                Compelete.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new ManagmentDB();
                if (!int.TryParse(taskId.Text, out int taskis))
                {
                    MessageBox.Show("Enter the task as a number !!");
                    return;
                }

                var tasid = db.Tasks.Find(taskis);
                if (tasid == null)
                {
                    MessageBox.Show("The Task is not found !!");
                    return;
                }
                ;
                var x = ((ComboBoxItem)asd.SelectedItem).Content.ToString();
                tasid.Status = x;
                tasid.DueDate = DateTime.Now;

                db.SaveChanges(); 
                LoadUserdataPending();
                LoadUserdataCompelete();
                MessageBox.Show("Data Loaded Successfully !!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();    // login 
            this.Close();
        }
    }
}