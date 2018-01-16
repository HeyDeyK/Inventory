using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var dbConnection = App.Database;

            TodoItemDatabase todoItemDatabase = App.Database;
            TodoItem item2 = new TodoItem();
            TodoItem item = new TodoItem();
            /*item2.Name = "Tramvaj";
            item2.Text = "Jela domů";
            App.Database.SaveItemAsync(item2);*/


            var itemsFromDb = App.Database.GetItemsAsync().Result;

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");

            Debug.WriteLine(itemsFromDb.Count);
            foreach (TodoItem todoItem in itemsFromDb)
            {
                Debug.WriteLine(todoItem);
            }

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");


            ItemsCount.Text = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;
        }
    }
}
