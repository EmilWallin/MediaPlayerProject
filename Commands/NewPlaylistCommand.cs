using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class NewPlaylistCommand : ICommand
    {
        private MediaManagerViewModel viewModel;

        public NewPlaylistCommand(MediaManagerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Can always open new playlist
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Creates new view, gets its viewmodel and opens it through the media manager
        public void Execute(object parameter)
        {
            PlaylistView view = new PlaylistView();
            viewModel.OpenPlaylistView(view.GetViewModel());
            view.Show();
        }
    }
}
