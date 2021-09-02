using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using System.Windows;
using EmilWallin_Assignment7.ViewModels;
using System.IO;

namespace EmilWallin_Assignment7.Commands
{
    class OpenFolderCommand : ICommand
    {
        private MediaManagerViewModel mediaManagerViewModel;

        //Constructor, gets viewmodel reference
        public OpenFolderCommand(MediaManagerViewModel viewModel)
        {
            mediaManagerViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Execute. Opens directory and 
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Gets all files in folder (not subfolders) using Ooki vista folder browser
        public void Execute(object parameter)
        {
            try
            {
                VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
                dialog.ShowNewFolderButton = false;

                if(dialog.ShowDialog() == true)
                {
                    if (dialog.SelectedPath != null)
                    {
                        DirectoryInfo di = new DirectoryInfo(dialog.SelectedPath);
                        FileInfo[] infoArray = di.GetFiles("*.*");
                        mediaManagerViewModel.AddToFileList(infoArray);
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }
    }
}
