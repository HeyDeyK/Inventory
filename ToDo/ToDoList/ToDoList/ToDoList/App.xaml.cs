using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Abstract;
using Xamarin.Forms;

namespace ToDoList
{
	public partial class App : Application
    {
        public App()
        {
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new BindingListview());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static TodoItemDatabase _database;

        public static TodoItemDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TodoItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return _database;
            }
        }
        private static DatabaseAccess _databaseAccess;

        public static DatabaseAccess DatabaseAccess
        {
            get
            {
                if (_databaseAccess == null)
                {
                    IFileHelper filehelperInstance = DependencyService.Get<IFileHelper>();
                    _databaseAccess = new DatabaseAccess(filehelperInstance.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _databaseAccess;
            }
        }
    }
}
