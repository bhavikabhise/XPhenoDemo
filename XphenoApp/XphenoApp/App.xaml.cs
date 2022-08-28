using System;
using Autofac;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XphenoApp.DataStore;
using System.IO;
using static XphenoApp.Constants.GlobalConstants;
using System.Threading.Tasks;
using XphenoApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XphenoApp.SQLRepository.ISQLServices;
using XphenoApp.SQLRepository.SQLServices;
using System.Runtime.CompilerServices;
using XphenoApp.IService;
using XphenoApp.Service;
using XphenoApp.Views;
using XphenoApp.ViewModel;

namespace XphenoApp
{
    public partial class App : Application
    {
        private ContainerBuilder containerBuilder;
        private bool shouldBlockUI;

        public static IContainer DiContainer;

        public static App Instance { get; private set; }

        public App ()
        {
            InitializeComponent();
            Instance = this;
            InitialDataBase();
            InitDIContainter();
            MainPage = new SplashScreen();
        }

        public bool ShouldBlockUI
        {
            get => shouldBlockUI;
            set => SetProperty(ref shouldBlockUI, value);
        }

        private async void InitialDataBase()
        {
            SqlDataStore.CreateSharedDataStore(
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData
                        ),
                    DataStoreConstants.DatabaseName
                    )
                );
            await CreateAllTablesAsync();
        }

        private async Task CreateAllTablesAsync()
        {
            await SqlDataStore.SharedInstance.Database.CreateTableAsync<EmployeeModel>();
        }

        private void InitDIContainter()
        {
            this.containerBuilder = new ContainerBuilder();
            RegisterDIServices(this.containerBuilder);
        }

        //TODO: Remove content page dependencies from DI
        private void RegisterDIServices(ContainerBuilder builder)
        {
            builder.RegisterType<NavigatorService>().As<INavigatorService>().SingleInstance();
            builder.RegisterType<SqlService>().As<ISqlService>().SingleInstance();
            builder.RegisterType<SqlEmployeeService>().As<ISqlEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<ShowDialogs>().As<IShowDialogs>().SingleInstance();
            builder.RegisterType<ExceptionLogService>().As<IExceptionLogService>().SingleInstance();
            builder.RegisterType<EmployeeListView>();
            builder.RegisterType<EmployeeListViewModel>();
            builder.RegisterType<EmployeeDetailView>();
            builder.RegisterType<EmployeeViewModel>().WithParameter(
                new TypedParameter(typeof(EmployeeModel),null)).InstancePerLifetimeScope();
            builder.Register(c => new NavigationPage(c.Resolve<EmployeeListView>())).SingleInstance();
            builder.Register(c => c.Resolve<NavigationPage>().Navigation).As<INavigation>().SingleInstance();
        }

        public void BuildDIContainer()
        {
            DiContainer = this.containerBuilder.Build();
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

