using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BindingListview : ContentPage
    {
        public ObservableCollection<TodoItem> Persons { get; set; } = new ObservableCollection<TodoItem>();

        public ICommand SelectedItemCommand { get; set; }
        public BindingListview()
        {
            InitializeComponent();

            SelectedItemCommand = new Command<TodoItem>(SelectedItemCommandImplementation);

            CreateSampleData();
            this.BindingContext = this;
            

        }

        private void SelectedItemCommandImplementation(TodoItem person)
        {
            Console.WriteLine(person.Notes);
        }
        private void CreateSampleData()
        {

            Persons.Add(new TodoItem() { Notes = "text poznamky" });
            // for (int i = 0; i < 10; i++) persons.Add(new Person() { Lastname = "Jméno" + i, Firstname = "Přijmení" + i, DateOfBirth = new DateTime(1980+i,1,1) });
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            var switchItem = (Switch)sender;
            TodoItem SelectedParent = (TodoItem)switchItem.BindingContext;
        }
    }
}