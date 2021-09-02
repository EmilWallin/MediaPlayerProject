using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using EmilWallin_Assignment7.Models;
using System.Windows;

namespace EmilWallin_Assignment7.Commands
{
    class OpenPlaylistCommand : ICommand
    {
        private MediaManagerViewModel viewModel;

        public OpenPlaylistCommand(MediaManagerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Can always open a playlist
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Open file with .playlist extension. Uses LoadPersistent class
        public void Execute(object parameter)
        {
            try
            {
                VistaOpenFileDialog openDialog = new VistaOpenFileDialog();
                openDialog.Filter = "playlist files (*.playlist)|*.playlist";
                openDialog.Multiselect = false;

                if (openDialog.ShowDialog() != true)
                {
                    return;
                }

                Playlist playlist = SavingLoading.LoadPersistent.LoadBinary<Playlist>(openDialog.FileName);

                PlaylistView view = new PlaylistView(new PlaylistViewModel(playlist, openDialog.FileName));
                viewModel.OpenPlaylistView(view.GetViewModel());
                view.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
}
