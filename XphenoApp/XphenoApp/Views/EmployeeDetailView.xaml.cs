using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XphenoApp.ViewModel;

namespace XphenoApp.Views
{
    public partial class EmployeeDetailView : ContentPage
    {
        private EmployeeViewModel viewModel;

        public EmployeeDetailView(EmployeeViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }
    }
}

