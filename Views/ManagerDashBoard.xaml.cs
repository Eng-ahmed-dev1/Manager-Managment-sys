using ManagerManagmentsys.Model;
using Microsoft.EntityFrameworkCore;
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
using ManagerManagmentsys;
namespace ManagerManagmentsys.Views
{
    /// <summary>
    /// Interaction logic for ManagerDashBoard.xaml
    /// </summary>
    public partial class ManagerDashBoard : Window
    {
        public ManagerDashBoard()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser()
        {
            try
            {
                using var db = new ManagmentDB();
                var list = db.Tasks
                             .Include(t => t.User)
                             .OrderBy(u => u.Userid)
                             .ToList();
                TasksGrid.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new ManagmentDB();
                if (!int.TryParse(txtTaskid.Text, out int taskid))
                {
                    MessageBox.Show("Please Enter Task ia as a number !!");
                    txtTaskid.Text = "";
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Please Enter Valied Title !!");
                    txtTitle.Text = "";
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDesc.Text))
                {
                    MessageBox.Show("Please Enter Valied Descrption !!");
                    txtDesc.Text = "";
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmpName.Text))
                {
                    MessageBox.Show("Please Enter Valied Empolyee Name !!");
                    txtEmpName.Text = "";
                    return;
                }
                if (!int.TryParse(txtEmpId.Text, out int EmpId))
                {
                    MessageBox.Show("Please Enter Valied Empolyee ID !!");
                    txtEmpId.Text = "";
                    return;
                }
                var x = ((ComboBoxItem)combo.SelectedItem).Content.ToString();

                if (x == null)
                {
                    MessageBox.Show("Choose a Status Please !!");
                    return;
                }
                var existUser = db.User.FirstOrDefault(u => u.UserID == EmpId);

                if (existUser == null)
                {
                    existUser = new User
                    {
                        UserID = EmpId,
                        Name = txtEmpName.Text,
                        Email = "user@gmail.com",
                        Password = "123",
                        Role = "Employee"
                    };
                    db.User.Add(existUser);
                    db.SaveChanges();
                }

                var newTask = new Tasks
                {
                    TaskId = taskid,
                    Title = txtTitle.Text,
                    Description = txtDesc.Text,
                    Status = x,
                    DueDate = DateTime.Now,
                    User = existUser
                };

                db.Tasks.Add(newTask);
                db.SaveChanges();

                LoadUser();
                MessageBox.Show("Task added successfully!");

                txtTaskid.Text = "";
                txtTitle.Text = "";
                txtDesc.Text = "";
                txtEmpName.Text = "";
                txtEmpId.Text = "";
                combo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new ManagmentDB();
                if (!int.TryParse(txtTaskid.Text, out int Ct))
                {
                    MessageBox.Show("Please select a valid task!");
                    return;
                }

                var task = db.Tasks.Include(t => t.User).
                    FirstOrDefault(x => x.TaskId == Ct);

                if (task == null)
                {
                    MessageBox.Show("Task not found!");
                    return;
                }
                task.Title = txtTitle.Text;
                task.Description = txtDesc.Text;
                task.Status = ((ComboBoxItem)combo.SelectedItem).Content.ToString();

                if (task.User != null)
                {
                    task.User.Name = txtEmpName.Text;
                }
                db.SaveChanges();
                LoadUser();
                MessageBox.Show("Data Updated successgully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void TasksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TasksGrid.SelectedItem == null)
                return;

            var y = (Tasks)TasksGrid.SelectedItem;
            txtTaskid.Text = y.TaskId.ToString();
            txtTitle.Text = y.Title?.ToString();
            txtDesc.Text = y.Description?.ToString();
            txtEmpName.Text = y.User?.Name;
            txtEmpId.Text = y.User?.UserID.ToString();


            foreach (ComboBoxItem item in combo.Items)
            {
                if (item.Content.ToString() == y.Status)
                {
                    combo.SelectedItem = item;
                    break;
                }
            }
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtTaskid.Text, out int taskid))
                {
                    MessageBox.Show("please select a task to delete");
                    return;
                }

                using var db = new ManagmentDB();
                var taid = db.Tasks.Find(taskid);
                if (taid != null)
                {
                    db.Tasks.Remove(taid);
                    db.SaveChanges();
                    LoadUser();
                }
                MessageBox.Show("The Task deleted sucessfully !");

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