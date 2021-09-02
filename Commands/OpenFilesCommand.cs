using EmilWallin_Assignment7.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class OpenFilesCommand : ICommand
    {
        private MediaManagerViewModel mediaManagerViewModel;

        //Constructor, gets viewmodel reference
        public OpenFilesCommand(MediaManagerViewModel viewModel)
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

        //Open multiple files, uses Ooki vista file dialog
        public void Execute(object parameter)
        {
            try
            {
                VistaOpenFileDialog dialog = new VistaOpenFileDialog();
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == true)
                {
                    if (dialog.FileNames.Length > 0)
                    {
                        FileInfo[] infoArray = new FileInfo[dialog.FileNames.Length];
                        for (int i = 0; i < infoArray.Length; i++)
                        {
                            infoArray[i] = new FileInfo(dialog.FileNames[i]);
                        }
                        mediaManagerViewModel.AddToFileList(infoArray);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open files.", "Error");
            }
        }
    }
}
