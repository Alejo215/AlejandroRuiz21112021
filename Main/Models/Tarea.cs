using SQLite;

namespace ListaTareas.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Completada { get; set; }
    }
}
