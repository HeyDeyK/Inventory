using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SQLite;

namespace EvidenceSkladeb
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TodoItem> itemsFromDb;
        public MainWindow()
        {
            InitializeComponent();
            LoadTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TodoItem item = new TodoItem();
            item.Name = txtName.Text;
            item.Who = txtWho.Text;
            int n;
            if(Int32.TryParse(txtLenght.Text, out n))
            {
                item.Lenght = txtLenght.Text;
                TimeSpan time = TimeSpan.FromSeconds(Int32.Parse(item.Lenght));
                string str = time.ToString(@"mm\:ss");
                item.Lenght = str;
                App.Database.SaveItemAsync(item);
                itemsFromDb.Add(item);
                SeznamListView.ItemsSource = itemsFromDb;
                txtLenght.Text = "";
                txtName.Text = "";
                txtWho.Text = "";
            }
            else
            {
                txtLenght.Text = "Toto není správný formát";
            }
            


        }
        public void LoadTable()
        {
            itemsFromDb = new ObservableCollection<TodoItem>(App.Database.GetItemsNotDoneAsync().Result);
            SeznamListView.ItemsSource = itemsFromDb;
        }

        private void DeleteBtn(object sender, RoutedEventArgs e)
        {
            
            App.Database.DeleteItemAsync(itemsFromDb[SeznamListView.SelectedIndex]);
            itemsFromDb.Remove(itemsFromDb[SeznamListView.SelectedIndex]);
            SeznamListView.ItemsSource = itemsFromDb;

        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {
            itemsFromDb[SeznamListView.SelectedIndex].Name = changeName.Text;
            itemsFromDb[SeznamListView.SelectedIndex].Who = changeWho.Text;
            itemsFromDb[SeznamListView.SelectedIndex].Lenght = changeLenght.Text;
            App.Database.SaveItemAsync(itemsFromDb[SeznamListView.SelectedIndex]);

        }

        private void SeznamListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(SeznamListView.SelectedIndex);
            if(SeznamListView.SelectedIndex==-1)
            {
                changeName.Text = "";
                changeWho.Text = "";
                changeLenght.Text = "";
            }
            else
            {
                changeName.Text = itemsFromDb[SeznamListView.SelectedIndex].Name;
                changeWho.Text = itemsFromDb[SeznamListView.SelectedIndex].Who;
                changeLenght.Text = itemsFromDb[SeznamListView.SelectedIndex].Lenght;
            }
            
        }
    }
}
