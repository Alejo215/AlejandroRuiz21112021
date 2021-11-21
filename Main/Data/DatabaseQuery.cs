using ListaTareas.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListaTareas.Data
{
    public class DatabaseQuery
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseQuery(string path)
        {
            _database = new SQLiteAsyncConnection(path);

            _database.CreateTableAsync<Tarea>().Wait();
        }

        #region CRUD

        //Trae la lista de tareas
        public Task<List<Tarea>> GetTarea()
        {
            return _database.Table<Tarea>().ToListAsync();
        }

        //Actualiza o agrega una tarea
        public Task<int> SaveTareaAsync(Tarea tarea)
        {
            if(tarea.Id != 0)
            {
                return _database.UpdateAsync(tarea);
            }
            else
            {
                return _database.InsertAsync(tarea);
            }
        }

        //Eliminar tarea
        public Task<int> DeleteTarea(Tarea tarea)
        {
            return _database.DeleteAsync(tarea);
        }

        #endregion
    }
}
