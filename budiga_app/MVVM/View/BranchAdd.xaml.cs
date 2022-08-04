﻿using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace budiga_app.MVVM.View
{
    /// <summary>
    /// Interaction logic for BranchAdd.xaml
    /// </summary>
    public partial class BranchAdd : Window
    {
        public BranchAdd()
        {
            InitializeComponent();
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(locationTextBox.Text))
            {
                MessageBox.Show("Fill all empty fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                StoreModel.BranchModel branch = new StoreModel.BranchModel()
                {
                    Id = string.Format("{0}-{1}", nameTextBox.Text, GenerateId.GenerateNum()),
                    Name = nameTextBox.Text,
                    Location = locationTextBox.Text,
                };
                BranchRepository branchRepository = new BranchRepository();
                if (await branchRepository.AddBranch(branch))
                {
                    this.Close();
                }
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
