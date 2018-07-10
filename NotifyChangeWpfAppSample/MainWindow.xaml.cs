using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotifyChangeWpfAppSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();

        private User a = new User();

        public MainWindow()
        {
            InitializeComponent();
            this.txtusername.Background = new SolidColorBrush(Colors.LightGray);

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Marry" });

            lbUsers.ItemsSource = users;
           
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });          

        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                users.Remove(lbUsers.SelectedItem as User);
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                a = (User)lbUsers.SelectedItem;
                if (lbUsers.SelectedItem != null)
                {
                    //users.Add(new User { UserName = a.Name});
                    a.UserName = a.Name;
                    this.DataContext = a;
                    //this.DataContext = users;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txtadduser_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtadduser != null && txtadduser.Text.Length > 0)
                    {
                        if (sender is TextBox)
                        users.Add(new User() { Name = txtadduser.Text.ToString() });
                        txtadduser.Text = null;
                    }
                    else
                    {
                        users.Add(new User() { Name = null });
                    }

                }              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                
            }
        }


        public class User : INotifyPropertyChanged
        {

            //public string Name { get; set; }
            
            private string name;
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                      this.NotifyPropertyChanged("Name");
                    }
                }
            }

            private string userName;
            public string UserName
            {
                get
                {
                    return this.userName;
                }
                set
                {
                    if (this.userName != value)
                    {
                        this.userName = value;
                    this.NotifyPropertyChanged("UserName");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}
