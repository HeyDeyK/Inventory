using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ToDoList.Abstract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbstractDatabaseAccess : ContentPage
    {
        public AbstractDatabaseAccess()
        {
            InitializeComponent();

            InitializeComponent();
            
            var dbConnection = App.Database;

            DatabaseAccess todoItemDatabase = App.DatabaseAccess;

            TodoItemConcretization item = new TodoItemConcretization();


            /*item.Name = "item";
            item.Text = "item text";
            todoItemDatabase.SaveItemAsync(item);*/

            /*
            var itemsFromDb = todoItemDatabase.GetItemsAsync<TodoItemConcretization>().Result;

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");

            Debug.WriteLine(itemsFromDb.Count);
            foreach (TodoItemConcretization todoItem in itemsFromDb)
            {
                Debug.WriteLine(todoItem);
            }

            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");
            Debug.WriteLine("                             ");


            ItemsCount.Text = "Items in Database " + itemsFromDb.Count;
            ToDoItemsListView.ItemsSource = itemsFromDb;*/                                                          
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}