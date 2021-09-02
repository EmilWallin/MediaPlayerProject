using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.SortingClasses;
using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class SortListViewCommand : ICommand
    {
        private MediaManagerParent viewModel;

        //Constructor, gets viewmodel reference
        public SortListViewCommand(MediaManagerParent viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Can always execute
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Gets parameter from listview header click and sorts list in viewmodel according to the IComparer from the switch
        public void Execute(object parameter)
        {
            string sortType = parameter.ToString();

            IComparer<FileInfoHolder> comparer = null;

            switch(sortType)
            {
                case "Media":
                    comparer = new SortMediaType();
                    break;
                case "Name":
                    comparer = new SortName();
                    break;
                case "Duration":
                    comparer = new SortDuration();
                    break;
                case "FileType":
                    comparer = new SortFileType();
                    break;
                default:
                    comparer = new SortName();
                    break;
            }
            viewModel.SortFileInfo(comparer);
        }
    }
}
