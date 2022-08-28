using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XphenoApp.ViewModel;

namespace XphenoApp.Views
{
    public partial class EmployeeListView : ContentPage
    {
        private EmployeeListViewModel viewModel;

        public EmployeeListView(EmployeeListViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel.Intitialise();
        }
    }
}

