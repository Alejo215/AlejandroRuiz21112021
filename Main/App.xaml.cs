using ListaTareas.Data;
using ListaTareas.ViewModels;
using ListaTareas.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.IO;
using Xamarin.Forms;

namespace ListaTareas
{
    public partial class App : PrismApplication
    {
        static DatabaseQuery database;

        public static DatabaseQuery Database
        {
            get
            {
                if(database == null)
                {
                    database = new DatabaseQuery(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.db3"));
                }

                return database;
            }
        }
        public App(IPlatformInitializer init = null) : base(init){ }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/Main");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Main, MainModel>();
        }
    }
}
