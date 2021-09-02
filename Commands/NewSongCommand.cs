using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class NewSongCommand : ICommand
    {
        private MediaPlayerViewModel viewModel;

        //Constructor, gets viewmodel reference
        public NewSongCommand(MediaPlayerViewModel viewModel)
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
            if ("Next" == (string)parameter)
                return true;
            else
                return (viewModel.IsShuffle ? false : true);
        }

        //Execute clears the selectedfiles in viewmodel and clears the selected items through MainWindow.ListView
        public void Execute(object parameter)
        {
            if ("Next" == (string)parameter)
            {
                viewModel.PlayNext();
            }
            else
            {
                viewModel.PlayPrevious();
            }
        }
    }
}
