using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class RemoveFilesCommand : ICommand
    {
        private MediaManagerParent viewModel;

        //Constructor. Gets reference of viewmodel
        public RemoveFilesCommand(MediaManagerParent viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //CanExecute if more than 0 files are selected
        public bool CanExecute(object parameter)
        {
            return viewModel.SelectedFiles.Count > 0;
        }

        //Execute! Gets the files to remove from the viewmodel.
        //Concocts strings for messagebox confirmation, and checks if file exists before deleting (and deletes them from the lists in viewmodel)
        public void Execute(object parameter)
        {
            List<FileInfoHolder> filesToRemove = new List<FileInfoHolder>(viewModel.SelectedFiles);

            string messageString = "Do you want to remove these files from the list?\n";
            string filesString = "";
            foreach(FileInfoHolder fi in filesToRemove)
            {
                filesString += fi.FileName + "\n";
            }

            if(MessageBox.Show(messageString + filesString, "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach(FileInfoHolder fi in filesToRemove)
                {

                    viewModel.DeleteFile(fi);

                }
            }

        }
    }
}
