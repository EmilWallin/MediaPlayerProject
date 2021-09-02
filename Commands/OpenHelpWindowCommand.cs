using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class OpenHelpWindowCommand : ICommand
    {


        //Constructor
        public OpenHelpWindowCommand()
        {

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

        //Open a new help window
        public void Execute(object parameter)
        {
            Help helpWindow = new Help();
            helpWindow.Show();
        }
    }
}
