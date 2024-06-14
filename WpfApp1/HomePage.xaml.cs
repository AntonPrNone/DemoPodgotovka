using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users;
        Page1 page1 = new Page1();
        Page2 page2 = new Page2();
        public MainWindow()
        {
            InitializeComponent();

            DB_Management.OpenConnection();
            DB_Management.AddUser("Антон", 21);
            //DB_Management.UpdateUser(new User() { Id = 21, Age = 21, Name = "12345"});

            LoadData();
        }

        private void LoadData()
        {
            users = DB_Management.GetUsers();
            Society_ItemControl.ItemsSource = users.OrderBy(p => p.Age);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            User user = button.Tag as User;

            if (user != null)
            {
                if (int.TryParse(user.Age.ToString(), out _))
                {
                    DB_Management.UpdateUser(user);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Ошибка", "Данное значение не является числом");
                }
            }

            FramePage.Content = page1;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            User user = button.Tag as User;

            if (user != null)
            {
                DB_Management.DeleteUser(user.Id);
                LoadData();
            }

            FramePage.Content = page2;
        }

        private void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Society_ItemControl.ItemsSource = users.Where(p => p.Name.Contains(Search_TextBox.Text));
        }
    }
}
