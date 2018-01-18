using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ToDoList.Abstract
{
    public class DatabaseAccess
    {

        private SQLiteAsyncConnection database;

        public DatabaseAccess(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItemConcretization>().Wait();
        }


        public Task<List<T>> GetItemsAsync<T>() where T : ATable, new()
        {
            return database.Table<T>().ToListAsync();
        }


        public Task<T> GetItemAsync<T>(int id) where T : ATable, new()
        {
            return database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync<T>(T item) where T : ATable, new()
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : ATable, new()
        {
            return database.DeleteAsync(item);
        }
    }

}