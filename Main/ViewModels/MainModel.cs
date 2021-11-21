using ListaTareas.Models;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace ListaTareas.ViewModels
{
    class MainModel : BindableBase
    {
        private int _id = 0;
        private string _nombre;
        private bool _completada;
        private Tarea _tareaSelected;
        private object _listViewSource;
        private string _textoBoton = "Agregar";
        private bool _eliminar;

        public MainModel()
        {
            AddEditCommand = new DelegateCommand(SaveTarea);
            DeleteTareaCommand = new DelegateCommand(DeleteTarea);
            LoadTarea();
        }

        #region Propiedades

        public int Id 
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public bool Completada
        {
            get => _completada;
            set => SetProperty(ref _completada, value);
        }

        public string TextoBoton
        {
            get => _textoBoton;
            set => SetProperty(ref _textoBoton, value);
        }

        public bool Eliminar
        {
            get => _eliminar;
            set => SetProperty(ref _eliminar, value);
        }

        public DelegateCommand AddEditCommand { get; set; }

        public DelegateCommand DeleteTareaCommand { get; set; }

        public object ListViewSource
        {
            get => _listViewSource;
            set => SetProperty(ref _listViewSource, value);
        }

        public Tarea TareaSelected
        {
            get => _tareaSelected;
            set 
            {
                if(_tareaSelected != value)
                {
                    SetProperty(ref _tareaSelected, value);
                    Selected();
                }
            }
        }

        #endregion

        #region Metodos

        //Se traen las tareas
        async void LoadTarea()
        {
            ListViewSource = await App.Database.GetTarea();
        }

        //Se agrega o edita una tarea
        async void SaveTarea()
        {
            if (!string.IsNullOrEmpty(_nombre))
            {
                var tarea = new Tarea
                {
                    Id = _id,
                    Nombre = _nombre,
                    Completada = _completada
                };

                await App.Database.SaveTareaAsync(tarea);

                Id = 0;
                Nombre = "";
                Completada = false;
                TextoBoton = "Agregar";
                Eliminar = false;

                LoadTarea();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Se debe ingresar un nombre.", "Aceptar");
            }
        }

        void Selected()
        {
            Id = _tareaSelected.Id;
            Nombre = _tareaSelected.Nombre;
            Completada = _tareaSelected.Completada;
            TextoBoton = "Editar";
            Eliminar = true;
        }

        async void DeleteTarea()
        {
            await App.Database.DeleteTarea(_tareaSelected);

            Id = 0;
            Nombre = "";
            Completada = false;
            TextoBoton = "Agregar";
            Eliminar = false;

            LoadTarea();
        }

        #endregion
    }
}
