using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class TogglePlayCommand : ICommand
    {
        private MediaPlayerViewModel viewModel;

        //Constructor, gets viewmodel reference
        public TogglePlayCommand(MediaPlayerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //CanExecute always
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Execute, calls TogglePlay in viewmodel
        public void Execute(object parameter)
        {
            viewModel.TogglePlay();
        }
    }
}
