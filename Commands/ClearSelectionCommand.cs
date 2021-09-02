using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class ClearSelectionCommand : ICommand
    {
        private MediaManagerParent viewModel;

        //Constructor, gets viewmodel reference
        public ClearSelectionCommand(MediaManagerParent viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        //CanExecute if more than one file is selected
        public bool CanExecute(object parameter)
        {
            return viewModel.SelectedFiles.Count > 0;
        }

        //Execute clears the selectedfiles in viewmodel and clears the selected items through MainWindow.ListView
        public void Execute(object parameter)
        {
            viewModel.SelectedFiles.Clear();
            MediaManager window = (MediaManager)Application.Current.MainWindow;
            window.filesListView.SelectedItems.Clear();
        }
    }
}
